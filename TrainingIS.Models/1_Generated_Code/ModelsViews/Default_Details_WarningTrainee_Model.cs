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
	[DetailsView(typeof(WarningTrainee))]
	[IndexView(typeof(WarningTrainee))]
    public class Default_Details_WarningTrainee_Model : BaseModel
    {
		[SearchBy("Trainee.FirstName")]
		[SearchBy("Trainee.LastName")]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  PropertyPath = "Trainee")]
		public Trainee Trainee  {set; get;}  
   
		[Required]
		[Display(Name = "WarningDate", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_WarningTrainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "WarningDate", SearchBy = "WarningDate", OrderBy = "WarningDate",  PropertyPath = "WarningDate")]
		public DateTime WarningDate  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Category_WarningTrainee))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Category_WarningTrainee.Id", SearchBy = "Category_WarningTrainee.Reference", OrderBy = "Category_WarningTrainee.Reference",  PropertyPath = "Category_WarningTrainee")]
		public Category_WarningTrainee Category_WarningTrainee  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
