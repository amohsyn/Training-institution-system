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
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    public class Default_Form_SeancePlanning_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(PropertyPath = "ScheduleId", FilterBy = "ScheduleId", SearchBy = "ScheduleId", OrderBy = "ScheduleId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 ScheduleId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Training))]
		[GAppDataTable(PropertyPath = "TrainingId", FilterBy = "TrainingId", SearchBy = "TrainingId", OrderBy = "TrainingId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceDay))]
		[GAppDataTable(PropertyPath = "SeanceDayId", FilterBy = "SeanceDayId", SearchBy = "SeanceDayId", OrderBy = "SeanceDayId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SeanceDayId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceNumber))]
		[GAppDataTable(PropertyPath = "SeanceNumberId", FilterBy = "SeanceNumberId", SearchBy = "SeanceNumberId", OrderBy = "SeanceNumberId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 SeanceNumberId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Classroom))]
		[GAppDataTable(PropertyPath = "ClassroomId", FilterBy = "ClassroomId", SearchBy = "ClassroomId", OrderBy = "ClassroomId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 ClassroomId  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
