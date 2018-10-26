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
using TrainingIS.Entities.Resources.TraineeResources;  
using TrainingIS.Entities.Resources.WarningTraineeResources;  
using TrainingIS.Entities.Resources.Category_WarningTraineeResources;  
using GApp.Entities.Resources.AppResources;  
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(WarningTrainee))]
	[CreateView(typeof(WarningTrainee))]
    public class Default_Form_WarningTrainee_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "TraineeId", FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "WarningDate", Order = 0, ResourceType = typeof(msg_WarningTrainee))]
		[GAppDataTable(PropertyPath = "WarningDate", FilterBy = "WarningDate", SearchBy = "WarningDate", OrderBy = "WarningDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime WarningDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Category_WarningTrainee))]
		[GAppDataTable(PropertyPath = "Category_WarningTraineeId", FilterBy = "Category_WarningTraineeId", SearchBy = "Category_WarningTraineeId", OrderBy = "Category_WarningTraineeId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 Category_WarningTraineeId  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
