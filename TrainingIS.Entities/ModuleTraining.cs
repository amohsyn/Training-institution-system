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
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
  
    [EntityMetataDataAttribute(isMale =true)]
    public class ModuleTraining : TrainingIS_BaseEntity
    {
        public override string ToString()
        {
            return string.Format("{0}-{1} : {2}",this.Specialty.Code,this.Code,this.Name) ;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}-{1}", this.Specialty.Code, this.Code);
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
