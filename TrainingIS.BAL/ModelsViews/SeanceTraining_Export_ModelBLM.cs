using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial  class SeanceTraining_Export_ModelBLM
    {
        public override SeanceTraining_Export_Model ConverTo_SeanceTraining_Export_Model(SeanceTraining SeanceTraining)
        {
            SeanceTraining_Export_Model seanceTraining_Export_Model = base.ConverTo_SeanceTraining_Export_Model(SeanceTraining);

            // Training
            seanceTraining_Export_Model.Former = SeanceTraining.SeancePlanning.Training.Former;
            seanceTraining_Export_Model.ModuleTraining = SeanceTraining.SeancePlanning.Training.ModuleTraining;
            seanceTraining_Export_Model.Group = SeanceTraining.SeancePlanning.Training.Group;

            // SeancePlanning
            seanceTraining_Export_Model.Schedule = SeanceTraining.SeancePlanning.Schedule;
            seanceTraining_Export_Model.SeanceDay = SeanceTraining.SeancePlanning.SeanceDay;
            seanceTraining_Export_Model.SeanceNumber = SeanceTraining.SeancePlanning.SeanceNumber;
            seanceTraining_Export_Model.Classroom = SeanceTraining.SeancePlanning.Classroom;
            if (SeanceTraining.Absences != null && SeanceTraining.Absences.Count > 0)
                seanceTraining_Export_Model.Absences_Trainees = string.Join(" , ", SeanceTraining.Absences.Select(a => a.Trainee.ToString()).ToArray());

            return seanceTraining_Export_Model;
        }
    }
}
