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
	[DetailsView(typeof(Meeting))]
	[IndexView(typeof(Meeting))]
    public class Default_Details_Meeting_Model : BaseModel
    {
		[Display(Name = "MeetingDate", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate",  PropertyPath = "MeetingDate")]
		public DateTime MeetingDate  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_WorkGroup))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "WorkGroup.Id", SearchBy = "WorkGroup.Reference", OrderBy = "WorkGroup.Reference",  PropertyPath = "WorkGroup")]
		public WorkGroup WorkGroup  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Mission_Working_Group))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Mission_Working_Group.Id", SearchBy = "Mission_Working_Group.Reference", OrderBy = "Mission_Working_Group.Reference",  PropertyPath = "Mission_Working_Group")]
		public Mission_Working_Group Mission_Working_Group  {set; get;}  
   
		[Display(Name = "PresenceFormers", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Formers", SearchBy = "Formers", OrderBy = "Formers",  PropertyPath = "Formers")]
		public List<Former> Formers  {set; get;}  
   
		[Display(Name = "PresenceAdministrators", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Administrators", SearchBy = "Administrators", OrderBy = "Administrators",  PropertyPath = "Administrators")]
		public List<Administrator> Administrators  {set; get;}  
   
		[Display(Name = "PresenceTrainees", ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Trainees", SearchBy = "Trainees", OrderBy = "Trainees",  PropertyPath = "Trainees")]
		public List<Trainee> Trainees  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
