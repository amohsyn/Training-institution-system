using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.SeanceTrainings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Create_SeanceTraining_ModelBLM
    {
        public Create_SeanceTraining_Model CreateNew(DateTime seanceDate, Former former)
        {
            Create_SeanceTraining_Model create_SeanceTraining_Model = base.CreateNew();
            this.Fill(seanceDate, former, create_SeanceTraining_Model);
            return create_SeanceTraining_Model;
        }

        public override Create_SeanceTraining_Model ConverTo_Create_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {
            Create_SeanceTraining_Model create_SeanceTraining_Model = base.ConverTo_Create_SeanceTraining_Model(SeanceTraining);
            if (SeanceTraining.SeanceDate != null && SeanceTraining.SeancePlanning!=null)
                this.Fill(Convert.ToDateTime(SeanceTraining.SeanceDate), SeanceTraining.SeancePlanning.Training.Former, create_SeanceTraining_Model);
            return create_SeanceTraining_Model;
        }

        private void Fill(DateTime seanceDate, Former former, Create_SeanceTraining_Model create_SeanceTraining_Model)
        {
            SeancePlanningBLO seancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
            List<SeancePlanning> seancePlannings = seancePlanningBLO.GetSeancesPlanning(seanceDate, former);


            SeanceNumber Current_seanceNumber = new SeanceNumberBLO(this.UnitOfWork, this.GAppContext).GetSeanceNumber(DateTime.Now.TimeOfDay);
            SeancePlanning current_seancePlanning = null;
            if (Current_seanceNumber != null)
            {
                current_seancePlanning = seancePlannings.Where(p => p.SeanceNumberId == Current_seanceNumber.Id).FirstOrDefault();
            }

            create_SeanceTraining_Model.SeanceDate = seanceDate;
            create_SeanceTraining_Model.SeancePlannings = seancePlannings;
            if (current_seancePlanning != null)
            {
                create_SeanceTraining_Model.SeanceNumberId = current_seancePlanning.SeanceNumber.Id;
                create_SeanceTraining_Model.GroupId = current_seancePlanning.Training.Group.Id;
                create_SeanceTraining_Model.ClassroomId = current_seancePlanning.Classroom.Id;
                create_SeanceTraining_Model.ModuleTrainingId = current_seancePlanning.Training.ModuleTraining.Id;
                create_SeanceTraining_Model.ScheduleCode = current_seancePlanning.Schedule.ToString();
            }
        }
    }
}
