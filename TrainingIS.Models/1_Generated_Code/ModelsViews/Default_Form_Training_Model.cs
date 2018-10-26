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
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Training))]
	[CreateView(typeof(Training))]
    public class Default_Form_Training_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "TrainingYearId", FilterBy = "TrainingYearId", SearchBy = "TrainingYearId", OrderBy = "TrainingYearId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TrainingYearId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "ModuleTrainingId", FilterBy = "ModuleTrainingId", SearchBy = "ModuleTrainingId", OrderBy = "ModuleTrainingId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 ModuleTrainingId  {set; get;}  
   
		[Display(Name = "Hourly_Mass_To_Teach", Order = 0, ResourceType = typeof(msg_ModuleTraining))]
		[GAppDataTable(PropertyPath = "Hourly_Mass_To_Teach", FilterBy = "Hourly_Mass_To_Teach", SearchBy = "Hourly_Mass_To_Teach", OrderBy = "Hourly_Mass_To_Teach",  AutoGenerateFilter = false,isColumn = true )]
		public Single Hourly_Mass_To_Teach  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Former))]
		[GAppDataTable(PropertyPath = "FormerId", FilterBy = "FormerId", SearchBy = "FormerId", OrderBy = "FormerId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 FormerId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Group))]
		[GAppDataTable(PropertyPath = "GroupId", FilterBy = "GroupId", SearchBy = "GroupId", OrderBy = "GroupId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 GroupId  {set; get;}  
   
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
