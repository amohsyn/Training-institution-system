using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.SanctionCategoryResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Sanction : BaseEntity
    {
        // Trainee
        [SearchBy("Trainee.FirstName")]
        [SearchBy("Trainee.LastName")]
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Trainee))]
        public virtual Trainee Trainee { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
        public long TraineeId { set; get; }

        // SanctionCategory
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SanctionCategory))]
        public virtual SanctionCategory SanctionCategory { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SanctionCategory))]
        public long SanctionCategoryId { set; get; }


        // Meeting
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SanctionCategory))]
        public virtual Meeting Meeting { set; get; }
        [Required]
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SanctionCategory))]
        public virtual Int64 MeetingId { set; get; }

        [Display(AutoGenerateField = false)]
        public virtual List<Absence> Absences { set; get; }



    }
}
