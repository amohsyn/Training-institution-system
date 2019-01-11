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
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(WarningTrainee))]
	[SearchBy("Reference")]
    public class Default_WarningTrainee_Index_Model : BaseModel
    {
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Trainee Trainee  {set; get;}  
   
		[Required]
		[Display(Name = "WarningDate", Order = 0, ResourceType = typeof(msg_WarningTrainee))]
		[GAppDataTable(PropertyPath = "WarningDate", FilterBy = "WarningDate", SearchBy = "WarningDate", OrderBy = "WarningDate",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public DateTime WarningDate  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Category_WarningTrainee))]
		[GAppDataTable(PropertyPath = "Category_WarningTrainee", FilterBy = "Category_WarningTrainee.Id", SearchBy = "Category_WarningTrainee.Reference", OrderBy = "Category_WarningTrainee.Reference",  AutoGenerateFilter = true,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Category_WarningTrainee Category_WarningTrainee  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Description  {set; get;}  
   
    }
}    
