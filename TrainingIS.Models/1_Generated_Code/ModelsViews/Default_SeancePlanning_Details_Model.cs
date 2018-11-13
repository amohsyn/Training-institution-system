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
	[DetailsView(typeof(SeancePlanning))]
    public class Default_SeancePlanning_Details_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(PropertyPath = "Schedule", FilterBy = "Schedule.Id", SearchBy = "Schedule.Reference", OrderBy = "Schedule.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public Schedule Schedule  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Training))]
		[GAppDataTable(PropertyPath = "Training", FilterBy = "Training.Id", SearchBy = "Training.Reference", OrderBy = "Training.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public Training Training  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceDay))]
		[GAppDataTable(PropertyPath = "SeanceDay", FilterBy = "SeanceDay.Id", SearchBy = "SeanceDay.Reference", OrderBy = "SeanceDay.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public SeanceDay SeanceDay  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceNumber))]
		[GAppDataTable(PropertyPath = "SeanceNumber", FilterBy = "SeanceNumber.Id", SearchBy = "SeanceNumber.Reference", OrderBy = "SeanceNumber.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public SeanceNumber SeanceNumber  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_Classroom))]
		[GAppDataTable(PropertyPath = "Classroom", FilterBy = "Classroom.Id", SearchBy = "Classroom.Reference", OrderBy = "Classroom.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public Classroom Classroom  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
