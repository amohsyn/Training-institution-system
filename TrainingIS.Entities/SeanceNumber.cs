using GApp.Models.DataAnnotations;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.SeanceNumberResources;
using TrainingIS.Entities.Base;
namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class SeanceNumber : BaseEntity
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
        [Required]
        [Unique]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Required]
        [Display(Name = "StartTime", ResourceType = typeof(msg_SeanceNumber))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Display(Name = "EndTime", ResourceType = typeof(msg_SeanceNumber))]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

    }
}
