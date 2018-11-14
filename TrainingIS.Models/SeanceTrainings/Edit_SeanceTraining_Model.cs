using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.Models.SeanceTrainings
{
    [EditView(typeof(SeanceTraining))]
    public class Edit_SeanceTraining_Model : Form_SeanceTraining_Model
    {

    }
}
