using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
    public enum OperationWorkTypes
    {
        Import,
        AddEntity,
        UpdateEntity,
        DeleteEntity
    }
    [EntityMetataData(isMale = false)]
    public class LogWork : BaseEntity , ISystemEntity
    {
 
        [Required]
        public string UserId { get; set; }

        [Required]
        public OperationWorkTypes OperationWorkType { get; set; }


        public string OperationReference { get; set; }

     
        public string EntityType { get; set; }

        public string Description { set; get; }

        
    }
}
