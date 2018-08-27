using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class AbsenceBLO
    {
        public override List<Absence> FindAll()
        {
            return base.FindAll().OrderByDescending(a => a.AbsenceDate).ToList();
        }
        public Absence Find_By_TraineeId_SeancePlanningId(long traineeId, long seancePlanningId, DateTime AbsenceDate)
        {
            Absence absence = this._UnitOfWork.context.Absences
                .Where(
                a => a.Trainee.Id == traineeId
                && a.SeancePlanning.Id == seancePlanningId
                && DbFunctions.TruncateTime(a.AbsenceDate) == DbFunctions.TruncateTime(AbsenceDate)
                ).FirstOrDefault();
            return absence;
        }

        public override int Save(Absence absence)
        {

            this.Throw_GAppException_if_not_valide(absence);
            int returned_value = base.Save(absence);
            StateOfAbseceBLO stateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork, this.GAppContext);
            stateOfAbseceBLO.Calculate_State_Of_Absence(absence.Trainee, absence.AbsenceDate, absence.SeancePlanning, true);
            return returned_value;
        }

        public override int Delete(Absence item)
        {
            this.Throw_GAppException_if_not_valide(item);

            Trainee trainee = item.Trainee;
            SeancePlanning seancePlanning = item.SeancePlanning;
            DateTime AbsenceDate = item.AbsenceDate;

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
            SeancePlanning seancePlanning = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(absence.SeancePlanningId);

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
    }
}
