using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Category_WarningTrainee : BaseEntity
    {
        public override string ToString()
        {
            string reference = string.Format("{0}", this.Name);
            return reference;
        }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }
    }
}
