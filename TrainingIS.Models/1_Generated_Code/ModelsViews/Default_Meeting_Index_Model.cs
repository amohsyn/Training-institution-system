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
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(Meeting))]
	[SearchBy("Reference")]
    public class Default_Meeting_Index_Model : BaseModel
    {
		[Display(Name = "MeetingDate", GroupName = "Object", Order = 1, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "MeetingDate", FilterBy = "MeetingDate", SearchBy = "MeetingDate", OrderBy = "MeetingDate",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public DateTime MeetingDate  {set; get;}  
   
		[Display(Name = "WorkGroup", GroupName = "Object", Order = 2, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "WorkGroup", FilterBy = "WorkGroup.Id", SearchBy = "WorkGroup.Reference", OrderBy = "WorkGroup.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public WorkGroup WorkGroup  {set; get;}  
   
		[Display(Name = "Mission_Working_Group", GroupName = "Object", Order = 3, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Mission_Working_Group", FilterBy = "Mission_Working_Group.Id", SearchBy = "Mission_Working_Group.Reference", OrderBy = "Mission_Working_Group.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Mission_Working_Group Mission_Working_Group  {set; get;}  
   
		[Display(Name = "Description", GroupName = "Object", Order = 4, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Description  {set; get;}  
   
		[Display(Name = "Presence_Of_President", GroupName = "Members", Order = 1, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_President", FilterBy = "Presence_Of_President", SearchBy = "Presence_Of_President", OrderBy = "Presence_Of_President",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Boolean Presence_Of_President  {set; get;}  
   
		[Display(Name = "Presence_Of_VicePresident", GroupName = "Members", Order = 2, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_VicePresident", FilterBy = "Presence_Of_VicePresident", SearchBy = "Presence_Of_VicePresident", OrderBy = "Presence_Of_VicePresident",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Boolean Presence_Of_VicePresident  {set; get;}  
   
		[Display(Name = "Presence_Of_Protractor", GroupName = "Members", Order = 3, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presence_Of_Protractor", FilterBy = "Presence_Of_Protractor", SearchBy = "Presence_Of_Protractor", OrderBy = "Presence_Of_Protractor",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Boolean Presence_Of_Protractor  {set; get;}  
   
		[Display(Name = "Presences_Of_Formers", GroupName = "Members", Order = 4, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Formers", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Formers.Count",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public List<Former> Presences_Of_Formers  {set; get;}  
   
		[Display(Name = "Presences_Of_Administrators", GroupName = "Members", Order = 5, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Administrators", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Administrators.Count",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public List<Administrator> Presences_Of_Administrators  {set; get;}  
   
		[Display(Name = "Presences_Of_Trainees", GroupName = "Members", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Trainees", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Trainees.Count",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public List<Trainee> Presences_Of_Trainees  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Formers", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Formers", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Guests_Formers.Count",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public List<Former> Presences_Of_Guests_Formers  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Administrators", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Administrators", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Guests_Administrators.Count",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public List<Administrator> Presences_Of_Guests_Administrators  {set; get;}  
   
		[Display(Name = "Presences_Of_Guests_Trainees", GroupName = "Presences_Guest", Order = 6, ResourceType = typeof(msg_Meeting))]
		[GAppDataTable(PropertyPath = "Presences_Of_Guests_Trainees", FilterBy = "", SearchBy = "", OrderBy = "Presences_Of_Guests_Trainees.Count",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public List<Trainee> Presences_Of_Guests_Trainees  {set; get;}  
   
    }
}    
