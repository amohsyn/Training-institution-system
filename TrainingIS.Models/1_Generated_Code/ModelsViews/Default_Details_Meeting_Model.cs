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
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Meeting))]
	[IndexView(typeof(Meeting))]
    public class Default_Details_Meeting_Model : BaseModel
    {
		[Display(Name = "MeetingDate", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "MeetingDate", FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime MeetingDate  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(PropertyPath = "WorkGroup", FilterBy = "WorkGroup.Id", SearchBy = "WorkGroup", OrderBy = "WorkGroup",  AutoGenerateFilter = true,isColumn = true )]
		public WorkGroup WorkGroup  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Mission_Working_Group))]
		[GAppDataTable(PropertyPath = "Mission_Working_Group", FilterBy = "Mission_Working_Group.Id", SearchBy = "Mission_Working_Group", OrderBy = "Mission_Working_Group",  AutoGenerateFilter = true,isColumn = true )]
		public Mission_Working_Group Mission_Working_Group  {set; get;}  
   
		[Display(Name = "Presence_Of_President", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_President", FilterBy = "Presence_Of_President", SearchBy = "Presence_Of_President", OrderBy = "Presence_Of_President",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean Presence_Of_President  {set; get;}  
   
		[Display(Name = "Presence_Of_VicePresident", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_VicePresident", FilterBy = "Presence_Of_VicePresident", SearchBy = "Presence_Of_VicePresident", OrderBy = "Presence_Of_VicePresident",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean Presence_Of_VicePresident  {set; get;}  
   
		[Display(Name = "Presence_Of_Protractor", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_Protractor", FilterBy = "Presence_Of_Protractor", SearchBy = "Presence_Of_Protractor", OrderBy = "Presence_Of_Protractor",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean Presence_Of_Protractor  {set; get;}  
   
		[Display(Name = "Presences_Of_Formers", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Formers", FilterBy = "Presences_Of_Formers", SearchBy = "Presences_Of_Formers", OrderBy = "Presences_Of_Formers",  AutoGenerateFilter = false,isColumn = true )]
		public List<Former> Presences_Of_Formers  {set; get;}  
   
		[Display(Name = "Presences_Of_Administrators", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Administrators", FilterBy = "Presences_Of_Administrators", SearchBy = "Presences_Of_Administrators", OrderBy = "Presences_Of_Administrators",  AutoGenerateFilter = false,isColumn = true )]
		public List<Administrator> Presences_Of_Administrators  {set; get;}  
   
		[Display(Name = "Presences_Of_Trainees", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Trainees", FilterBy = "Presences_Of_Trainees", SearchBy = "Presences_Of_Trainees", OrderBy = "Presences_Of_Trainees",  AutoGenerateFilter = false,isColumn = true )]
		public List<Trainee> Presences_Of_Trainees  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Formers", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Formers", FilterBy = "Presences_Of_Guests_Formers", SearchBy = "Presences_Of_Guests_Formers", OrderBy = "Presences_Of_Guests_Formers",  AutoGenerateFilter = false,isColumn = true )]
		public List<Former> Presences_Of_Guests_Formers  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Administrators", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Administrators", FilterBy = "Presences_Of_Guests_Administrators", SearchBy = "Presences_Of_Guests_Administrators", OrderBy = "Presences_Of_Guests_Administrators",  AutoGenerateFilter = false,isColumn = true )]
		public List<Administrator> Presences_Of_Guests_Administrators  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Trainees", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Trainees", FilterBy = "Presences_Of_Guests_Trainees", SearchBy = "Presences_Of_Guests_Trainees", OrderBy = "Presences_Of_Guests_Trainees",  AutoGenerateFilter = false,isColumn = true )]
		public List<Trainee> Presences_Of_Guests_Trainees  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
