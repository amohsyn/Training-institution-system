using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AttendanceStateResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class AttendanceState : BaseEntity
    {
        public override string ToString()
        {
            return this.Trainee?.GetFullName();
        }

        // Trainee
        [SearchBy("Trainee.FirstName")]
        [SearchBy("Trainee.LastName")]
        [Display(Name = "SingularName", GroupName ="Trainee", AutoGenerateFilter = true, ResourceType = typeof(msg_Trainee))]
        public virtual Trainee Trainee { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public long TraineeId { set; get; }

        // Note
        [Display(Name = "Valid_Note", GroupName = "Note", Order =1, ResourceType = typeof(msg_AttendanceState))]
        public float Valid_Note { set; get; }

        [Display(Name = "Invalid_Note", GroupName = "Note", Order = 2, ResourceType = typeof(msg_AttendanceState))]
        public float Invalid_Note { set; get; }

        // Sanction
        [Display(Name = "Valid_Sanction", GroupName = "Note", Order = 1, ResourceType = typeof(msg_AttendanceState))]
        public virtual Sanction Valid_Sanction { set; get; }

        [Display(Name = "Invalid_Sanction", GroupName = "Note", Order = 2, ResourceType = typeof(msg_AttendanceState))]
        public virtual Sanction Invalid_Sanction { set; get; }
 
    }
}
