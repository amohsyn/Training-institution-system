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
using TrainingIS.Entities.Resources.AppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_AuthrorizationAppFormView : BaseModelView
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_RoleApp))]
		public RoleApp RoleApp  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
		public ControllerApp ControllerApp  {set; get;}  
   
		[Required]
		[Display(Name = "isAllAction", ResourceType = typeof(msg_AuthrorizationApp))]
		public Boolean isAllAction  {set; get;}  
   
		[Display(Name = "ActionControllerApps", ResourceType = typeof(msg_app))]
		public List`1 ActionControllerApps  {set; get;}  
   
    }
}
