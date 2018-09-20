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
using TrainingIS.Entities.Resources.SeanceTrainingResources; 
using TrainingIS.Entities.Resources.SeancePlanningResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(SeanceTraining))]
	[IndexView(typeof(SeanceTraining))]
    public class Default_Details_SeanceTraining_Model : BaseModel
    {
		[Required]
		[Display(Name = "SeanceDate", ResourceType = typeof(msg_SeanceTraining))]
		[DataType(DataType.Date)]
		public DateTime SeanceDate  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
		public SeancePlanning SeancePlanning  {set; get;}  
   
		[Display(Name = "Contained", ResourceType = typeof(msg_SeanceTraining))]
		public String Contained  {set; get;}  
   
		public Boolean FormerValidation  {set; get;}  
   
		public List<Absence> Absences  {set; get;}  
   
    }
}    
