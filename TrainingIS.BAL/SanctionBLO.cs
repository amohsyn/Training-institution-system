using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.Services.Import;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.SanctionResources;

namespace TrainingIS.BLL
{
    public partial class SanctionBLO
    {


        #region Find
        public Sanction Find_By_Meeting_Id(long MeetingId)
        {
            var sanction = this._UnitOfWork.context.Sanctions.Where(s => s.MeetingId == MeetingId).FirstOrDefault();
            return sanction;
        }
        public List<Sanction> Find_InValide_Sanction(long Trainee_Id)
        {
            var inValideSanctions = this._UnitOfWork.context.Sanctions
                .Where(s => s.Trainee.Id == Trainee_Id)
                .Where(s => s.SanctionState == Entities.enums.SanctionStates.InValid);

            return inValideSanctions.ToList();
        }
        public List<Sanction> Find_Valide_Sanction(long Trainee_Id)
        {
            var inValideSanctions = this._UnitOfWork.context.Sanctions
                 .Where(s => s.Trainee.Id == Trainee_Id)
                .Where(s => s.SanctionState == Entities.enums.SanctionStates.Valid);

            return inValideSanctions.ToList();
        }
        public Sanction Find_Current_Invalid_Sanction(long trainee_id)
        {
            var Sanction = this._UnitOfWork.context.Sanctions
                .Where(s => s.TraineeId == trainee_id)
                .Where(s => s.SanctionState == SanctionStates.InValid)
                .OrderByDescending(s => s.SanctionCategory.WorkflowOrder)
                .FirstOrDefault();

            return Sanction;
        }

        public Sanction Find_Current_Valide_Sanction(long trainee_id)
        {
            var Sanction = this._UnitOfWork.context.Sanctions
                 .Where(s => s.TraineeId == trainee_id)
                 .Where(s => s.SanctionState == SanctionStates.Valid)
                 .OrderByDescending(s => s.SanctionCategory.WorkflowOrder)
                 .FirstOrDefault();

            return Sanction;
        }
        #endregion

        #region CRUD
        public override int Save(Sanction item)
        {
            // BLO
            AttendanceStateBLO attendanceStateBLO = new AttendanceStateBLO(this._UnitOfWork, this.GAppContext);

            // Check Uniqkness of Sanction by User
            this.Check_Unitqueness_of_Sanction_By_Trainee_And_SanctionCategory(item);
            int return_value = base.Save(item);

            // Update AttendanceState
            attendanceStateBLO.Update(item.Trainee.Id);

            return return_value;
        }
        public override int Delete(Sanction item)
        {
            long TraineeId = item.Trainee.Id;
            // BLO
            AttendanceStateBLO attendanceStateBLO = new AttendanceStateBLO(this._UnitOfWork, this.GAppContext);

            var r = base.Delete(item);

            // Update AttendanceState
            attendanceStateBLO.Update(TraineeId);
            return r;
        }

        private void Check_Unitqueness_of_Sanction_By_Trainee_And_SanctionCategory(Sanction item)
        {
            var Query = this._UnitOfWork.context.Sanctions
                .Where(s => s.Trainee.Id == item.Trainee.Id)
                .Where(s => s.SanctionCategory.Id == item.SanctionCategory.Id);

            // Update Case
            if (item.Id != 0)
            {
                Query = Query.Where(s => s.Id != item.Id);
            }

            var existant_sanction = Query.FirstOrDefault();
            if (existant_sanction != null)
            {
                // [Localization]
                string msg_ex = string.Format("il exist déja une sanction du catégorie {0} pour le stagiaire {1} pour la discipline {2}",
                    item.SanctionCategory.Name,
                    item.Trainee.GetFullName(),
                    item.SanctionCategory.DisciplineCategory.Name
                    );
                throw new BLL_Exception(msg_ex);
            }
        }
        #endregion

        #region Calculate InValide Sanctions and AttendenceStates
        /// <summary>
        ///  Calculate Invalide Sanctions for Attendance_DisciplineCategory
        /// </summary>
        /// <param name="Trainee_Id"></param>
        /// <returns></returns>
        public List<Sanction> Calculate_InValide_Sanctions(long Trainee_Id)
        {

            // BLO
            AbsenceBLO absenceBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);
            AttendanceStateBLO attendanceStateBLO = new AttendanceStateBLO(this._UnitOfWork, this.GAppContext);
            SanctionCategoryBLO sanctionCategory = new SanctionCategoryBLO(this._UnitOfWork, this.GAppContext);

            // Return value
            List<Sanction> InValide_Sanctions = new List<Sanction>();


            // Get Absences with Valid_Absence state
            var Valid_Absences = absenceBLO.Find_Absences_By_States(Trainee_Id, Entities.enums.AbsenceStates.Valid_Absence);


            // Groups absences by Trainees
            var Trainees_Absences = Valid_Absences
                .GroupBy(a => a.Trainee)
                .Select(g => new { Tainee = g.Key, Absences = g.ToList() }).ToList();

            //
            // Calculate Attentance Sanctions
            //
            System_DisciplineCategories Attendance_DisciplineCategory = System_DisciplineCategories.Attendance;
            foreach (var Trainee_Absences in Trainees_Absences)
            {
                AttendanceState attendanceState = attendanceStateBLO.Find_Or_Create_AttendanceState(Trainee_Absences.Tainee.Id);

                var Absences_Ordered_By_Date = Trainee_Absences.Absences.OrderBy(a => a.AbsenceDate).ToList();
                int Number_Absences_Not_Sanctioned_In_Minute = Absences_Ordered_By_Date
                    .Select(a => a.SeanceTraining.Duration)
                    .Sum();

                // Current_Sanction_Category
                SanctionCategory Current_Sanction_Category = null;
                if (attendanceState.Valid_Sanction != null)
                {
                    Current_Sanction_Category = attendanceState.Valid_Sanction.SanctionCategory;
                }


                // Sanction Next_InValid_Sanction = attendanceState.Invalid_Sanction;
                int skip_minute = 0;
                do
                {
                    // Next Sanction Category
                    Current_Sanction_Category = sanctionCategory.Get_Next_SanctionCategory(Current_Sanction_Category, Attendance_DisciplineCategory);

                    // d'ont create sanction if the next sanction d'ont exist
                    if (Current_Sanction_Category == null) break;

                    // Create Next_Ivalid_Sanction
                    Sanction sanction = this.CreateInstance();
                    sanction.Trainee = Trainee_Absences.Tainee;
                    sanction.SanctionState = SanctionStates.InValid;
                    sanction.SanctionCategory = Current_Sanction_Category;


                    int Plurality_Of_Absences_Minute = sanction.SanctionCategory.Plurality_Of_Absences;

                    // we can note save Invalide Sanctions with absences 
                    // because we will not be able to delete a absence

                    var Absences = this
                        .Skip_And_Take_Absences_By_Minute(Absences_Ordered_By_Date, skip_minute, Plurality_Of_Absences_Minute);

                    if (Absences != null && Absences.Count > 0)
                        InValide_Sanctions.Add(sanction);

                    skip_minute += Plurality_Of_Absences_Minute;

                } while (skip_minute < Number_Absences_Not_Sanctioned_In_Minute);
            }
            return InValide_Sanctions;
        }


        /// <summary>
        /// Skip and Take absences from a lite by Minute
        /// </summary>
        /// <param name="absences_Ordered_By_Date"></param>
        /// <param name="skip_minute"></param>
        /// <param name="take_Minute"></param>
        /// <returns>List of Taked absences or null</returns>
        private List<Absence> Skip_And_Take_Absences_By_Minute(List<Absence> absences_Ordered_By_Date, int skip_minute, int take_Minute)
        {
            List<Absence> absences_resulat = new List<Absence>();
            int sum_skiped = 0;
            int sum_take = 0;
            foreach (Absence absence in absences_Ordered_By_Date)
            {
                sum_skiped += absence.SeanceTraining.Duration;

                if (sum_skiped > skip_minute)
                {
                    absences_resulat.Add(absence);
                    sum_take += absence.SeanceTraining.Duration;
                }

                if (sum_take >= take_Minute)
                {
                    return absences_resulat;
                }
            }
            return null;
        }

        public int Update_InValide_Sanction(long Trainee_Id)
        {
            int Updated_Object = 0;
            this.Delete_InValide_Sanction(Trainee_Id);
            var InValideSanctions = this.Calculate_InValide_Sanctions(Trainee_Id);
            foreach (var inValid_sanction in InValideSanctions)
            {
                Updated_Object += this.Save(inValid_sanction);
            }
            return Updated_Object;
        }
        public int Delete_InValide_Sanction(long Trainee_Id)
        {
            int Deleted = 0;
            AbsenceBLO absenceBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);
            var InValideSanctions = this.Find_InValide_Sanction(Trainee_Id);
            foreach (var item in InValideSanctions)
            {

                // Delete Absence and Seanction RelationShip
                foreach (var absence in item.Absences)
                {
                    absence.Sanction = null;
                    absenceBLO.Save(absence);
                }
                Deleted += this.Delete(item);
            }
            return Deleted;
        }


        #endregion

        #region Import & Export
        /// <summary>
        /// Export all data to DataTable
        /// </summary>
        /// <returns>DataTable contain all data in database</returns>
        public virtual DataTable Import_File_Example()
        {
            ExportService exportService = new ExportService(typeof(Sanction));
            DataTable entityDataTable = exportService.CreateDataTable(msg_Sanction.PluralName);
            exportService.Fill(entityDataTable, this.FindAll().ToList<object>());
            return entityDataTable;
        }

        /// <summary>
        /// Export Selected Filtered Data And Searched Data without pagination
        /// </summary>
        /// <returns></returns>
        public virtual DataTable Export()
        {

            ExportService exportService = new ExportService(typeof(Sanction));
            DataTable entityDataTable = exportService.CreateDataTable(msg_Sanction.PluralName);
            exportService.Fill(entityDataTable, this.FindAll().ToList<object>());
            return entityDataTable;
        }
        #endregion
    }
}
