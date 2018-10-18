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

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Sanction : BaseEntity
    {
 
        // SanctionCategory
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SanctionCategory))]
        public virtual SanctionCategory SanctionCategory { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_SanctionCategory))]
        public long SanctionCategoryId { set; get; }

    }
}
