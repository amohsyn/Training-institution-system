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
using TrainingIS.Entities.Resources.WorkGroupResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(WorkGroup))]
    public class Default_WorkGroup_Details_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", GroupName = "Designation", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = false,isColumn = true )]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Code", GroupName = "Designation", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", GroupName = "Designation", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Display(Name = "President_Former", GroupName = "President", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "President_Former", FilterBy = "President_Former.Id", SearchBy = "President_Former.Reference", OrderBy = "President_Former.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Former President_Former  {set; get;}  
   
		[Display(Name = "President_Trainee", GroupName = "President", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "President_Trainee", FilterBy = "President_Trainee.Id", SearchBy = "President_Trainee.Reference", OrderBy = "President_Trainee.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Trainee President_Trainee  {set; get;}  
   
		[Display(Name = "President_Administrator", GroupName = "President", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "President_Administrator", FilterBy = "President_Administrator.Id", SearchBy = "President_Administrator.Reference", OrderBy = "President_Administrator.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Administrator President_Administrator  {set; get;}  
   
		[Display(Name = "VicePresident_Former", GroupName = "VicePresident", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "VicePresident_Former", FilterBy = "VicePresident_Former.Id", SearchBy = "VicePresident_Former.Reference", OrderBy = "VicePresident_Former.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Former VicePresident_Former  {set; get;}  
   
		[Display(Name = "VicePresident_Trainee", GroupName = "VicePresident", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "VicePresident_Trainee", FilterBy = "VicePresident_Trainee.Id", SearchBy = "VicePresident_Trainee.Reference", OrderBy = "VicePresident_Trainee.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Trainee VicePresident_Trainee  {set; get;}  
   
		[Display(Name = "VicePresident_Administrator", GroupName = "VicePresident", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "VicePresident_Administrator", FilterBy = "VicePresident_Administrator.Id", SearchBy = "VicePresident_Administrator.Reference", OrderBy = "VicePresident_Administrator.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Administrator VicePresident_Administrator  {set; get;}  
   
		[Display(Name = "Protractor_Former", GroupName = "Protractor", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Protractor_Former", FilterBy = "Protractor_Former.Id", SearchBy = "Protractor_Former.Reference", OrderBy = "Protractor_Former.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Former Protractor_Former  {set; get;}  
   
		[Display(Name = "Protractor_Administrator", GroupName = "Protractor", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Protractor_Administrator", FilterBy = "Protractor_Administrator.Id", SearchBy = "Protractor_Administrator.Reference", OrderBy = "Protractor_Administrator.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Administrator Protractor_Administrator  {set; get;}  
   
		[Display(Name = "Protractor_Trainee", GroupName = "Protractor", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Protractor_Trainee", FilterBy = "Protractor_Trainee.Id", SearchBy = "Protractor_Trainee.Reference", OrderBy = "Protractor_Trainee.Reference",  AutoGenerateFilter = false,isColumn = false )]
		public Trainee Protractor_Trainee  {set; get;}  
   
		[Display(Name = "MemebersFormers", GroupName = "Members", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "MemebersFormers", FilterBy = "", SearchBy = "", OrderBy = "MemebersFormers.Count",  AutoGenerateFilter = false,isColumn = false )]
		public List<Former> MemebersFormers  {set; get;}  
   
		[Display(Name = "MemebersAdministrators", GroupName = "Members", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "MemebersAdministrators", FilterBy = "", SearchBy = "", OrderBy = "MemebersAdministrators.Count",  AutoGenerateFilter = false,isColumn = false )]
		public List<Administrator> MemebersAdministrators  {set; get;}  
   
		[Display(Name = "MemebersTrainees", GroupName = "Members", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "MemebersTrainees", FilterBy = "", SearchBy = "", OrderBy = "MemebersTrainees.Count",  AutoGenerateFilter = false,isColumn = false )]
		public List<Trainee> MemebersTrainees  {set; get;}  
   
		[Display(Name = "GuestFormers", GroupName = "Guests", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "GuestFormers", FilterBy = "GuestFormers", SearchBy = "GuestFormers", OrderBy = "GuestFormers",  AutoGenerateFilter = false,isColumn = false )]
		public Boolean GuestFormers  {set; get;}  
   
		[Display(Name = "GuestTrainees", GroupName = "Guests", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "GuestTrainees", FilterBy = "GuestTrainees", SearchBy = "GuestTrainees", OrderBy = "GuestTrainees",  AutoGenerateFilter = false,isColumn = false )]
		public Boolean GuestTrainees  {set; get;}  
   
		[Display(Name = "GuestAdministrator", GroupName = "Guests", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "GuestAdministrator", FilterBy = "GuestAdministrator", SearchBy = "GuestAdministrator", OrderBy = "GuestAdministrator",  AutoGenerateFilter = false,isColumn = false )]
		public Boolean GuestAdministrator  {set; get;}  
   
		[Display(Name = "Missions", GroupName = "Missions", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Mission_Working_Groups", FilterBy = "", SearchBy = "", OrderBy = "Mission_Working_Groups.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<Mission_Working_Group> Mission_Working_Groups  {set; get;}  
   
		public Person President  {set; get;}  
   
		public Person VicePresident  {set; get;}  
   
		public Person Protractor  {set; get;}  
   
    }
}    
