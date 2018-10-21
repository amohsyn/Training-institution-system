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
using GApp.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.WorkGroupResources;
using TrainingIS.Entities.Resources.Mission_Working_GroupResources;
using TrainingIS.Entities;

namespace TrainingIS.Models.WorkGroups
{
    /// <summary>
    /// Riason to Add a Specific Form_Model : 
    /// the Default_WorkGroup_Model not support Foreign Key Witout Foreign Key Id
    /// </summary>
    [EditView(typeof(WorkGroup))]
    [CreateView(typeof(WorkGroup))]
    public class Form_WorkGroup_Model : BaseModel
    {
       

   


        [Required]
        [Display(Name = "Name", GroupName = "Designation", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name", PropertyPath = "Name")]
        public String Name { set; get; }

        [Required]
        [Display(Name = "Code", GroupName = "Designation", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code", PropertyPath = "Code")]
        public String Code { set; get; }

        [Display(Name = "Description", GroupName = "Designation", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description", PropertyPath = "Description")]
        public String Description { set; get; }

        #region President
        // 
        // The present can be Former, Administrator or Trainee
        //
        [Display(Name = "President_Former", GroupName = "President", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public Int64? President_FormerId { set; get; }

        [Display(Name = "President_Trainee", GroupName = "President", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public Int64? President_TraineeId { set; get; }

        [Display(Name = "President_Administrator", GroupName = "President", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public Int64? President_AdministratorId { set; get; }
        #endregion

        #region VicePresident
        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [Display(Name = "VicePresident_Former", GroupName = "VicePresident", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public Int64? VicePresident_FormerId { set; get; }
        [Display(Name = "VicePresident_Trainee", GroupName = "VicePresident", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public Int64? VicePresident_TraineeId { set; get; }
        [Display(Name = "VicePresident_Administrator", GroupName = "VicePresident", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public Int64? VicePresident_AdministratorId { set; get; }
        #endregion

        #region Protractor
        // 
        // The VicePresident present can be Former, Administrator or Trainee
        //
        [Display(Name = "Protractor_Former", GroupName = "Protractor", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        public Int64? Protractor_FormerId { set; get; }

        [Display(Name = "Protractor_Administrator", GroupName = "Protractor", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        public Int64? Protractor_AdministratorId { set; get; }

        [Display(Name = "Protractor_Trainee", GroupName = "Protractor", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        public Int64? Protractor_TraineeId { set; get; }
        #endregion




        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "MemebersFormers", GroupName = "Members", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersFormers", SearchBy = "MemebersFormers", OrderBy = "MemebersFormers", PropertyPath = "MemebersFormers")]
        public List<String> Selected_MemebersFormers { set; get; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "MemebersAdministrators", GroupName = "Members", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersAdministrators", SearchBy = "MemebersAdministrators", OrderBy = "MemebersAdministrators", PropertyPath = "MemebersAdministrators")]
        public List<String> Selected_MemebersAdministrators { set; get; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "MemebersTrainees", GroupName = "Members", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersTrainees", SearchBy = "MemebersTrainees", OrderBy = "MemebersTrainees", PropertyPath = "MemebersTrainees")]
        public List<String> Selected_MemebersTrainees { set; get; }

        [Display(Name = "GuestFormers", GroupName = "Guests", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestFormers", SearchBy = "GuestFormers", OrderBy = "GuestFormers", PropertyPath = "GuestFormers")]
        public Boolean GuestFormers { set; get; }

        [Display(Name = "GuestTrainees", GroupName = "Guests", Order = 2, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestTrainees", SearchBy = "GuestTrainees", OrderBy = "GuestTrainees", PropertyPath = "GuestTrainees")]
        public Boolean GuestTrainees { set; get; }

        [Display(Name = "GuestAdministrator", GroupName = "Guests", Order = 3, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestAdministrator", SearchBy = "GuestAdministrator", OrderBy = "GuestAdministrator", PropertyPath = "GuestAdministrator")]
        public Boolean GuestAdministrator { set; get; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Mission_Working_Group))]
        [Display(Name = "Missions", GroupName = "Missions", Order = 1, ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Mission_Working_Groups", SearchBy = "Mission_Working_Groups", OrderBy = "Mission_Working_Groups", PropertyPath = "Mission_Working_Groups")]
        public List<String> Selected_Mission_Working_Groups { set; get; }
    }
}
