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
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Specialty.Id", SearchBy = "Specialty.Reference", OrderBy = "Specialty.Reference",  PropertyPath = "Specialty")]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Metier))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Metier.Id", SearchBy = "Metier.Reference", OrderBy = "Metier.Reference",  PropertyPath = "Metier")]
		public Metier Metier  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "YearStudy.Id", SearchBy = "YearStudy.Reference", OrderBy = "YearStudy.Reference",  PropertyPath = "YearStudy")]
		public YearStudy YearStudy  {set; get;}  
   
		[Required]
		[Display(Name = "Name", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Code", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Display(Name = "HourlyMass", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "HourlyMass", SearchBy = "HourlyMass", OrderBy = "HourlyMass",  PropertyPath = "HourlyMass")]
		public Single HourlyMass  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Hourly_Mass_To_Teach", SearchBy = "Hourly_Mass_To_Teach", OrderBy = "Hourly_Mass_To_Teach",  PropertyPath = "Hourly_Mass_To_Teach")]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
