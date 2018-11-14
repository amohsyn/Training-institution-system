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
using System.ComponentModel.DataAnnotations;

using TrainingIS.Entities.Resources.TrainingYearResources;  
using TrainingIS.Entities.Resources.ScheduleResources;  
using GApp.Entities.Resources.AppResources;  
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    [FormView(typeof(Schedule))]
    public class Default_Form_Schedule_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_TrainingYear))]
		[GAppDataTable(PropertyPath = "TrainingYearId", FilterBy = "TrainingYearId", SearchBy = "TrainingYearId", OrderBy = "TrainingYearId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 TrainingYearId  {set; get;}  
   
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
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
