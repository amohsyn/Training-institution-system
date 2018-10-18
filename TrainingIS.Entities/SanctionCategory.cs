using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.SanctionCategoryResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class SanctionCategory : BaseEntity
    {
        public override string ToString()
        {
            return this.Code;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}", this.Code);
            return reference;
        }



        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "DecisionAuthority", ResourceType = typeof(msg_SanctionCategory))]
        public DecisionsAuthorities DecisionAuthority { get; set; }

        [Display(Name = "WorkflowOrder", ResourceType = typeof(msg_SanctionCategory))]
        public int WorkflowOrder { set; get; }

        [Display(Name = "Number_Of_Days_Of_Exclusion", ResourceType = typeof(msg_SanctionCategory))]
        public int Number_Of_Days_Of_Exclusion { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
