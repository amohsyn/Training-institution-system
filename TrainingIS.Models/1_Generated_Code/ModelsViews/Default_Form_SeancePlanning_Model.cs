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
	[EditView(typeof(SeancePlanning))]
	[CreateView(typeof(SeancePlanning))]
    public class Default_Form_SeancePlanning_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Schedule))]
		public Int64 ScheduleId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
		public Int64 TrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
		public Int64 SeanceDayId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
		public Int64 SeanceNumberId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Classroom))]
		public Int64 ClassroomId  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}    
