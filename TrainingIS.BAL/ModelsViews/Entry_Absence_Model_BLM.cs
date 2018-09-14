using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL.ModelsViews
{
    public class Entry_Absence_Model_BLM : BaseModelBLM
    {
        public Entry_Absence_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) : base(unitOfWork, GAppContext)
        {
        }

        public List<Entry_Absence_Model> Get_Entry_Absence_Models(SeanceTraining seanceTraining)
        {

            SeancePlanning seancePlanning = seanceTraining.SeancePlanning;
            this.UnitOfWork.context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Int64 GroupId = seancePlanning.Training.Group.Id;
            Int64 ModuleTrainingId = seancePlanning.Training.ModuleTraining.Id;


            // Trainees of Current Group 
            var Query_Trainees = from trainee in this.UnitOfWork.context.Trainees
                                 where trainee.GroupId == GroupId
                                 select new
                                 {
                                     TraineeId = trainee.Id,
                                     TraineeFirstName = trainee.FirstName,
                                     TraineeLastName = trainee.LastName
                                 };

            // Trainee Absence in current TrainingYear ( the current TrainingYear is fixex by the group)
            var Query_Just_Trainees_Absences = from absence in this.UnitOfWork.context.Absences
                                               where absence.SeanceTraining.SeancePlanning.Training.Group.Id == GroupId
                                               group absence by absence.TraineeId into Trainees_Absences
                                               select new 
                                               {
                                                   TraineeId = Trainees_Absences.Key,
                                                   AbsenceCount = Trainees_Absences.Count(),
                                                   InValideAbsences = Trainees_Absences.Where(a => a.Valide == false).ToList(),
                                                   Absence = Trainees_Absences.Where(a=>a.SeanceTrainingId == seanceTraining.Id).FirstOrDefault()
                                               };

            var Query_Trainees_Absences = from trainee in Query_Trainees
                                          join just_trainee_absence in Query_Just_Trainees_Absences
                                          on trainee.TraineeId equals just_trainee_absence.TraineeId
                                          into absece
                                          from trainee_absence in absece.DefaultIfEmpty()

                                          select new
                                          {
                                              trainee.TraineeId,
                                              trainee.TraineeFirstName,
                                              trainee.TraineeLastName,
                                              trainee_absence.AbsenceCount,
                                              trainee_absence.InValideAbsences,
                                              trainee_absence.Absence
                                          };


            // Trainees_Absences In Current Module and TraineeYear
            var Query_Just_Trainees_Absences_In_Current_Module = from absence in this.UnitOfWork.context.Absences
                                                                 where absence.SeanceTraining.SeancePlanning.Training.Group.Id == GroupId
                                                                    && absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Id == ModuleTrainingId
                                                                 group absence by absence.TraineeId into Trainees_Absences
                                                                 select new
                                                                 {
                                                                     TraineeId = Trainees_Absences.Key,
                                                                     Absences_In_Current_Module = Trainees_Absences.ToList()
                                                                 };

            var Query_Trainees_Absences_In_Current_Module = from Trainees_Of_Current_Group in Query_Trainees
                                                            join Trainees_Absences_In_Current_Module in Query_Just_Trainees_Absences_In_Current_Module
                                                            on Trainees_Of_Current_Group.TraineeId equals Trainees_Absences_In_Current_Module.TraineeId
                                                            into Trainees_Absences
                                                            from absence in Trainees_Absences.DefaultIfEmpty()

                                                            select new
                                                            {
                                                                Trainees_Of_Current_Group.TraineeId,
                                                                Trainees_Of_Current_Group.TraineeFirstName,
                                                                Trainees_Of_Current_Group.TraineeLastName,
                                                                absence.Absences_In_Current_Module
                                                            };


            var Query_Entry_Absence_Model = from entry_absence in Query_Trainees_Absences
                                            join absene_in_current_module in Query_Trainees_Absences_In_Current_Module
                                            on entry_absence.TraineeId equals absene_in_current_module.TraineeId
                                            orderby entry_absence.TraineeFirstName
                                            select new Entry_Absence_Model
                                            {
                                                TraineeId = entry_absence.TraineeId,
                                                TraineeFirstName = entry_absence.TraineeFirstName,
                                                TraineeLastName = entry_absence.TraineeLastName,
                                                AbsenceCount = entry_absence.AbsenceCount,
                                                InValideAbsences = entry_absence.InValideAbsences,
                                                Absences_In_Current_Module = absene_in_current_module.Absences_In_Current_Module,
                                                SeanceTrainingId = seanceTraining.Id,
                                                Absence = entry_absence.Absence

                                            };

            return Query_Entry_Absence_Model.ToList();
        }


        public Entry_Absence_Model Get_Trainee_Entry_Absence_Model(SeanceTraining seanceTraining, Int64 TraineeId)
        {
           

            Trainee trainee = (from t in this.UnitOfWork.context.Trainees
                               where t.Id == TraineeId
                               select t).FirstOrDefault();
            Int64 GroupId = trainee.GroupId;
            Int64 ModuleTrainingId = seanceTraining.SeancePlanning.Training.ModuleTraining.Id;


            // Trainee Absence in current TrainingYear ( the current TrainingYear is fixex by the group)
            var Query_Trainee_Absence = from absence in this.UnitOfWork.context.Absences
                                         where absence.TraineeId == TraineeId
                                         group absence by absence.TraineeId into Trainees_Absences
                                         select new
                                         {
                                             TraineeId = Trainees_Absences.Key,
                                             TraineeFirstName = trainee.FirstName,
                                             TraineeLastName = trainee.LastName,
                                             AbsenceCount = Trainees_Absences.Count(),
                                             InValideAbsences = Trainees_Absences.Where(a => a.Valide == false).ToList(),
                                             Absence = Trainees_Absences.Where(a => a.SeanceTrainingId == seanceTraining.Id).FirstOrDefault()
                                         };

            // Trainees_Absences In Current Module and TraineeYear
            var Query_Trainees_Absences_In_Current_Module = from absence in this.UnitOfWork.context.Absences
                                                                 where absence.SeanceTraining.SeancePlanning.Training.Group.Id == GroupId
                                                                    && absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Id == ModuleTrainingId
                                                                    && absence.TraineeId == TraineeId
                                                                 group absence by absence.TraineeId into Trainees_Absences
                                                                 select new
                                                                 {
                                                                     TraineeId = Trainees_Absences.Key,
                                                                     Absences_In_Current_Module = Trainees_Absences.ToList()
                                                                 };

            


            var Query_Entry_Absence_Model = from entry_absence in Query_Trainee_Absence
                                            join absene_in_current_module in Query_Trainees_Absences_In_Current_Module
                                            on entry_absence.TraineeId equals absene_in_current_module.TraineeId
                                            orderby entry_absence.TraineeFirstName
                                            select new Entry_Absence_Model
                                            {
                                                TraineeId = entry_absence.TraineeId,
                                                TraineeFirstName = entry_absence.TraineeFirstName,
                                                TraineeLastName = entry_absence.TraineeLastName,
                                                AbsenceCount = entry_absence.AbsenceCount,
                                                InValideAbsences = entry_absence.InValideAbsences,
                                                Absences_In_Current_Module = absene_in_current_module.Absences_In_Current_Module,
                                                SeanceTrainingId = seanceTraining.Id,
                                                Absence = entry_absence.Absence
                                            };

            Entry_Absence_Model entry_Absence_Model = Query_Entry_Absence_Model.FirstOrDefault();
            if (entry_Absence_Model != null) return entry_Absence_Model;
            else
            {
                entry_Absence_Model = new Entry_Absence_Model();
                entry_Absence_Model.TraineeId = trainee.Id;
                entry_Absence_Model.TraineeFirstName = trainee.FirstName;
                entry_Absence_Model.TraineeLastName = trainee.LastName;
                entry_Absence_Model.SeanceTrainingId = seanceTraining.Id;

                return entry_Absence_Model;
            }
        }

         
    }
}
