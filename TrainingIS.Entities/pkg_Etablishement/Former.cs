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
using GApp.Entities.Resources.PersonResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]

    public class Former : Employee
    {
        [Display(Name = "Photo", GroupName = "Photo", Order = 1, ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, SearchBy = "Photo.Description", OrderBy = "Photo.UpdateDate", PropertyPath = "Photo")]
        public virtual GPicture Photo { set; get; }

        // Metier
        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_FormerSpecialty))]
        public virtual FormerSpecialty FormerSpecialty { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_FormerSpecialty))]
        public long FormerSpecialtyId { set; get; }
        
        [Display(Name = "WeeklyHourlyMass", ResourceType = typeof(msg_Former))]
        public int WeeklyHourlyMass { set; get; }


        [Display(AutoGenerateField =false)]
        public virtual List<WorkGroup> Member_To_WorkGroups { get; set; }

    }
}
