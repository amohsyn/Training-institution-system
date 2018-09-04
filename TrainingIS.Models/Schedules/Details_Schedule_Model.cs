using GApp.Core.Entities.ModelsViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.Resources.ScheduleResources;
using GApp.Entities.Resources.AppResources;

namespace TrainingIS.Entities.ModelsViews
{
    [DetailsView(typeof(Schedule))]
    [IndexView(typeof(Schedule))]
    public class Details_Schedule_Model : BaseModel
    {
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        public TrainingYear TrainingYear { set; get; }

        [Required]
        [Display(Name = "StartDate", ResourceType = typeof(msg_Schedule))]
        public DateTime StartDate { set; get; }

        [Required]
        [Display(Name = "EndtDate", ResourceType = typeof(msg_Schedule))]
        public DateTime EndtDate { set; get; }

        [Display(Name = "Reference", ResourceType = typeof(msg_Schedule))]
        public String Reference { set; get; }

    }
}
