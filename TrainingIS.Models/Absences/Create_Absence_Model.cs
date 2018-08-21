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
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.AbsenceResources;
using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;

namespace TrainingIS.Models.Absences
{
    [CreateView(typeof(Absence))]
    [EditView(typeof(Absence))]
    public class Create_Absence_Model : BaseModel
    {
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public Int64 TraineeId { set; get; }

        [Required]
        [Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
        public Boolean isHaveAuthorization { set; get; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
        public Int64 SeanceTrainingId { set; get; }

        [Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
        public String FormerComment { set; get; }

        [Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
        public String TraineeComment { set; get; }

        [Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
        public String SupervisorComment { set; get; }
    }
}
