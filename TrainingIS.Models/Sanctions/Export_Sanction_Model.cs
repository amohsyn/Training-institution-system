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
        public string CEF { set; get; }
 
        [Display(Name = "FirstName", GroupName = "CivilStatus", Order = 1, ResourceType = typeof(msg_Person))]
        public string FirstName { set; get; }

        [Display(Name = "LastName", GroupName = "CivilStatus", Order = 2, ResourceType = typeof(msg_Person))]
        public string LastName { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "Assignements", Order = 40, ResourceType = typeof(msg_Group))]
        public string Group_Code { set; get; }

        [Display(Name = "SingularName", AutoGenerateFilter = true, GroupName = "Assignements", ResourceType = typeof(msg_Specialty))]
        public string Specialty_Code { set; get; }

        [Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
        public string SanctionCategory_Code { set; get; }

        [Display(Name = "SanctionState", Order = 0, ResourceType = typeof(msg_Sanction))]
        public string SanctionState_Code { set; get; }

        [Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
        public string Meeting_Code { set; get; }
    }
}
