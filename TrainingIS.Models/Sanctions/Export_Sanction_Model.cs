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
using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Entities.Resources.SanctionCategoryResources;
using TrainingIS.Entities.Resources.SanctionResources;
using TrainingIS.Entities.Resources.MeetingResources;
using GApp.Entities.Resources.PersonResources;
using TrainingIS.Entities.Resources.GroupResources;
using TrainingIS.Entities.Resources.SpecialtyResources;

namespace TrainingIS.Entities.ModelsViews
{
    [ExportView(typeof(Sanction))]
    public class Export_Sanction_Model : BaseModel
    {

        [Display(Name = "CEF", GroupName = "RegistrationForm", Order = 30, ResourceType = typeof(msg_Trainee))]
        [GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference", AutoGenerateFilter = true, isColumn = true)]
        public Trainee Trainee { set; get; }
 
        [Display(Name = "FirstName", GroupName = "CivilStatus", Order = 1, ResourceType = typeof(msg_Person))]
        public string FirstName { set; get; }

        [Display(Name = "LastName", GroupName = "CivilStatus", Order = 2, ResourceType = typeof(msg_Person))]
        public string LastName { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "Assignements", Order = 40, ResourceType = typeof(msg_Group))]
        public virtual Group Group { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "Assignements", ResourceType = typeof(msg_Specialty))]
        public virtual Specialty Specialty { set; get; }

        [Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
        [GAppDataTable(PropertyPath = "SanctionCategory", FilterBy = "SanctionCategory.Id", SearchBy = "SanctionCategory.Reference", OrderBy = "SanctionCategory.Reference", AutoGenerateFilter = true, isColumn = true)]
        public SanctionCategory SanctionCategory { set; get; }

        [Display(Name = "SanctionState", Order = 0, ResourceType = typeof(msg_Sanction))]
        [GAppDataTable(PropertyPath = "SanctionState", FilterBy = "SanctionState", SearchBy = "SanctionState", OrderBy = "SanctionState", AutoGenerateFilter = true, isColumn = true)]
        public SanctionStates SanctionState { set; get; }

        [Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
        [GAppDataTable(PropertyPath = "Meeting", FilterBy = "Meeting.Id", SearchBy = "Meeting.Reference", OrderBy = "Meeting.Reference", AutoGenerateFilter = true, isColumn = true)]
        public Meeting Meeting { set; get; }
    }
}
