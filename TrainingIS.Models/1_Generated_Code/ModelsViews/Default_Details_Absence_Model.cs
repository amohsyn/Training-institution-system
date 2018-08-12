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
using TrainingIS.Entities.Resources.TraineeResources; 
using TrainingIS.Entities.Resources.AbsenceResources; 
using TrainingIS.Entities.Resources.SeanceTrainingResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(Absence))]
    public class Default_Details_Absence_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		public Trainee Trainee  {set; get;}  
   
		[Required]
		[Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
		public SeanceTraining SeanceTraining  {set; get;}  
   
		[Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
		public String FormerComment  {set; get;}  
   
		[Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
		public String TraineeComment  {set; get;}  
   
		[Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
		public String SupervisorComment  {set; get;}  
   
    }
}    
