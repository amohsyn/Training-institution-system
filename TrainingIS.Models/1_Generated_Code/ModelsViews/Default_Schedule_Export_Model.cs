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
	[ExportView(typeof(Schedule))]
    public class Default_Schedule_Export_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "TrainingYear", FilterBy = "TrainingYear.Id", SearchBy = "TrainingYear.Reference", OrderBy = "TrainingYear.Reference",  AutoGenerateFilter = false,isColumn = true )]
		public TrainingYear TrainingYear  {set; get;}  
   
		[Required]
		[Display(Name = "StartDate", Order = 0, ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(PropertyPath = "StartDate", FilterBy = "StartDate", SearchBy = "StartDate", OrderBy = "StartDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime StartDate  {set; get;}  
   
		[Required]
		[Display(Name = "EndtDate", Order = 0, ResourceType = typeof(msg_Schedule))]
		[GAppDataTable(PropertyPath = "EndtDate", FilterBy = "EndtDate", SearchBy = "EndtDate", OrderBy = "EndtDate",  AutoGenerateFilter = false,isColumn = true )]
		public DateTime EndtDate  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
