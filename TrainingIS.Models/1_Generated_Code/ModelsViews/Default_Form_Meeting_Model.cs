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
using TrainingIS.Entities.Resources.MeetingResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Meeting))]
	[CreateView(typeof(Meeting))]
    public class Default_Form_Meeting_Model : BaseModel
    {
		[Display(Name = "MeetingDate", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate",  PropertyPath = "MeetingDate")]
		public DateTime MeetingDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "WorkGroupId", SearchBy = "WorkGroupId", OrderBy = "WorkGroupId",  PropertyPath = "WorkGroupId")]
		public Int64 WorkGroupId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Mission_Working_Group))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Mission_Working_GroupId", SearchBy = "Mission_Working_GroupId", OrderBy = "Mission_Working_GroupId",  PropertyPath = "Mission_Working_GroupId")]
		public Int64 Mission_Working_GroupId  {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Former))]
		[Display(Name = "PresenceFormers", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Formers", SearchBy = "Formers", OrderBy = "Formers",  PropertyPath = "Formers")]
		public List<String> Selected_Formers {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Administrator))]
		[Display(Name = "PresenceAdministrators", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Administrators", SearchBy = "Administrators", OrderBy = "Administrators",  PropertyPath = "Administrators")]
		public List<String> Selected_Administrators {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Trainee))]
		[Display(Name = "PresenceTrainees", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Trainees", SearchBy = "Trainees", OrderBy = "Trainees",  PropertyPath = "Trainees")]
		public List<String> Selected_Trainees {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
