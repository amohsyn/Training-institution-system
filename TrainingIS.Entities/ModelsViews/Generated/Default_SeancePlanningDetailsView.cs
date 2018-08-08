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
using TrainingIS.Entities.Resources.ScheduleResources; 
using TrainingIS.Entities.Resources.TrainingResources; 
using TrainingIS.Entities.Resources.SeanceDayResources; 
using TrainingIS.Entities.Resources.SeanceNumberResources; 
using TrainingIS.Entities.Resources.AppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_SeancePlanningDetailsView : BaseModelView
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_Schedule))]
		public Schedule Schedule  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_Training))]
		public Training Training  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceDay))]
		public SeanceDay SeanceDay  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_SeanceNumber))]
		public SeanceNumber SeanceNumber  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}
