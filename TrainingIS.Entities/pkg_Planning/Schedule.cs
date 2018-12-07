using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Base;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.ScheduleResources;
using TrainingIS.Entities.Resources.TrainingYearResources;
using GApp.Entities;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class Schedule : BaseEntity
    {
        public override string ToString()
        {
            string reference = string.Format("[{0}]",
                this.StartDate.ToShortDateString()
                );
            return reference;
        }
        public override string CalculateReference()
        {
            string reference = string.Format("[{0}]",
                this.StartDate.ToString(@"dd/MM/yyyy", CultureInfo.InvariantCulture)
                );
            return reference;
        }

        // TrainingYear
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public virtual TrainingYear TrainingYear { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public long TrainingYearId { set; get; }

        [Required]
        [Display(Name = "StartDate", ResourceType = typeof(msg_Schedule))]
        public DateTime StartDate { set; get; }

        [Required]
        [Display(Name = "EndtDate", ResourceType = typeof(msg_Schedule))]
        public DateTime EndtDate { set; get; }


        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }

        [Display(AutoGenerateField = false)]
        public virtual List<SeancePlanning> SeancePlannings { set; get; }
    }
}
