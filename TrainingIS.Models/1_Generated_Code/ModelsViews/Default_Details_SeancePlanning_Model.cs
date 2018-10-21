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
	[IndexView(typeof(SeancePlanning))]
    public class Default_Details_SeancePlanning_Model : BaseModel
    {
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Schedule.Id", SearchBy = "Schedule.Reference", OrderBy = "Schedule.Reference",  PropertyPath = "Schedule")]
		public Schedule Schedule  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Training))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Training.Id", SearchBy = "Training.Reference", OrderBy = "Training.Reference",  PropertyPath = "Training")]
		public Training Training  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceDay))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "SeanceDay.Id", SearchBy = "SeanceDay.Reference", OrderBy = "SeanceDay.Reference",  PropertyPath = "SeanceDay")]
		public SeanceDay SeanceDay  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceNumber))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "SeanceNumber.Id", SearchBy = "SeanceNumber.Reference", OrderBy = "SeanceNumber.Reference",  PropertyPath = "SeanceNumber")]
		public SeanceNumber SeanceNumber  {set; get;}  
   
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Classroom))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Classroom.Id", SearchBy = "Classroom.Reference", OrderBy = "Classroom.Reference",  PropertyPath = "Classroom")]
		public Classroom Classroom  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
