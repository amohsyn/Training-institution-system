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
using GApp.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.StateOfAbseceResources; 
using TrainingIS.Entities.Resources.TraineeResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(StateOfAbsece))]
	[SearchBy("Reference")]
    public class Default_StateOfAbsece_Index_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Category", Order = 0, ResourceType = typeof(msg_StateOfAbsece))]
		[GAppDataTable(PropertyPath = "Category", FilterBy = "Category", SearchBy = "Category", OrderBy = "Category",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public StateOfAbseceCategories Category  {set; get;}  
   
		[Required]
		[Display(Name = "Value", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Value", FilterBy = "Value", SearchBy = "Value", OrderBy = "Value",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Int32 Value  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(PropertyPath = "Trainee", FilterBy = "Trainee.Id", SearchBy = "Trainee.Reference", OrderBy = "Trainee.Reference",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public Trainee Trainee  {set; get;}  
   
    }
}    
