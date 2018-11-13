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
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    public class Default_Form_ModuleTraining_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Specialty))]
		[GAppDataTable(PropertyPath = "SpecialtyId", FilterBy = "SpecialtyId", SearchBy = "SpecialtyId", OrderBy = "SpecialtyId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SpecialtyId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Metier))]
		[GAppDataTable(PropertyPath = "MetierId", FilterBy = "MetierId", SearchBy = "MetierId", OrderBy = "MetierId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 MetierId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_YearStudy))]
		[GAppDataTable(PropertyPath = "YearStudyId", FilterBy = "YearStudyId", SearchBy = "YearStudyId", OrderBy = "YearStudyId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 YearStudyId  {set; get;}  
   
		[Required]
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = true,isColumn = true )]
		public String Name  {set; get;}  
   
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Display(Name = "HourlyMass", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "HourlyMass", FilterBy = "HourlyMass", SearchBy = "HourlyMass", OrderBy = "HourlyMass",  AutoGenerateFilter = false,isColumn = true )]
		public Single HourlyMass  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "Hourly_Mass_To_Teach", FilterBy = "Hourly_Mass_To_Teach", SearchBy = "Hourly_Mass_To_Teach", OrderBy = "Hourly_Mass_To_Teach",  AutoGenerateFilter = false,isColumn = true )]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
