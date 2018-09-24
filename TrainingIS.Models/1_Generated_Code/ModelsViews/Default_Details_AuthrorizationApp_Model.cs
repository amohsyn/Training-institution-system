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
using GApp.Entities.Resources.RoleAppResources; 
using GApp.Entities.Resources.ControllerAppResources; 
using GApp.Entities.Resources.AuthrorizationAppResources; 
using GApp.Entities.Resources.ActionControllerAppResources; 
using GApp.Models.DataAnnotations; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(AuthrorizationApp))]
	[IndexView(typeof(AuthrorizationApp))]
    public class Default_Details_AuthrorizationApp_Model : BaseModel
    {
		[Display(Name = "SingularName", ResourceType = typeof(msg_RoleApp))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "RoleApp.Reference")]
		public RoleApp RoleApp  {set; get;}  
   
		[Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ControllerApp.Reference")]
		public ControllerApp ControllerApp  {set; get;}  
   
		[Required]
		[Display(Name = "isAllAction", ResourceType = typeof(msg_AuthrorizationApp))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "isAllAction")]
		public Boolean isAllAction  {set; get;}  
   
		[Display(Name = "PluralName", ResourceType = typeof(msg_ActionControllerApp))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ActionControllerApps")]
		[SelectFilter(Filter_HTML_Id = "ControllerAppId" , PropertyType = typeof(ActionControllerApp))]
		public List<ActionControllerApp> ActionControllerApps  {set; get;}  
   
    }
}    
