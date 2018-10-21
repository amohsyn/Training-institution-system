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
		[Display(Name = "MeetingDate", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate",  PropertyPath = "MeetingDate")]
		public DateTime MeetingDate  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "WorkGroup.Id", SearchBy = "WorkGroup.Reference", OrderBy = "WorkGroup.Reference",  PropertyPath = "WorkGroup")]
		public WorkGroup WorkGroup  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Mission_Working_Group))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Mission_Working_Group.Id", SearchBy = "Mission_Working_Group.Reference", OrderBy = "Mission_Working_Group.Reference",  PropertyPath = "Mission_Working_Group")]
		public Mission_Working_Group Mission_Working_Group  {set; get;}  
   
		[Display(Name = "Presence_Of_President", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presence_Of_President", SearchBy = "Presence_Of_President", OrderBy = "Presence_Of_President",  PropertyPath = "Presence_Of_President")]
		public Boolean Presence_Of_President  {set; get;}  
   
		[Display(Name = "Presence_Of_VicePresident", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presence_Of_VicePresident", SearchBy = "Presence_Of_VicePresident", OrderBy = "Presence_Of_VicePresident",  PropertyPath = "Presence_Of_VicePresident")]
		public Boolean Presence_Of_VicePresident  {set; get;}  
   
		[Display(Name = "Presence_Of_Protractor", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presence_Of_Protractor", SearchBy = "Presence_Of_Protractor", OrderBy = "Presence_Of_Protractor",  PropertyPath = "Presence_Of_Protractor")]
		public Boolean Presence_Of_Protractor  {set; get;}  
   
		[Display(Name = "Presences_Of_Formers", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Formers", SearchBy = "Presences_Of_Formers", OrderBy = "Presences_Of_Formers",  PropertyPath = "Presences_Of_Formers")]
		public List<Former> Presences_Of_Formers  {set; get;}  
   
		[Display(Name = "Presences_Of_Administrators", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Administrators", SearchBy = "Presences_Of_Administrators", OrderBy = "Presences_Of_Administrators",  PropertyPath = "Presences_Of_Administrators")]
		public List<Administrator> Presences_Of_Administrators  {set; get;}  
   
		[Display(Name = "Presences_Of_Trainees", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Trainees", SearchBy = "Presences_Of_Trainees", OrderBy = "Presences_Of_Trainees",  PropertyPath = "Presences_Of_Trainees")]
		public List<Trainee> Presences_Of_Trainees  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Formers", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Guests_Formers", SearchBy = "Presences_Of_Guests_Formers", OrderBy = "Presences_Of_Guests_Formers",  PropertyPath = "Presences_Of_Guests_Formers")]
		public List<Former> Presences_Of_Guests_Formers  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Administrators", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Guests_Administrators", SearchBy = "Presences_Of_Guests_Administrators", OrderBy = "Presences_Of_Guests_Administrators",  PropertyPath = "Presences_Of_Guests_Administrators")]
		public List<Administrator> Presences_Of_Guests_Administrators  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Trainees", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Guests_Trainees", SearchBy = "Presences_Of_Guests_Trainees", OrderBy = "Presences_Of_Guests_Trainees",  PropertyPath = "Presences_Of_Guests_Trainees")]
		public List<Trainee> Presences_Of_Guests_Trainees  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
