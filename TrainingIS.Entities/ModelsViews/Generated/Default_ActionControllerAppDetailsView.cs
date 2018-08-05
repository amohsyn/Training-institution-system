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
using TrainingIS.Entities.Resources.ControllerAppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_ActionControllerAppDetailsView : BaseModelView
    {
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
		public ControllerApp ControllerApp  {set; get;}  
   
		public List<AuthrorizationApp> AuthrorizationApps  {set; get;}  
   
    }
}
