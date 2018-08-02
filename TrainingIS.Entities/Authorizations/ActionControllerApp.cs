using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.ControllerAppResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class ActionControllerApp : BaseEntity
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

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }


        [Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
        public virtual ControllerApp ControllerApp { set; get; }

        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
        public virtual Int64 ControllerAppId { set; get; }


    }
}
