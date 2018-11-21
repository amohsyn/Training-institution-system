using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.MeetingResources;
using TrainingIS.Entities.Resources.SanctionCategoryResources;
using TrainingIS.Entities.Resources.SanctionResources;
using TrainingIS.Entities.Resources.TraineeResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Sanction : BaseEntity
    {

        public override string ToString()
        {
            return string.Format("{0}-{1}", this.Trainee?.GetFullName(), this.SanctionCategory?.Name);
        }

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

        [Display(Name = "SanctionState", AutoGenerateFilter = true, ResourceType = typeof(msg_Sanction))]
        public SanctionStates SanctionState { set; get; }
 
        // Meeting
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Meeting))]
        public virtual Meeting Meeting { set; get; }

        // the property MeetingId added to Support Generator that not generated MenyToOne without Null
        // we must Update manuelly the ModelBLM to Update MeetingId
        [NotMapped]
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Meeting))]
        public virtual Int64 MeetingId { set; get; }

        

        [Display(AutoGenerateField = false)]
        public virtual List<Absence> Absences { set; get; }



    }
}
