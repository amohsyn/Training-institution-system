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
using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.Resources.SeancePlanningResources;

namespace TrainingIS.Models.Absences
{
    public class Create_Group_Absences_Model
    {
        [Required]
        [Display(Name = "AbsenceDate", ResourceType = typeof(msg_Absence))]
        public DateTime AbsenceDate { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Schedule))]
        public string ScheduleCode { set; get; }

        // SeancePlanningId
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
        public Int64 SeancePlanningId { set; get; }

        public List<SeancePlanning> SeancePlannings { set; get; }


        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
        public SeanceNumber SeanceNumber;
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
        public long SeanceNumberId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_Classroom))]
        public Int64 ClassroomId { set; get; }

        // Group
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public Int64 GroupId { set; get; }
 

        [Display(Name = "SingularName", ResourceType = typeof(msg_Former))]
        public Int64 FormerId { set; get; }

        [Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
        public Int64 ModuleTrainingId { set; get; }
    }
}
