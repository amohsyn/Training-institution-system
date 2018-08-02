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
using TrainingIS.Entities.Resources.SeanceTrainingResources; 
using TrainingIS.Entities.Resources.SeancePlanningResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_SeanceTrainingDetailsView : BaseModelView
    {
		[Required]
		[Display(Name = "SeanceDate", ResourceType = typeof(msg_SeanceTraining))]
		public DateTime SeanceDate  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeancePlanning))]
		public Int64 SeancePlanningId  {set; get;}  
   
    }
}
