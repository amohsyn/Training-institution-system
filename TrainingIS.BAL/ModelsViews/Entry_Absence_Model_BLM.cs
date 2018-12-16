using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL.ModelsViews
{
    /// <summary>
    ///  Entry_Absence_Model BLM
    /// </summary>
    public class Entry_Absence_Model_BLM : BaseModelBLM
    {
        public Entry_Absence_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) : base(unitOfWork, GAppContext)
        {
        }

        /// <summary>
        /// Get the List of AbsenceInfo of All Trainee of Groupe
        /// for one trainee seel olso : Get_Trainee_Entry_Absence_Model
        /// 
        /// this list is used to entry absence of one groupe
        /// We count only the absences with Authorized = false
        /// </summary>
        /// <param name="seanceTraining"></param>
        /// <returns>a list of AbsenceInfo of a Group in SeaneTraining</returns>
        public List<Entry_Absence_Model> Get_Entry_Absence_Models(SeanceTraining seanceTraining)
        {
            // Collect Data : Group, ModuleTraining,  SancePlanning
            SeancePlanning seancePlanning = seanceTraining.SeancePlanning;
            Int64 GroupId = seancePlanning.Training.Group.Id;
            Int64 ModuleTrainingId = seancePlanning.Training.ModuleTraining.Id;

            // BLO Instance
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            // Trainees of Current Group 
            var Trainees_of_Current_Group_Query = from trainee in traineeBLO.Trainee_Active_Query()
                                                  where trainee.GroupId == GroupId
                                                  select trainee;


            // Absences of Trainees  in current TrainingYear ( the current TrainingYear is fixex by the group)
            var Absences_of_Trainees_Query = from absence in absenceBLO.Absences_Query()
                                             where absence.SeanceTraining.SeancePlanning.Training.Group.Id == GroupId
                                             group absence by absence.TraineeId into Trainees_Absences
                                             select new
                                             {
                                                 TraineeId = Trainees_Absences.Key,
                                                 AbsenceCount = Trainees_Absences.Count(),
                                                 InValideAbsences = Trainees_Absences.Where(a => a.AbsenceState == Entities.enums.AbsenceStates.InValid_Absence).ToList(),
                                                 Absence = Trainees_Absences.Where(a => a.SeanceTrainingId == seanceTraining.Id).FirstOrDefault()
                                             };

            var Trainees_And_Its_Absences_Query = from trainee in Trainees_of_Current_Group_Query
                                                  join Absences_of_Trainees in Absences_of_Trainees_Query
                                                  on trainee.Id equals Absences_of_Trainees.TraineeId
                                                  into absence
                                                  from trainee_absence in absence.DefaultIfEmpty()

                                                  select new
                                                  {
                                                      Trainee = trainee,
                                                      trainee_absence.AbsenceCount,
                                                      trainee_absence.InValideAbsences,
                                                      trainee_absence.Absence
                                                  };


            // Trainees_Absences In Current Module in current Training Year
            var Absences_of_Trainees_In_Module_Query = from absence in absenceBLO.Absences_Query()
                                                       where absence.SeanceTraining.SeancePlanning.Training.Group.Id == GroupId
                                                          && absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Id == ModuleTrainingId
                                                       group absence by absence.TraineeId into Trainees_Absences
                                                       select new
                                                       {
                                                           TraineeId = Trainees_Absences.Key,
                                                           Absences_In_Current_Module = Trainees_Absences.ToList()
                                                       };

            var Trainees_And_Absences_In_Module_Query = from Trainees_Of_Current_Group in Trainees_of_Current_Group_Query
                                                        join Absences_of_Trainees_In_Module in Absences_of_Trainees_In_Module_Query
                                                        on Trainees_Of_Current_Group.Id equals Absences_of_Trainees_In_Module.TraineeId
                                                        into Trainees_Absences
                                                        from absence in Trainees_Absences.DefaultIfEmpty()
                                                        select new
                                                        {
                                                            Trainees_Of_Current_Group,
                                                            absence.Absences_In_Current_Module
                                                        };


            var Entry_Absence_Model_Query = from Trainees_And_Its_Absences in Trainees_And_Its_Absences_Query
                                            join Trainees_And_Absences_In_Module in Trainees_And_Absences_In_Module_Query
                                            on Trainees_And_Its_Absences.Trainee.Id equals Trainees_And_Absences_In_Module.Trainees_Of_Current_Group.Id
                                            orderby Trainees_And_Its_Absences.Trainee.FirstName
                                            select new Entry_Absence_Model
                                            {
                                                Trainee = Trainees_And_Its_Absences.Trainee,
                                                TraineeId = Trainees_And_Its_Absences.Trainee.Id,
                                                TraineeFirstName = Trainees_And_Its_Absences.Trainee.FirstName,
                                                TraineeLastName = Trainees_And_Its_Absences.Trainee.LastName,
                                                AbsenceCount = Trainees_And_Its_Absences.AbsenceCount,
                                                InValideAbsences = Trainees_And_Its_Absences.InValideAbsences,
                                                Absences_In_Current_Module = Trainees_And_Absences_In_Module.Absences_In_Current_Module,
                                                SeanceTrainingId = seanceTraining.Id,
                                                Absence = Trainees_And_Its_Absences.Absence,
                                                Last_Valid_Attendance_Sanction = Trainees_And_Its_Absences.Trainee.AttendanceState.Valid_Sanction,
                                                Valid_Note = Trainees_And_Its_Absences.Trainee.AttendanceState.Valid_Note,
                                                Invalid_Note = Trainees_And_Its_Absences.Trainee.AttendanceState.Invalid_Note,
                                                AttendanceState = Trainees_And_Its_Absences.Trainee.AttendanceState
                                            };

            var Entry_Absence_Models = Entry_Absence_Model_Query.ToList();
            foreach (var Entry_Absence_Model in Entry_Absence_Models)
            {
                this.Calculate_Entry_Absence_Model(Entry_Absence_Model);
            }

            return Entry_Absence_Models;
        }

        /// <summary>
        /// Get the AbsenceInfo of one trainee : used by Create Absence and Delete absence by Ajax
        /// </summary>
        /// <param name="seanceTraining"></param>
        /// <param name="TraineeId"></param>
        /// <returns></returns>
        public Entry_Absence_Model Get_Trainee_Entry_Absence_Model(SeanceTraining seanceTraining, Int64 TraineeId)
        {

            // BLO Instance
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);

            // Collect Data
            Trainee trainee = traineeBLO.FindBaseEntityByID(TraineeId);
            Int64 GroupId = trainee.GroupId;
            Int64 ModuleTrainingId = seanceTraining.SeancePlanning.Training.ModuleTraining.Id;


            // Trainee Absence in current TrainingYear ( the current TrainingYear is fixex by the group)
            var Absences_Of_Trainee_Query = from absence in absenceBLO.Absences_Query()
                                            where absence.TraineeId == TraineeId
                                            group absence by absence.TraineeId into Trainees_Absences
                                            select new
                                            {
                                                Trainees_Absences.FirstOrDefault().Trainee,
                                                AbsenceCount = Trainees_Absences.Count(),
                                                InValideAbsences = Trainees_Absences.Where(a => a.AbsenceState == Entities.enums.AbsenceStates.InValid_Absence).ToList(),
                                                Absence = Trainees_Absences.Where(a => a.SeanceTrainingId == seanceTraining.Id).FirstOrDefault()
                                            };



            // Trainees_Absences In Current Module and TraineeYear
            var Absences_of_Trainee_In_Module_Query = from absence in absenceBLO.Absences_NotAuthorized_Query()
                                                where absence.SeanceTraining.SeancePlanning.Training.Group.Id == GroupId
                                                               && absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Id == ModuleTrainingId
                                                               && absence.TraineeId == TraineeId
                                                group absence by absence.TraineeId into Trainees_Absences
                                                select new
                                                {
                                                    TraineeId = Trainees_Absences.Key,
                                                    Absences_In_Current_Module = Trainees_Absences.ToList()
                                                };





            Entry_Absence_Model entry_Absence_Model = null;

            if (Absences_of_Trainee_In_Module_Query.Count() > 0)
            {
                var Query_Entry_Absence_Model = from Absences_Of_Trainee in Absences_Of_Trainee_Query
                                                join Absences_of_Trainee_In_Module in Absences_of_Trainee_In_Module_Query
                                                on Absences_Of_Trainee.Trainee.Id equals Absences_of_Trainee_In_Module.TraineeId
                                                orderby Absences_Of_Trainee.Trainee.FirstName
                                                select new Entry_Absence_Model
                                                {
                                                    Trainee = Absences_Of_Trainee.Trainee,
                                                    TraineeId = Absences_Of_Trainee.Trainee.Id,
                                                    TraineeFirstName = Absences_Of_Trainee.Trainee.FirstName,
                                                    TraineeLastName = Absences_Of_Trainee.Trainee.LastName,
                                                    AbsenceCount = Absences_Of_Trainee.AbsenceCount,
                                                    InValideAbsences = Absences_Of_Trainee.InValideAbsences,
                                                    Absences_In_Current_Module = Absences_of_Trainee_In_Module.Absences_In_Current_Module,
                                                    SeanceTrainingId = seanceTraining.Id,
                                                    Absence = Absences_Of_Trainee.Absence,
                                                    Last_Valid_Attendance_Sanction = Absences_Of_Trainee.Trainee.AttendanceState.Valid_Sanction,
                                                    Valid_Note = Absences_Of_Trainee.Trainee.AttendanceState.Valid_Note,
                                                    Invalid_Note = Absences_Of_Trainee.Trainee.AttendanceState.Invalid_Note,
                                                    AttendanceState = Absences_Of_Trainee.Trainee.AttendanceState
                                                };

                entry_Absence_Model = Query_Entry_Absence_Model.FirstOrDefault();
            }
            else
            {
                var Query_Entry_Absence_Model = from Absences_Of_Trainee in Absences_Of_Trainee_Query
                                                orderby Absences_Of_Trainee.Trainee.FirstName
                                                select new Entry_Absence_Model
                                                {
                                                    Trainee = Absences_Of_Trainee.Trainee,
                                                    TraineeId = Absences_Of_Trainee.Trainee.Id,
                                                    TraineeFirstName = Absences_Of_Trainee.Trainee.FirstName,
                                                    TraineeLastName = Absences_Of_Trainee.Trainee.LastName,
                                                    AbsenceCount = Absences_Of_Trainee.AbsenceCount,
                                                    InValideAbsences = Absences_Of_Trainee.InValideAbsences,
                                                    SeanceTrainingId = seanceTraining.Id,
                                                    Absence = Absences_Of_Trainee.Absence,
                                                    Last_Valid_Attendance_Sanction = Absences_Of_Trainee.Trainee.AttendanceState.Valid_Sanction,
                                                    Valid_Note = Absences_Of_Trainee.Trainee.AttendanceState.Valid_Note,
                                                    Invalid_Note = Absences_Of_Trainee.Trainee.AttendanceState.Invalid_Note,
                                                    AttendanceState = Absences_Of_Trainee.Trainee.AttendanceState
                                                };

                entry_Absence_Model = Query_Entry_Absence_Model.FirstOrDefault();

            }





            if (entry_Absence_Model != null) return entry_Absence_Model;
            else
            {
                entry_Absence_Model = new Entry_Absence_Model();
                entry_Absence_Model.TraineeId = trainee.Id;
                entry_Absence_Model.Trainee = trainee;
                entry_Absence_Model.TraineeFirstName = trainee.FirstName;
                entry_Absence_Model.TraineeLastName = trainee.LastName;
                entry_Absence_Model.SeanceTrainingId = seanceTraining.Id;

                if(trainee.AttendanceState != null)
                {
                    entry_Absence_Model.Last_Valid_Attendance_Sanction = trainee.AttendanceState.Valid_Sanction;
                    entry_Absence_Model.Valid_Note = trainee.AttendanceState.Valid_Note;
                    entry_Absence_Model.Invalid_Note = trainee.AttendanceState.Invalid_Note;
                    entry_Absence_Model.AttendanceState = trainee.AttendanceState;
                }


                // Calculate 
                this.Calculate_Entry_Absence_Model(entry_Absence_Model);
                return entry_Absence_Model;
            }
        }

        public void Calculate_Entry_Absence_Model(Entry_Absence_Model entry_Absence_Model)
        {
            List<string> Notifications = new List<string>();
            List<string> Html_Classes = new List<string>();

            // 
            // Existance of Invalide Absence
            //
            string msg_invalide_absence = "";
            if (entry_Absence_Model.InValideAbsenceCount == 1)
            {
                msg_invalide_absence = "Une absence non valide";
            }
            if (entry_Absence_Model.InValideAbsenceCount > 1)
            {
                msg_invalide_absence = string.Format("{0} absences non valide", entry_Absence_Model.InValideAbsenceCount);
            }
            Notifications.Add( msg_invalide_absence);


            // Justified absence with Sanction of Exclusion of N Days
            if (entry_Absence_Model.Absence != null
                && entry_Absence_Model.AbsenceState == AbsenceStates.Justified_Absence
                && entry_Absence_Model.Absence.JustificationAbsence.Reference == Category_JustificationAbsenceBLO.Absence_Sanction_Justification
                )
            {
                string msg_notification = string.Format("Le stagiaire n'est pas autorisé à entrer dans la séance due une sanction de conseil disciplinaire");
                Notifications.Add(msg_notification);
                Html_Classes.Add("Absence_By_System");
            }

            // Add Absence State 
            if (entry_Absence_Model.Absence != null)
            {
                Html_Classes.Add(entry_Absence_Model.AbsenceState.ToString());
            }
 
            // Write ot Object
            entry_Absence_Model.Notification = string.Join(" , ",Notifications);
            entry_Absence_Model.Html_Classes = string.Join(" ", Html_Classes);
        }

    }
}
