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
using TrainingIS.Entities.Resources.TrainingYearResources; 
using TrainingIS.Entities.Resources.ScheduleResources; 
using GApp.Entities.Resources.AppResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Schedule))]
	[IndexView(typeof(Schedule))]
    public class Default_Details_Schedule_Model : BaseModel
    {
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "TrainingYear.Id", SearchBy = "TrainingYear.Reference", OrderBy = "TrainingYear.Reference",  PropertyPath = "TrainingYear")]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  PropertyPath = "StartDate")]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  PropertyPath = "EndtDate")]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
