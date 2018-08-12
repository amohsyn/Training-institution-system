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
using TrainingIS.Entities.Resources.ScheduleResources; 
using TrainingIS.Entities.Resources.TrainingResources; 
using TrainingIS.Entities.Resources.SeanceDayResources; 
using TrainingIS.Entities.Resources.SeanceNumberResources; 
using TrainingIS.Entities.Resources.ClassroomResources; 
using GApp.Entities.Resources.AppResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(SeancePlanning))]
    public class Default_Details_SeancePlanning_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_Schedule))]
		public Schedule Schedule  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
		public Training Training  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
		public SeanceDay SeanceDay  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
		public SeanceNumber SeanceNumber  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Classroom))]
		public Classroom Classroom  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}    
