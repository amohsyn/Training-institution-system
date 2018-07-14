using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.SpecialtyResources;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingIS.Entities
{
  
    [EntityMetataDataAttribute(isMale =true)]
    public class ModuleTraining : BaseEntity
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

        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
        public long SpecialtyId { set; get; }


        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

    }
}
