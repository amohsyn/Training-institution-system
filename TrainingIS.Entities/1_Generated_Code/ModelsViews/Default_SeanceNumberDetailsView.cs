using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.AppResources; 
using TrainingIS.Entities.Resources.SeanceNumberResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_SeanceNumberDetailsView : BaseModelView
    {
		[Required]
		[Unique]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "StartTime", ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
		[DataType(DataType.Time)]
		public DateTime StartTime  {set; get;}  
   
		[Required]
		[Display(Name = "EndTime", ResourceType = typeof(msg_SeanceNumber))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
		[DataType(DataType.Time)]
		public DateTime EndTime  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}
