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
    
    [EditView(typeof(WorkGroup))]
    [CreateView(typeof(WorkGroup))]
    public class Form_WorkGroup_Model : BaseModel
    {
        [Required]
        [Display(Name = "Name", ResourceType = typeof(msg_app))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name", PropertyPath = "Name")]
        public String Name { set; get; }

        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code", PropertyPath = "Code")]
        public String Code { set; get; }

        // 
        // The present can be Former, Administrator or Trainee
        //
        [Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
        public Int64 President_FormerId { set; get; }

        [Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
        public Int64 President_TraineeId { set; get; }

        [Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
        public Int64 President_AdministratorId { set; get; }


        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Former))]
        [Display(Name = "MemebersFormers", ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersFormers", SearchBy = "MemebersFormers", OrderBy = "MemebersFormers", PropertyPath = "MemebersFormers")]
        public List<String> Selected_MemebersFormers { set; get; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Administrator))]
        [Display(Name = "MemebersAdministrators", ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersAdministrators", SearchBy = "MemebersAdministrators", OrderBy = "MemebersAdministrators", PropertyPath = "MemebersAdministrators")]
        public List<String> Selected_MemebersAdministrators { set; get; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Trainee))]
        [Display(Name = "MemebersTrainees", ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersTrainees", SearchBy = "MemebersTrainees", OrderBy = "MemebersTrainees", PropertyPath = "MemebersTrainees")]
        public List<String> Selected_MemebersTrainees { set; get; }

        [Display(Name = "GuestFormers", ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestFormers", SearchBy = "GuestFormers", OrderBy = "GuestFormers", PropertyPath = "GuestFormers")]
        public Boolean GuestFormers { set; get; }

        [Display(Name = "GuestTrainees", ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestTrainees", SearchBy = "GuestTrainees", OrderBy = "GuestTrainees", PropertyPath = "GuestTrainees")]
        public Boolean GuestTrainees { set; get; }

        [Display(Name = "GuestAdministrator", ResourceType = typeof(msg_WorkGroup))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestAdministrator", SearchBy = "GuestAdministrator", OrderBy = "GuestAdministrator", PropertyPath = "GuestAdministrator")]
        public Boolean GuestAdministrator { set; get; }

        [Many(userInterfaces = UserInterfaces.MultiSelect, TypeOfEntity = typeof(Mission_Working_Group))]
        [Display(Name = "PluralName", ResourceType = typeof(msg_Mission_Working_Group))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Mission_Working_Groups", SearchBy = "Mission_Working_Groups", OrderBy = "Mission_Working_Groups", PropertyPath = "Mission_Working_Groups")]
        public List<String> Selected_Mission_Working_Groups { set; get; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        [GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description", PropertyPath = "Description")]
        public String Description { set; get; }

    }
}
