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
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.MeetingResources;
using TrainingIS.Entities.Resources.WorkGroupResources;
using TrainingIS.Entities.Resources.Mission_Working_GroupResources;
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Base;

namespace TrainingIS.Entities.ModelsViews
{
    [DetailsView(typeof(Meeting))]
    [IndexView(typeof(Meeting))]
    public class Details_Meeting_Model : BaseModel
    {

        [Display(Name = "MeetingDate", GroupName = "Object", Order = 1, ResourceType = typeof(msg_Meeting))]
        [GAppDataTable(PropertyPath = "MeetingDate", FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate", AutoGenerateFilter = false, isColumn = true)]
        public DateTime MeetingDate { set; get; }

        [Display(Name = "WorkGroup", GroupName = "Object", Order = 2, ResourceType = typeof(msg_Meeting))]
        [GAppDataTable(PropertyPath = "WorkGroup", FilterBy = "WorkGroup.Id", SearchBy = "WorkGroup.Reference", OrderBy = "WorkGroup.Reference", AutoGenerateFilter = true, isColumn = true)]
        public WorkGroup WorkGroup { set; get; }

        [Display(Name = "Mission_Working_Group", GroupName = "Object", Order = 3, ResourceType = typeof(msg_Meeting))]
        [GAppDataTable(PropertyPath = "Mission_Working_Group", FilterBy = "Mission_Working_Group.Id", SearchBy = "Mission_Working_Group.Reference", OrderBy = "Mission_Working_Group.Reference", AutoGenerateFilter = true, isColumn = true)]
        public Mission_Working_Group Mission_Working_Group { set; get; }

        [GAppDataTable(isColumn = true)]
        [Display(Name = "Presences", GroupName = "Object", Order = 4, ResourceType = typeof(msg_Meeting))]
        public string Presences { set; get; }

 
    }
}
