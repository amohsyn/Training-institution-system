using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Administrator : Employee
    {
        [Display(AutoGenerateField = false)]
        public virtual List<WorkGroup> Member_To_WorkGroups { get; set; }

    }
}
