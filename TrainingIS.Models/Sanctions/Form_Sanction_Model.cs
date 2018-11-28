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
using System.ComponentModel.DataAnnotations;

using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.SanctionCategoryResources;
using TrainingIS.Entities.Resources.SanctionResources;
using TrainingIS.Entities.Resources.MeetingResources;
using GApp.Entities.Resources.BaseEntity;

namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Sanction))]
    public class Form_Sanction_Model : BaseModel
    {
        [Required]
        [Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
        [GAppDataTable(PropertyPath = "TraineeId", FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId", AutoGenerateFilter = false, isColumn = true)]
        public Int64 TraineeId { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_Trainee))]
        public Trainee Trainee { set; get; }

        [Required]
        [Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
        [GAppDataTable(PropertyPath = "SanctionCategoryId", FilterBy = "SanctionCategoryId", SearchBy = "SanctionCategoryId", OrderBy = "SanctionCategoryId", AutoGenerateFilter = false, isColumn = true)]
        public Int64 SanctionCategoryId { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, ResourceType = typeof(msg_SanctionCategory))]
        public virtual SanctionCategory SanctionCategory { set; get; }

        [Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
        [GAppDataTable(PropertyPath = "MeetingId", FilterBy = "MeetingId", SearchBy = "MeetingId", OrderBy = "MeetingId", AutoGenerateFilter = true, isColumn = true)]
        public Int64 MeetingId { set; get; }

        [Unique]
        [Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
        [GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference", AutoGenerateFilter = false, isColumn = false)]
        public String Reference { set; get; }

    }
}
