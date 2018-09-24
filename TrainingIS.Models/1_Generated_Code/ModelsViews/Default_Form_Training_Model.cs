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
	[EditView(typeof(Training))]
	[CreateView(typeof(Training))]
    public class Default_Form_Training_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingYearId")]
		public Int64 TrainingYearId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ModuleTrainingId")]
		public Int64 ModuleTrainingId  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Hourly_Mass_To_Teach")]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Former))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "FormerId")]
		public Int64 FormerId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Group))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "GroupId")]
		public Int64 GroupId  {set; get;}  
   
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
