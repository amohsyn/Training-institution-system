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
    public class Default_SeanceNumber_Details_Model : BaseModel
    {
		[Required]
		[Unique]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "StartTime", Order = 0, ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		[GAppDataTable(PropertyPath = "StartTime", FilterBy = "StartTime", SearchBy = "StartTime", OrderBy = "StartTime",  AutoGenerateFilter = false,isColumn = true )]
		[DataType(DataType.Time)]
		public DateTime StartTime  {set; get;}  
   
		[Required]
		[Display(Name = "EndTime", Order = 0, ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		[GAppDataTable(PropertyPath = "EndTime", FilterBy = "EndTime", SearchBy = "EndTime", OrderBy = "EndTime",  AutoGenerateFilter = false,isColumn = true )]
		[DataType(DataType.Time)]
		public DateTime EndTime  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
    }
}    
