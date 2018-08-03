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

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_AuthrorizationAppFormView : BaseModelView
    {
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_RoleApp))]
		public Int64 RoleAppId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
		public Int64 ControllerAppId  {set; get;}  
   
		[Required]
		[Display(Name = "isAllAction", ResourceType = typeof(msg_AuthrorizationApp))]
		public Boolean isAllAction  {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.Checkbox , TypeOfEntity = typeof(ActionControllerApp))]
		[Display(Name = "PluralName", ResourceType = typeof(msg_ActionControllerApp))]
		public List<String> Selected_ActionControllerApps {set; get;}
		[Display(AutoGenerateField = false)]
		public List<System.Web.Mvc.SelectListItem> All_ActionControllerApps  {set; get;}  
   
    }
}
