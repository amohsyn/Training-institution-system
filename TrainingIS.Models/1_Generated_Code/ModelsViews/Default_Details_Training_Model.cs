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
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingYear.Reference")]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ModuleTraining.Reference")]
		public ModuleTraining ModuleTraining  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Hourly_Mass_To_Teach")]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Former.Reference")]
		public Former Former  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Group.Reference")]
		public Group Group  {set; get;}  
   
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
