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
    public class Mission_Working_Group : BaseEntity
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
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }


        [Display(Name = "DecisionAuthority", ResourceType = typeof(msg_SanctionCategory))]
        public DecisionsAuthorities DecisionAuthority { get; set; }


        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
