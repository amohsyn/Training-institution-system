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
using TrainingIS.Entities.enums;
using System.ComponentModel.DataAnnotations;

using TrainingIS.Entities.Resources.SeanceTrainingResources;
using TrainingIS.Entities.Resources.SeancePlanningResources;
using GApp.Entities.Resources.BaseEntity;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.ModuleTrainingResources;

namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(SeanceTraining))]
    public class Form_SeanceTraining_Model : BaseModel
    {
        [Required]
        [Display(Name = "SeanceDate", ResourceType = typeof(msg_SeanceTraining))]
        public DateTime SeanceDate { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Schedule))]
        public string ScheduleCode { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
        public long SeanceNumberId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Classroom))]
        public Int64 ClassroomId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public Int64 GroupId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
        public Int64 ModuleTrainingId { set; get; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
        public Int64 SeancePlanningId { set; get; }


        [Display(Name = "Contained", ResourceType = typeof(msg_SeanceTraining))]
        public String Contained { set; get; }

        public List<SeancePlanning> SeancePlannings { set; get; }

    }
}
