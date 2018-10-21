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
using GApp.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.SeanceNumberResources; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(SeanceNumber))]
	[IndexView(typeof(SeanceNumber))]
    public class Default_Details_SeanceNumber_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "Code", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "StartTime", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "StartTime", SearchBy = "StartTime", OrderBy = "StartTime",  PropertyPath = "StartTime")]
		[DataType(DataType.Time)]
		public DateTime StartTime  {set; get;}  
   
		[Required]
		[Display(Name = "EndTime", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EndTime", SearchBy = "EndTime", OrderBy = "EndTime",  PropertyPath = "EndTime")]
		[DataType(DataType.Time)]
		public DateTime EndTime  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
