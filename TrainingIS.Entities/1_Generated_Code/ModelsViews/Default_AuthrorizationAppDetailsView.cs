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
using TrainingIS.Entities.Resources.RoleAppResources; 
using TrainingIS.Entities.Resources.ControllerAppResources; 
using TrainingIS.Entities.Resources.AuthrorizationAppResources; 
using TrainingIS.Entities.Resources.ActionControllerAppResources; 
using GApp.WebApp.Manager.Views.Attributes; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_AuthrorizationAppDetailsView : BaseModelView
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_RoleApp))]
		public RoleApp RoleApp  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
		public ControllerApp ControllerApp  {set; get;}  
   
		[Required]
		[Display(Name = "isAllAction", ResourceType = typeof(msg_AuthrorizationApp))]
		public Boolean isAllAction  {set; get;}  
   
		[Display(Name = "PluralName", ResourceType = typeof(msg_ActionControllerApp))]
		[SelectFilter(Filter_HTML_Id = "ControllerAppId" , PropertyType = typeof(ActionControllerApp))]
		public List<ActionControllerApp> ActionControllerApps  {set; get;}  
   
    }
}
