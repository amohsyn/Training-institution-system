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
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ScheduleId")]
		public Int64 ScheduleId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "TrainingId")]
		public Int64 TrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "SeanceDayId")]
		public Int64 SeanceDayId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "SeanceNumberId")]
		public Int64 SeanceNumberId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Classroom))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ClassroomId")]
		public Int64 ClassroomId  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
