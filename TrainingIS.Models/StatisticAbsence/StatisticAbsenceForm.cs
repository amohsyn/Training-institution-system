using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.StatisticResources;

namespace TrainingIS.Models.StatisticAbsence
{
    public class StatisticAbsenceForm
    {
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

    }
}
