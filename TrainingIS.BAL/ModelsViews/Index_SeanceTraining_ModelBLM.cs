using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.SeanceTrainings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Index_SeanceTraining_ModelBLM
    {
        public override Index_SeanceTraining_Model ConverTo_Index_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {
            var Index_SeanceTraining = base.ConverTo_Index_SeanceTraining_Model(SeanceTraining);
            Index_SeanceTraining.Absences_Count = SeanceTraining.Absences.Count();
            Index_SeanceTraining.Absences_Description = string.Join(", ",SeanceTraining.Absences.Select(a => a.Trainee.ToString()).ToArray());
            Index_SeanceTraining.Group = SeanceTraining.SeancePlanning.Training.Group;
            Index_SeanceTraining.ModuleTraining = SeanceTraining.SeancePlanning.Training.ModuleTraining;
            Index_SeanceTraining.SeanceNumber = SeanceTraining.SeancePlanning.SeanceNumber;
            return Index_SeanceTraining;
        }
    }
}
