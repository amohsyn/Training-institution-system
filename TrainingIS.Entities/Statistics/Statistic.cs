using GApp.Entities;
using GApp.Entities.Resources.AppResources;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.StatisticResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class Statistic : BaseEntity
    {
        public Statistic()
        {
            StatisticAbsenceValues = new List<StatisticAbsenceValue>();
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

        // Group
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
        public long GroupId { set; get; }


        public List<StatisticAbsenceValue> StatisticAbsenceValues { set; get; }

        public List<String> StatisticSelectors { set; get; }
        public DataTable DataTable { get; set; }
    }
}
