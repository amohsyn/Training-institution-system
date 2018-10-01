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
	[EditView(typeof(WarningTrainee))]
	[CreateView(typeof(WarningTrainee))]
    public class Default_Form_WarningTrainee_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  PropertyPath = "TraineeId")]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "WarningDate", ResourceType = typeof(msg_WarningTrainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "WarningDate", SearchBy = "WarningDate", OrderBy = "WarningDate",  PropertyPath = "WarningDate")]
		public DateTime WarningDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Category_WarningTrainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Category_WarningTraineeId", SearchBy = "Category_WarningTraineeId", OrderBy = "Category_WarningTraineeId",  PropertyPath = "Category_WarningTraineeId")]
		public Int64 Category_WarningTraineeId  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
