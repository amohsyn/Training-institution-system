using GApp.Models.DataAnnotations;
using GApp.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Entities.Resources.MetierResources;
using TrainingIS.Entities.Resources.FormerSpecialtyResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Former : Employee
    {
 
        // Metier
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_FormerSpecialty))]
        public virtual FormerSpecialty FormerSpecialty { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
        public long FormerSpecialtyId { set; get; }
        
        [Display(Name = "WeeklyHourlyMass", ResourceType = typeof(msg_Former))]
        public int WeeklyHourlyMass { set; get; }

    }
}
