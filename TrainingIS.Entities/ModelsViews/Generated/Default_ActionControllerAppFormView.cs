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
    
    public class Default_ActionControllerAppFormView : BaseModelView
    {
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
		public Int64 ControllerAppId  {set; get;}  
   
		[Many (TypeOfEntity = typeof(AuthrorizationApp))]
		public List<String> Selected_AuthrorizationApps {set; get;}
		[Display(AutoGenerateField = false)]
		public List<System.Web.Mvc.SelectListItem> All_AuthrorizationApps  {set; get;}  
   
    }
}
