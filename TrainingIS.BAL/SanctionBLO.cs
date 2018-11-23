﻿using GApp.Entities;
using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.BLL.Services.Import;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.Resources.SanctionResources;

namespace TrainingIS.BLL
{
    public partial class SanctionBLO
    {


        #region Find
        public Sanction Find_By_Meeting_Id(long MeetingId)
        {
            var sanction = this._UnitOfWork.context.Sanctions.Where(s => s.Meeting.Id == MeetingId).FirstOrDefault();
            return sanction;
        }
        public List<Sanction> Find_InValide_Sanction(long Trainee_Id)
        {
            var inValideSanctions = this._UnitOfWork.context.Sanctions
                .Where(s => s.Trainee.Id == Trainee_Id)
                .Where(s => s.SanctionState == Entities.enums.SanctionStates.InValid);

            return inValideSanctions.ToList();
        }

        /// <summary>
        /// Find the Invalid sanction by Trainee Trainee_Id and DisciplineCategory_Id
        /// </summary>
        /// <param name="Trainee_Id"></param>
        /// <param name="DisciplineCategory_Id"></param>
        /// <returns>List of InValid Sanction</returns>
        public List<Sanction> Find_InValide_Sanction(long Trainee_Id, long DisciplineCategory_Id)
        {
            var inValideSanctions = this._UnitOfWork.context.Sanctions
                .Where(s => s.Trainee.Id == Trainee_Id)
                .Where(s => s.SanctionCategory.DisciplineCategory.Id == DisciplineCategory_Id)
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
           return this.Delete(item, true);
        }
        public int Delete(Sanction item, bool isUpdate_InValid_Sanction)
        {
            long TraineeId = item.Trainee.Id;

            // BLO
            AttendanceStateBLO attendanceStateBLO = new AttendanceStateBLO(this._UnitOfWork, this.GAppContext);
            AbsenceBLO absenceBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);

            // Delete AttendanceState
            var AttendanceState = attendanceStateBLO.Find_Or_Create_AttendanceState(TraineeId);
            attendanceStateBLO.Delete(AttendanceState);

            // Delete Absence and Seanction RelationShip
            for (int i = 0; i < item.Absences.Count; i++)
            {
                item.Absences[i].Sanction = null;
                absenceBLO.Save(item.Absences[i]);
            }

            var r = base.Delete(item);

            if (isUpdate_InValid_Sanction)
            {
                this.Update_InValide_Sanction(TraineeId);
            }
            
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

                     

                    var Absences = this
                        .Skip_And_Take_Absences_By_Minute(Absences_Ordered_By_Date, skip_minute, Plurality_Of_Absences_Minute);

                    if (Absences != null && Absences.Count > 0)
                    {
                        sanction.Absences = new List<Absence>();
                        sanction.Absences.AddRange(Absences);
                        InValide_Sanctions.Add(sanction);
                    }
                       

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
                Deleted += this.Delete(item, false);
            }
            return Deleted;
        }


        #endregion

        #region Import & Export


        ///// <summary>
        ///// Export Selected Filtered Data And Searched Data without pagination
        ///// </summary>
        ///// <param name="Controller_Reference"> Reference of Controller to find the last applied filter</param>
        ///// <returns></returns>
        //public DataTable Export(string Controller_Reference)
        //{
        //    Int32 _TotalRecords = 0;
        //    List<string> SearchCreteria = this.GetSearchCreteria();
        //    List<Export_Sanction_Model> _ListDefault_Details_Sanction_Model = null;
        //    FilterRequestParams filterRequestParams = null;
        //    try
        //    {
        //        filterRequestParams = this.Save_OR_Load_filterRequestParams_State(filterRequestParams, Controller_Reference);
        //        filterRequestParams.pageSize = 0;
        //        _ListDefault_Details_Sanction_Model = new Export_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext)
        //            .Find(filterRequestParams, SearchCreteria, out _TotalRecords);

        //    }
        //    catch (Exception ex)
        //    {
        //        filterRequestParams = new FilterRequestParams();
        //        this.Delete_filterRequestParams_State(Controller_Reference);
        //        filterRequestParams.pageSize = 0;
        //        _ListDefault_Details_Sanction_Model = new Export_Sanction_ModelBLM(this._UnitOfWork, this.GAppContext)
        //          .Find(filterRequestParams, SearchCreteria, out _TotalRecords);
        //    }

        //    ExportService exportService = new ExportService(typeof(Sanction), typeof(Export_Sanction_Model));
        //    DataTable dataTable = exportService.CreateDataTable(msg_Sanction.PluralName);
        //    exportService.Fill(dataTable, _ListDefault_Details_Sanction_Model.Cast<object>().ToList());

        //    return dataTable;
        //}
        #endregion


        #region Validate Sanction
        /// <summary>
        /// Valide the sanction and create the meetting with the presence of all members
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Meeting Validate_Sanction(long id)
        {
            // BLO
            MeetingBLO meetingBLO = new MeetingBLO(this._UnitOfWork, this.GAppContext);
            WorkGroupBLO workGroupBLO = new WorkGroupBLO(this._UnitOfWork, this.GAppContext);
            Mission_Working_GroupBLO mission_Working_GroupBLO = new Mission_Working_GroupBLO(this._UnitOfWork, this.GAppContext);
            AbsenceBLO absenceBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);
            // Find the sanction
            Sanction Sanction = this.FindBaseEntityByID(id);

            // Check if the sanction is Invalide
            if (Sanction.SanctionState != Entities.enums.SanctionStates.InValid)
            {
                // [Localization]
                string msg_ex = "Pour valider une sanction il doit être en état non valide";
                throw new BLL_Exception(msg_ex);
            }

            // Check if the sanction is the last Invalide Sanction in the WorkFlow
            List<Sanction> InValide_Sanctions = this
                .Find_InValide_Sanction(Sanction.Trainee.Id, Sanction.SanctionCategory.DisciplineCategory.Id)
                .OrderBy(s => s.SanctionCategory.WorkflowOrder)
                .ToList();
            if (!(InValide_Sanctions.First().Id == Sanction.Id))
            {
                
                InValide_Sanctions.Remove(Sanction);

                // [Localization]
                string msg_ex = "Vous devez valider les sanctions précédentes en ordre : ";
                msg_ex += string.Join(" , ", InValide_Sanctions.Select(s => s.ToString()).ToList());
                throw new BLL_Exception(msg_ex);
            }


            // Sanve Meeting
            var Mision_Work_Group = mission_Working_GroupBLO.Find_By_Sanction(Sanction.Id);
            var WorkGroup = workGroupBLO.Find_By_Mission_Workgin_Group(Mision_Work_Group.Id);
            Meeting meeting = meetingBLO.CreateInstance();
            meeting.MeetingDate = DateTime.Now;
            meeting.WorkGroup = WorkGroup;
            meeting.Mission_Working_Group = Mision_Work_Group;
            meetingBLO.Add_Presence_Of_All_Members(meeting);
            meetingBLO.Save(meeting);

            // Change Sanction State
            Sanction.SanctionState = SanctionStates.Valid;
            Sanction.Meeting = meeting;
            this.Save(Sanction);

            // Change Absences States
            foreach (Absence absence in Sanction.Absences)
            {
                absenceBLO.ChangeState_to_Sanctioned(absence.Id);
            }

            return meeting;
        }

        //private bool Is_First_InValide_Sanction_In_WorkFlow(Sanction sanction)
        //{
        //    // BLO
        //    SanctionCategoryBLO sanctionCategoryBLO = new SanctionCategoryBLO(this._UnitOfWork, this.GAppContext);
        //    var sanctions_categories = sanctionCategoryBLO
        //        .Find_By_System_DisciplineCategory(
        //        sanction
        //        .SanctionCategory
        //        .DisciplineCategory
        //        .System_DisciplineCategy
        //        );

        //    if(sanctions_categories.First().Id == sanction.SanctionCategory.Id)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }
}
