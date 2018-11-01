using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Entities.Resources.PersonResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;

namespace TrainingIS.Entities
{
 
    [EntityMetataData(isMale = true)]
    public class Administrator : Employee
    {
        [Display(Name = "Photo", GroupName = "Photo", Order = 1, ResourceType = typeof(msg_Person))]
        [GAppDataTable(AutoGenerateFilter = false, SearchBy = "Photo.Description", OrderBy = "Photo.UpdateDate", PropertyPath = "Photo")]
        public virtual GPicture Photo { set; get; }

        [Display(AutoGenerateField = false)]
        public virtual List<WorkGroup> Member_To_WorkGroups { get; set; }

    }
}
