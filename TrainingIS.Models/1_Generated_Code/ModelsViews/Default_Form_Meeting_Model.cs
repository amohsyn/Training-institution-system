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
	[EditView(typeof(Meeting))]
	[CreateView(typeof(Meeting))]
    public class Default_Form_Meeting_Model : BaseModel
    {
		[Display(Name = "MeetingDate", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate",  PropertyPath = "MeetingDate")]
		public DateTime MeetingDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "WorkGroupId", SearchBy = "WorkGroupId", OrderBy = "WorkGroupId",  PropertyPath = "WorkGroupId")]
		public Int64 WorkGroupId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Mission_Working_Group))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Mission_Working_GroupId", SearchBy = "Mission_Working_GroupId", OrderBy = "Mission_Working_GroupId",  PropertyPath = "Mission_Working_GroupId")]
		public Int64 Mission_Working_GroupId  {set; get;}  
   
		[Display(Name = "Presence_Of_President", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presence_Of_President", SearchBy = "Presence_Of_President", OrderBy = "Presence_Of_President",  PropertyPath = "Presence_Of_President")]
		public Boolean Presence_Of_President  {set; get;}  
   
		[Display(Name = "Presence_Of_VicePresident", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presence_Of_VicePresident", SearchBy = "Presence_Of_VicePresident", OrderBy = "Presence_Of_VicePresident",  PropertyPath = "Presence_Of_VicePresident")]
		public Boolean Presence_Of_VicePresident  {set; get;}  
   
		[Display(Name = "Presence_Of_Protractor", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presence_Of_Protractor", SearchBy = "Presence_Of_Protractor", OrderBy = "Presence_Of_Protractor",  PropertyPath = "Presence_Of_Protractor")]
		public Boolean Presence_Of_Protractor  {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Former))]
		[Display(Name = "Presences_Of_Formers", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Formers", SearchBy = "Presences_Of_Formers", OrderBy = "Presences_Of_Formers",  PropertyPath = "Presences_Of_Formers")]
		public List<String> Selected_Presences_Of_Formers {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Administrator))]
		[Display(Name = "Presences_Of_Administrators", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Administrators", SearchBy = "Presences_Of_Administrators", OrderBy = "Presences_Of_Administrators",  PropertyPath = "Presences_Of_Administrators")]
		public List<String> Selected_Presences_Of_Administrators {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Trainee))]
		[Display(Name = "Presences_Of_Trainees", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Trainees", SearchBy = "Presences_Of_Trainees", OrderBy = "Presences_Of_Trainees",  PropertyPath = "Presences_Of_Trainees")]
		public List<String> Selected_Presences_Of_Trainees {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Former))]
		[Display(Name = "Presences_Of_Guests_Formers", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Guests_Formers", SearchBy = "Presences_Of_Guests_Formers", OrderBy = "Presences_Of_Guests_Formers",  PropertyPath = "Presences_Of_Guests_Formers")]
		public List<String> Selected_Presences_Of_Guests_Formers {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Administrator))]
		[Display(Name = "Presences_Of_Guests_Administrators", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Guests_Administrators", SearchBy = "Presences_Of_Guests_Administrators", OrderBy = "Presences_Of_Guests_Administrators",  PropertyPath = "Presences_Of_Guests_Administrators")]
		public List<String> Selected_Presences_Of_Guests_Administrators {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Trainee))]
		[Display(Name = "Presences_Of_Guests_Trainees", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Presences_Of_Guests_Trainees", SearchBy = "Presences_Of_Guests_Trainees", OrderBy = "Presences_Of_Guests_Trainees",  PropertyPath = "Presences_Of_Guests_Trainees")]
		public List<String> Selected_Presences_Of_Guests_Trainees {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
