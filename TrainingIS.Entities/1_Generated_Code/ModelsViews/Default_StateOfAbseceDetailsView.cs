using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.StateOfAbseceResources; 
using TrainingIS.Entities.Resources.TraineeResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_StateOfAbseceDetailsView : BaseModelView
    {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Category", ResourceType = typeof(msg_StateOfAbsece))]
		public StateOfAbseceCategories Category  {set; get;}  
   
		[Required]
		[Display(Name = "Value", ResourceType = typeof(msg_app))]
		public Int32 Value  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		public Trainee Trainee  {set; get;}  
   
    }
}
