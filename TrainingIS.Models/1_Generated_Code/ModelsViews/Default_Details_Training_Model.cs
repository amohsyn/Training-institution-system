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
using TrainingIS.Entities.Resources.TrainingYearResources; 
using TrainingIS.Entities.Resources.ModuleTrainingResources; 
using TrainingIS.Entities.Resources.FormerResources; 
using TrainingIS.Entities.Resources.GroupResources; 
using GApp.Entities.Resources.AppResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Training))]
	[IndexView(typeof(Training))]
    public class Default_Details_Training_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TrainingYear.Id", SearchBy = "TrainingYear.Reference", OrderBy = "TrainingYear.Reference",  PropertyPath = "TrainingYear")]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "ModuleTraining.Id", SearchBy = "ModuleTraining.Reference", OrderBy = "ModuleTraining.Reference",  PropertyPath = "ModuleTraining")]
		public ModuleTraining ModuleTraining  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Hourly_Mass_To_Teach", SearchBy = "Hourly_Mass_To_Teach", OrderBy = "Hourly_Mass_To_Teach",  PropertyPath = "Hourly_Mass_To_Teach")]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Former.Id", SearchBy = "Former.Reference", OrderBy = "Former.Reference",  PropertyPath = "Former")]
		public Former Former  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Group.Id", SearchBy = "Group.Reference", OrderBy = "Group.Reference",  PropertyPath = "Group")]
		public Group Group  {set; get;}  
   
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
