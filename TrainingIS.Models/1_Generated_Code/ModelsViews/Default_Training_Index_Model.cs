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
using TrainingIS.Entities.Resources.TrainingYearResources; 
using TrainingIS.Entities.Resources.ModuleTrainingResources; 
using TrainingIS.Entities.Resources.FormerResources; 
using TrainingIS.Entities.Resources.GroupResources; 
using GApp.Entities.Resources.AppResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(Training))]
	[SearchBy("Reference")]
    public class Default_Training_Index_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "TrainingYear", FilterBy = "TrainingYear.Id", SearchBy = "TrainingYear.Reference", OrderBy = "TrainingYear.Reference",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "ModuleTraining", FilterBy = "ModuleTraining.Id", SearchBy = "ModuleTraining.Reference", OrderBy = "ModuleTraining.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public ModuleTraining ModuleTraining  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "Hourly_Mass_To_Teach", FilterBy = "Hourly_Mass_To_Teach", SearchBy = "Hourly_Mass_To_Teach", OrderBy = "Hourly_Mass_To_Teach",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =false)]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(PropertyPath = "Former", FilterBy = "Former.Id", SearchBy = "Former.Reference", OrderBy = "Former.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Former Former  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Group))]
		[GAppDataTable(PropertyPath = "Group", FilterBy = "Group.Id", SearchBy = "Group.Reference", OrderBy = "Group.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Group Group  {set; get;}  
   
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Description  {set; get;}  
   
    }
}    
