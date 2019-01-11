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
	[IndexView(typeof(ModuleTraining))]
	[SearchBy("Reference")]
    public class Default_ModuleTraining_Index_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(PropertyPath = "Specialty", FilterBy = "Specialty.Id", SearchBy = "Specialty.Reference", OrderBy = "Specialty.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Specialty Specialty  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Metier))]
		[GAppDataTable(PropertyPath = "Metier", FilterBy = "Metier.Id", SearchBy = "Metier.Reference", OrderBy = "Metier.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Metier Metier  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(PropertyPath = "YearStudy", FilterBy = "YearStudy.Id", SearchBy = "YearStudy.Reference", OrderBy = "YearStudy.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public YearStudy YearStudy  {set; get;}  
   
		[Required]
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Name  {set; get;}  
   
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Code  {set; get;}  
   
		[Display(Name = "HourlyMass", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "HourlyMass", FilterBy = "HourlyMass", SearchBy = "HourlyMass", OrderBy = "HourlyMass",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Single HourlyMass  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "Hourly_Mass_To_Teach", FilterBy = "Hourly_Mass_To_Teach", SearchBy = "Hourly_Mass_To_Teach", OrderBy = "Hourly_Mass_To_Teach",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Description  {set; get;}  
   
    }
}    
