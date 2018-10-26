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
	[EditView(typeof(WorkGroup))]
	[CreateView(typeof(WorkGroup))]
    public class Default_Form_WorkGroup_Model : BaseModel
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
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Former))]
		[Display(Name = "MemebersFormers", GroupName = "Members", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "MemebersFormers", FilterBy = "", SearchBy = "", OrderBy = "MemebersFormers.Count",  AutoGenerateFilter = false,isColumn = false )]
		public List<String> Selected_MemebersFormers {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Administrator))]
		[Display(Name = "MemebersAdministrators", GroupName = "Members", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "MemebersAdministrators", FilterBy = "", SearchBy = "", OrderBy = "MemebersAdministrators.Count",  AutoGenerateFilter = false,isColumn = false )]
		public List<String> Selected_MemebersAdministrators {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Trainee))]
		[Display(Name = "MemebersTrainees", GroupName = "Members", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "MemebersTrainees", FilterBy = "", SearchBy = "", OrderBy = "MemebersTrainees.Count",  AutoGenerateFilter = false,isColumn = false )]
		public List<String> Selected_MemebersTrainees {set; get;}  
   
		[Display(Name = "GuestFormers", GroupName = "Guests", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "GuestFormers", FilterBy = "GuestFormers", SearchBy = "GuestFormers", OrderBy = "GuestFormers",  AutoGenerateFilter = false,isColumn = false )]
		public Boolean GuestFormers  {set; get;}  
   
		[Display(Name = "GuestTrainees", GroupName = "Guests", Order = 2, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "GuestTrainees", FilterBy = "GuestTrainees", SearchBy = "GuestTrainees", OrderBy = "GuestTrainees",  AutoGenerateFilter = false,isColumn = false )]
		public Boolean GuestTrainees  {set; get;}  
   
		[Display(Name = "GuestAdministrator", GroupName = "Guests", Order = 3, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "GuestAdministrator", FilterBy = "GuestAdministrator", SearchBy = "GuestAdministrator", OrderBy = "GuestAdministrator",  AutoGenerateFilter = false,isColumn = false )]
		public Boolean GuestAdministrator  {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Mission_Working_Group))]
		[Display(Name = "Missions", GroupName = "Missions", Order = 1, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "Mission_Working_Groups", FilterBy = "", SearchBy = "", OrderBy = "Mission_Working_Groups.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Mission_Working_Groups {set; get;}  
   
		public Person President  {set; get;}  
   
		public Person VicePresident  {set; get;}  
   
		public Person Protractor  {set; get;}  
   
    }
}    
