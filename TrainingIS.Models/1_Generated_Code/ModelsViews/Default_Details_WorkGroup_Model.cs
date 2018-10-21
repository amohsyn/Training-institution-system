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
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(WorkGroup))]
	[IndexView(typeof(WorkGroup))]
    public class Default_Details_WorkGroup_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "President_Former.Id", SearchBy = "President_Former.Reference", OrderBy = "President_Former.Reference",  PropertyPath = "President_Former")]
		public Former President_Former  {set; get;}  
   
		[Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "President_Trainee.Id", SearchBy = "President_Trainee.Reference", OrderBy = "President_Trainee.Reference",  PropertyPath = "President_Trainee")]
		public Trainee President_Trainee  {set; get;}  
   
		[Display(Name = "President", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "President_Administrator.Id", SearchBy = "President_Administrator.Reference", OrderBy = "President_Administrator.Reference",  PropertyPath = "President_Administrator")]
		public Administrator President_Administrator  {set; get;}  
   
		[Display(Name = "VicePresident", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "VicePresident_Former.Id", SearchBy = "VicePresident_Former.Reference", OrderBy = "VicePresident_Former.Reference",  PropertyPath = "VicePresident_Former")]
		public Former VicePresident_Former  {set; get;}  
   
		[Display(Name = "VicePresident", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "VicePresident_Trainee.Id", SearchBy = "VicePresident_Trainee.Reference", OrderBy = "VicePresident_Trainee.Reference",  PropertyPath = "VicePresident_Trainee")]
		public Trainee VicePresident_Trainee  {set; get;}  
   
		[Display(Name = "VicePresident", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "VicePresident_Administrator.Id", SearchBy = "VicePresident_Administrator.Reference", OrderBy = "VicePresident_Administrator.Reference",  PropertyPath = "VicePresident_Administrator")]
		public Administrator VicePresident_Administrator  {set; get;}  
   
		[Display(Name = "Protractor", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Protractor_Former.Id", SearchBy = "Protractor_Former.Reference", OrderBy = "Protractor_Former.Reference",  PropertyPath = "Protractor_Former")]
		public Former Protractor_Former  {set; get;}  
   
		[Display(Name = "Protractor", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Protractor_Trainee.Id", SearchBy = "Protractor_Trainee.Reference", OrderBy = "Protractor_Trainee.Reference",  PropertyPath = "Protractor_Trainee")]
		public Trainee Protractor_Trainee  {set; get;}  
   
		[Display(Name = "Protractor", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Protractor_Administrator.Id", SearchBy = "Protractor_Administrator.Reference", OrderBy = "Protractor_Administrator.Reference",  PropertyPath = "Protractor_Administrator")]
		public Administrator Protractor_Administrator  {set; get;}  
   
		[Display(Name = "MemebersFormers", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersFormers", SearchBy = "MemebersFormers", OrderBy = "MemebersFormers",  PropertyPath = "MemebersFormers")]
		public List<Former> MemebersFormers  {set; get;}  
   
		[Display(Name = "MemebersAdministrators", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersAdministrators", SearchBy = "MemebersAdministrators", OrderBy = "MemebersAdministrators",  PropertyPath = "MemebersAdministrators")]
		public List<Administrator> MemebersAdministrators  {set; get;}  
   
		[Display(Name = "MemebersTrainees", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MemebersTrainees", SearchBy = "MemebersTrainees", OrderBy = "MemebersTrainees",  PropertyPath = "MemebersTrainees")]
		public List<Trainee> MemebersTrainees  {set; get;}  
   
		[Display(Name = "GuestFormers", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestFormers", SearchBy = "GuestFormers", OrderBy = "GuestFormers",  PropertyPath = "GuestFormers")]
		public Boolean GuestFormers  {set; get;}  
   
		[Display(Name = "GuestTrainees", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestTrainees", SearchBy = "GuestTrainees", OrderBy = "GuestTrainees",  PropertyPath = "GuestTrainees")]
		public Boolean GuestTrainees  {set; get;}  
   
		[Display(Name = "GuestAdministrator", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "GuestAdministrator", SearchBy = "GuestAdministrator", OrderBy = "GuestAdministrator",  PropertyPath = "GuestAdministrator")]
		public Boolean GuestAdministrator  {set; get;}  
   
		[Display(Name = "PluralName", ResourceType = typeof(msg_Mission_Working_Group))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Mission_Working_Groups", SearchBy = "Mission_Working_Groups", OrderBy = "Mission_Working_Groups",  PropertyPath = "Mission_Working_Groups")]
		public List<Mission_Working_Group> Mission_Working_Groups  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
