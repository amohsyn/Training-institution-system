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
using TrainingIS.Entities.Resources.SeanceDayResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(SeanceDay))]
	[IndexView(typeof(SeanceDay))]
    public class Default_Details_SeanceDay_Model : BaseModel
    {
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Required]
		[Unique]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Unique]
		[Display(Name = "Day", ResourceType = typeof(msg_SeanceDay))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Day")]
		public String Day  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
