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
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    public class Default_Form_Meeting_Model : BaseModel
    {
		[Display(Name = "MeetingDate", GroupName = "Object", Order = 1, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "MeetingDate", FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime MeetingDate  {set; get;}  
   
		[Required]
		[Display(Name = "WorkGroup", GroupName = "Object", Order = 2, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "WorkGroupId", FilterBy = "WorkGroupId", SearchBy = "WorkGroupId", OrderBy = "WorkGroupId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 WorkGroupId  {set; get;}  
   
		[Required]
		[Display(Name = "Mission_Working_Group", GroupName = "Object", Order = 3, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Mission_Working_GroupId", FilterBy = "Mission_Working_GroupId", SearchBy = "Mission_Working_GroupId", OrderBy = "Mission_Working_GroupId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 Mission_Working_GroupId  {set; get;}  
   
		[Display(Name = "Description", GroupName = "Object", Order = 4, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Display(Name = "Presence_Of_President", GroupName = "Members", Order = 1, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_President", FilterBy = "Presence_Of_President", SearchBy = "Presence_Of_President", OrderBy = "Presence_Of_President",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean Presence_Of_President  {set; get;}  
   
		[Display(Name = "Presence_Of_VicePresident", GroupName = "Members", Order = 2, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_VicePresident", FilterBy = "Presence_Of_VicePresident", SearchBy = "Presence_Of_VicePresident", OrderBy = "Presence_Of_VicePresident",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean Presence_Of_VicePresident  {set; get;}  
   
		[Display(Name = "Presence_Of_Protractor", GroupName = "Members", Order = 3, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_Protractor", FilterBy = "Presence_Of_Protractor", SearchBy = "Presence_Of_Protractor", OrderBy = "Presence_Of_Protractor",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean Presence_Of_Protractor  {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Former))]
		[Display(Name = "Presences_Of_Formers", GroupName = "Members", Order = 4, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Formers", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Formers.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Presences_Of_Formers {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Administrator))]
		[Display(Name = "Presences_Of_Administrators", GroupName = "Members", Order = 5, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Administrators", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Administrators.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Presences_Of_Administrators {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Trainee))]
		[Display(Name = "Presences_Of_Trainees", GroupName = "Members", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Trainees", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Trainees.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Presences_Of_Trainees {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Former))]
		[Display(Name = "Presences_Of_Guests_Formers", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Formers", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Guests_Formers.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Presences_Of_Guests_Formers {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Administrator))]
		[Display(Name = "Presences_Of_Guests_Administrators", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Administrators", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Guests_Administrators.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Presences_Of_Guests_Administrators {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.MultiSelect , TypeOfEntity = typeof(Trainee))]
		[Display(Name = "Presences_Of_Guests_Trainees", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Trainees", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Guests_Trainees.Count",  AutoGenerateFilter = false,isColumn = true )]
		public List<String> Selected_Presences_Of_Guests_Trainees {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
