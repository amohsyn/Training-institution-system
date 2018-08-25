using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class AbsenceBLO
    {
        public Absence Find_By_TraineeId_SeancePlanningId(long traineeId, long seancePlanningId)
        {
            Absence absence = this._UnitOfWork.context.Absences
                .Where(a => a.Trainee.Id == traineeId && a.SeancePlanning.Id == seancePlanningId).FirstOrDefault();
            return absence;
        }

        public override int Save(Absence absence)
        {
            int returned_value = base.Save(absence);
            StateOfAbseceBLO stateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork, this.GAppContext);
            stateOfAbseceBLO.Calculate_State_Of_Absence(absence.Trainee,absence.AbsenceDate,absence.SeancePlanning,true);
            return returned_value;
        }

        public override int Delete(Absence item)
        {

            Trainee trainee = item.Trainee;
            SeancePlanning seancePlanning = item.SeancePlanning;
            DateTime AbsenceDate = item.AbsenceDate;

            int returned_value = base.Delete(item);
            if(returned_value == 1)
            {
                StateOfAbseceBLO stateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork, this.GAppContext);
                stateOfAbseceBLO.Calculate_State_Of_Absence(trainee, AbsenceDate, seancePlanning, false);
            }

            return returned_value;
        }
    }
}
