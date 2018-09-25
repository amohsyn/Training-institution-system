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
using GApp.Entities.Resources.AppResources;  
using TrainingIS.Entities.Resources.StateOfAbseceResources;  
using TrainingIS.Entities.Resources.TraineeResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(StateOfAbsece))]
	[CreateView(typeof(StateOfAbsece))]
    public class Default_Form_StateOfAbsece_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Category", ResourceType = typeof(msg_StateOfAbsece))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Category", SearchBy = "Category", OrderBy = "Category",  PropertyPath = "Category")]
		public StateOfAbseceCategories Category  {set; get;}  
   
		[Required]
		[Display(Name = "Value", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Value", SearchBy = "Value", OrderBy = "Value",  PropertyPath = "Value")]
		public Int32 Value  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TraineeId", SearchBy = "TraineeId", OrderBy = "TraineeId",  PropertyPath = "TraineeId")]
		public Int64 TraineeId  {set; get;}  
   
    }
}    
