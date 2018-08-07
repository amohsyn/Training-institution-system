using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.TrainingYearResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class TrainingYear : BaseEntity
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

        [Unique]
        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { set; get; }

        [Required]
        [Display(Name = "StartDate", ResourceType = typeof(msg_TrainingYear))]
        public DateTime? StartDate { set; get; }

        [Required]
        [Display(Name = "EndtDate", ResourceType = typeof(msg_TrainingYear))]
        public DateTime? EndtDate { set; get; }
    }
}
