using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL
{
    public partial class AbsenceBLO
    {
        public override List<Absence> FindAll()
        {
            var query = from absence in this._UnitOfWork.context.Absences
                        orderby absence.UpdateDate descending
                        select absence;

            return query.ToList();
        }
        public Absence Find_By_TraineeId_SeancePlanningId(long traineeId, long seancePlanningId, DateTime AbsenceDate)
        {
            Absence absence = this._UnitOfWork.context.Absences
                .Where(
                a => a.Trainee.Id == traineeId
                && a.SeanceTraining.SeancePlanning.Id == seancePlanningId
                && DbFunctions.TruncateTime(a.AbsenceDate) == DbFunctions.TruncateTime(AbsenceDate)
                ).FirstOrDefault();
            return absence;
        }
        public Absence Find_By_TraineeId_SeanceTraining(long traineeId, long seanceTrainingId)
        {
            Absence absence = this._UnitOfWork.context.Absences
                .Where(  a => a.Trainee.Id == traineeId && a.SeanceTraining.Id == seanceTrainingId)
                .FirstOrDefault();
            return absence;
        }

        public override int Save(Absence absence)
        {
            bool isImportProcess = GAppContext.Session.ContainsKey(ImportService.IMPORT_PROCESS_KEY) ? true : false;
            bool isInsert = true;
            if (absence.Id != 0)
                isInsert = false;

            if (!isImportProcess)
            {
                this.Throw_GAppException_if_not_valide(absence);
            }

               
            int returned_value = base.Save(absence);

            // if Insert
            if (isInsert)
            {
                if (!isImportProcess)
                {
                    StateOfAbseceBLO stateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork, this.GAppContext);
                    stateOfAbseceBLO.Calculate_State_Of_Absence(absence.Trainee, absence.AbsenceDate, absence.SeanceTraining.SeancePlanning, true);

                }
            }
            else
            {
                // if Update

            }




            return returned_value;
        }

        public override int Delete(Absence item)
        {
            this.Throw_GAppException_if_not_valide(item);

            Trainee trainee = item.Trainee;
            
            DateTime AbsenceDate = item.AbsenceDate;
            SeancePlanning seancePlanning = item.SeanceTraining.SeancePlanning;
            int returned_value = base.Delete(item);
            if (returned_value == 1)
            {
                StateOfAbseceBLO stateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork, this.GAppContext);
                stateOfAbseceBLO.Calculate_State_Of_Absence(trainee, AbsenceDate, seancePlanning, false);
            }

            return returned_value;
        }

        private void Throw_GAppException_if_not_valide(Absence absence)
        {
            if (absence.SeanceTrainingId == 0 && absence.SeanceTraining != null) absence.SeanceTrainingId = absence.SeanceTraining.Id;
            if (absence.TraineeId == 0 && absence.Trainee != null) absence.TraineeId = absence.Trainee.Id;


            SeancePlanning seancePlanning = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(absence.SeanceTraining.SeancePlanning.Id);

            // is the trainee has this SeancePlanning
            if (!seancePlanning.Training.Group.Trainees.Select(t => t.Id).Contains(absence.TraineeId))
            {
                string msg_ex = string.Format("The Trainee '{0}' not exsit in the group {1}", absence.TraineeId, seancePlanning.Training.Group.ToString());
                throw new GAppException(msg_ex);
            }

            // if user is former
            Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former();
            if (former != null)
            {
                // is the seance Planing is for the current Former
                if (seancePlanning.Training.Former.Id != former.Id)
                {
                    string msg_ex = string.Format("The former {0} not have the SeancePlaning {1}", former.ToString(), seancePlanning.ToString());
                    throw new GAppException(msg_ex);
                }
            }
        }


        public void Validate_All_Absences()
        {
            string sql_query  = "Update Absences set Valide = 'true'";
            this._UnitOfWork.context.Database.ExecuteSqlCommand(sql_query);
        }
    }
}
