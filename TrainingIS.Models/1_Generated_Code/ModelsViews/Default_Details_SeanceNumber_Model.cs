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
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "StartTime", ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "StartTime")]
		[DataType(DataType.Time)]
		public DateTime StartTime  {set; get;}  
   
		[Required]
		[Display(Name = "EndTime", ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "EndTime")]
		[DataType(DataType.Time)]
		public DateTime EndTime  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
