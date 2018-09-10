using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities
{
    public class StatisticSelector : BaseEntity
    {
        public override string ToString()
        {
            return this.Name;
        }
        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }
    }
}
