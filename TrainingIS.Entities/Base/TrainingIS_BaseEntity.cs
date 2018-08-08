using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities.Base
{
    public class TrainingIS_BaseEntity : BaseEntity
    {
        [NotMapped]
        [Display(AutoGenerateField = false)]
        public string ToStringValue
        {
            get
            {
                return this.ToString();
            }
        }
            
    }
}
