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
using TrainingIS.Entities.Resources.AbsenceResources;  
using TrainingIS.Entities.Resources.SeanceTrainingResources;  
using TrainingIS.Entities.Resources.TraineeResources;  
using TrainingIS.Entities.Resources.SeancePlanningResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(Absence))]
	[CreateView(typeof(Absence))]
    public class Default_Form_Absence_Model : BaseModel
    {
		[Required]
		[Display(Name = "AbsenceDate", ResourceType = typeof(msg_Absence))]
		[DataType(DataType.Date)]
		public DateTime AbsenceDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
		public Int64 SeanceTrainingId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
		public Int64 SeancePlanningId  {set; get;}  
   
		[Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
		public String FormerComment  {set; get;}  
   
		[Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
		public String TraineeComment  {set; get;}  
   
		[Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
		public String SupervisorComment  {set; get;}  
   
		[Display(Name = "Valide", ResourceType = typeof(msg_Absence))]
		public Boolean Valide  {set; get;}  
   
    }
}    
