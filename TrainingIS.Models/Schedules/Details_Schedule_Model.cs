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
    public class Details_Schedule_Model : BaseModel
    {
        [Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "TrainingYear.Id", SearchBy = "TrainingYear.Reference", OrderBy = "TrainingYear.Reference", PropertyPath = "TrainingYear")]
        public TrainingYear TrainingYear { set; get; }

        [Display(Name = "StartDate", ResourceType = typeof(msg_Schedule))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate", PropertyPath = "StartDate")]
        public DateTime StartDate { set; get; }

        [Display(Name = "EndtDate", ResourceType = typeof(msg_Schedule))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate", PropertyPath = "EndtDate")]
        public DateTime EndtDate { set; get; }

        [Display(Name = "Reference", ResourceType = typeof(msg_Schedule))]
        public String Reference { set; get; }

    }
}
