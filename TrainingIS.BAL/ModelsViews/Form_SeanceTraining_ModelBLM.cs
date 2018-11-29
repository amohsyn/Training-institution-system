using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Form_SeanceTraining_ModelBLM
    {
        public override void ConverTo_Form_SeanceTraining_Model(Form_SeanceTraining_Model Form_SeanceTraining_Model, SeanceTraining SeanceTraining)
        {
            base.ConverTo_Form_SeanceTraining_Model(Form_SeanceTraining_Model, SeanceTraining);

            Former former = new FormerBLO(this.UnitOfWork, this.GAppContext).Get_Current_Former();
            if (SeanceTraining.SeanceDate != null)

                this.Fill(Convert.ToDateTime(SeanceTraining.SeanceDate), former, SeanceTraining.SeancePlanning, Form_SeanceTraining_Model);
        }

        public void Fill(DateTime seanceDate, Former former, SeancePlanning current_seancePlanning, Form_SeanceTraining_Model form_SeanceTraining_Model)
        {

            SeancePlanningBLO seancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
            List<SeancePlanning> seancePlannings = seancePlanningBLO.GetSeancesPlanning(seanceDate, former);

            // ScheduleCode
            form_SeanceTraining_Model.ScheduleCode = new ScheduleBLO(this.UnitOfWork, this.GAppContext).GetExistantSchedule(seanceDate)?.ToString();

            // Current_seanceNumber
            SeanceNumber Current_seanceNumber = null;
            if (current_seancePlanning == null)
            {
                Current_seanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).GetSeanceNumber(DateTime.Now.TimeOfDay);
            }
            else
            {
                if (current_seancePlanning.SeanceNumber != null)
                    Current_seanceNumber = current_seancePlanning.SeanceNumber;
                else
                {
                    Current_seanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByID(current_seancePlanning.SeanceNumberId);
                }
            }

            // current_seancePlanning
            if (current_seancePlanning == null && Current_seanceNumber != null)
            {
                current_seancePlanning = seancePlannings.Where(p => p.SeanceNumberId == Current_seanceNumber.Id).FirstOrDefault();
            }

            form_SeanceTraining_Model.SeanceDate = seanceDate;
            form_SeanceTraining_Model.SeancePlannings = seancePlannings;
            if (current_seancePlanning != null)
            {
                form_SeanceTraining_Model.SeanceNumberId = current_seancePlanning.SeanceNumber.Id;
                form_SeanceTraining_Model.GroupId = current_seancePlanning.Training.Group.Id;
                form_SeanceTraining_Model.ClassroomId = current_seancePlanning.Classroom.Id;
                form_SeanceTraining_Model.ModuleTrainingId = current_seancePlanning.Training.ModuleTraining.Id;
                form_SeanceTraining_Model.SeancePlanningId = current_seancePlanning.Id;

            }
        }
    }
}
