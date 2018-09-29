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
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "ScheduleId", SearchBy = "ScheduleId", OrderBy = "ScheduleId",  PropertyPath = "ScheduleId")]
		public Int64 ScheduleId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TrainingId", SearchBy = "TrainingId", OrderBy = "TrainingId",  PropertyPath = "TrainingId")]
		public Int64 TrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SeanceDayId", SearchBy = "SeanceDayId", OrderBy = "SeanceDayId",  PropertyPath = "SeanceDayId")]
		public Int64 SeanceDayId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "SeanceNumberId", SearchBy = "SeanceNumberId", OrderBy = "SeanceNumberId",  PropertyPath = "SeanceNumberId")]
		public Int64 SeanceNumberId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Classroom))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "ClassroomId", SearchBy = "ClassroomId", OrderBy = "ClassroomId",  PropertyPath = "ClassroomId")]
		public Int64 ClassroomId  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
