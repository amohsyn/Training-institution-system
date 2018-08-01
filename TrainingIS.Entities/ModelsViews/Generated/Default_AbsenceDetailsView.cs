using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AppRoleResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.TraineeResources; 
using TrainingIS.Entities.Resources.AbsenceResources; 
using TrainingIS.Entities.Resources.SeanceTrainingResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_AbsenceDetailsView : BaseModelView
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_Trainee))]
		public Int64 TraineeId  {set; get;}  
   
		[Required]
		[Display(Name = "isHaveAuthorization", ResourceType = typeof(msg_Absence))]
		public Boolean isHaveAuthorization  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceTraining))]
		public Int64 SeanceTrainingId  {set; get;}  
   
		[Display(Name = "FormerComment", ResourceType = typeof(msg_Absence))]
		public String FormerComment  {set; get;}  
   
		[Display(Name = "TraineeComment", ResourceType = typeof(msg_Absence))]
		public String TraineeComment  {set; get;}  
   
		[Display(Name = "SupervisorComment", ResourceType = typeof(msg_Absence))]
		public String SupervisorComment  {set; get;}  
   
    }
}
