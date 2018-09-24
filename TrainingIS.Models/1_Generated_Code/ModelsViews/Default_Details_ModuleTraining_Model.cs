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
using TrainingIS.Entities.Resources.SpecialtyResources; 
using TrainingIS.Entities.Resources.MetierResources; 
using TrainingIS.Entities.Resources.YearStudyResources; 
using GApp.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.ModuleTrainingResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(ModuleTraining))]
	[IndexView(typeof(ModuleTraining))]
    public class Default_Details_ModuleTraining_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Specialty.Reference")]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Metier))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Metier.Reference")]
		public Metier Metier  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "YearStudy.Reference")]
		public YearStudy YearStudy  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "HourlyMass", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "HourlyMass")]
		public Single HourlyMass  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Hourly_Mass_To_Teach")]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
