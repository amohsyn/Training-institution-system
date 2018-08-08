using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.EntityPropertyShortcutResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class EntityPropertyShortcut : TrainingIS_BaseEntity
    {
        public override string ToString()
        {
            return this.Reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("{0}.{1}-{2}", this.EntityName,this.PropertyName,this.PropertyShortcutName);
            return reference;
        }

        [Required]
        [Display(Name = "EntityName", ResourceType = typeof(msg_EntityPropertyShortcut))]
        public string EntityName { get; set; }

        [Required]
        [Display(Name = "PropertyName", ResourceType = typeof(msg_EntityPropertyShortcut))]
        public string PropertyName { get; set; }

        [Required]
        [Display(Name = "PropertyShortcutName", ResourceType = typeof(msg_EntityPropertyShortcut))]
        public string PropertyShortcutName { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
