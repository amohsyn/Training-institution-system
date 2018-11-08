using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.SeanceDayResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Resources.TrainingResources;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.ClassroomResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class SeancePlanning : BaseEntity
    {
        public override string ToString()
        {
            string group = this.Training?.Group?.Code;
            string seanceNumber = this.SeanceNumber?.ToString();
            string module = this.Training?.ModuleTraining?.Code;
            string former = this.Training?.Former?.ToString();
            return string.Format("{0}-{1} [{2}, {3}]", group, seanceNumber, module, former);
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}-{2}-{3}", 
                this.Schedule.Reference,
                this.Training.Reference,
                this.SeanceNumber.Reference,
                this.SeanceDay.Reference);
            return reference;
        }

        // Schedule
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Schedule))]
        public virtual Schedule Schedule { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Schedule))]
        public long ScheduleId { set; get; }

        // Training
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Training))]
        public virtual Training Training { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
        public long TrainingId { set; get; }

        // SeanceDay
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SeanceDay))]
        public virtual SeanceDay SeanceDay { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
        public long SeanceDayId { set; get; }

        // SeanceNumber
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SeanceNumber))]
        public virtual SeanceNumber SeanceNumber { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
        public long SeanceNumberId { set; get; }


        // Classroom
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Classroom))]
        public virtual Classroom Classroom { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Classroom))]
        public long ClassroomId { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }


        [Display(AutoGenerateField = false)]
        public virtual List<Absence> Absences { set; get; }
    }
}
