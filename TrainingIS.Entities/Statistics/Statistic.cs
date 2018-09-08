using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.StatisticResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Statistic : BaseEntity
    {
        public Statistic()
        {
            this.Categories = new List<StatisticCategory>();
        }

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
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

        [Required]
        [Display(Name = "StartDate", ResourceType = typeof(msg_Statistic))]
        public DateTime StartDate { set; get; }

        [Required]
        [Display(Name = "EndDate", ResourceType = typeof(msg_Statistic))]
        public DateTime EndDate { set; get; }

        public List<StatisticCategory> Categories { set; get; }

    }
}
