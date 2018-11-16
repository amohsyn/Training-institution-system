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
using TrainingIS.Entities.enums;
using GApp.Entities.Resources.RoleAppResources; 
using GApp.Entities.Resources.ControllerAppResources; 
using GApp.Entities.Resources.AuthrorizationAppResources; 
using GApp.Entities.Resources.ActionControllerAppResources; 
using GApp.Models.DataAnnotations; 
 

namespace TrainingIS.Entities.ModelsViews
{
	[IndexView(typeof(AuthrorizationApp))]
	[SearchBy("Reference")]
    public class Default_AuthrorizationApp_Index_Model : BaseModel
    {
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_RoleApp))]
		[GAppDataTable(PropertyPath = "RoleApp", FilterBy = "RoleApp.Id", SearchBy = "RoleApp.Reference", OrderBy = "RoleApp.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public RoleApp RoleApp  {set; get;}  
   
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_ControllerApp))]
		[GAppDataTable(PropertyPath = "ControllerApp", FilterBy = "ControllerApp.Id", SearchBy = "ControllerApp.Reference", OrderBy = "ControllerApp.Reference",  AutoGenerateFilter = true,isColumn = true )]
		public ControllerApp ControllerApp  {set; get;}  
   
		[Required]
		[Display(Name = "isAllAction", Order = 0, ResourceType = typeof(msg_AuthrorizationApp))]
		[GAppDataTable(PropertyPath = "isAllAction", FilterBy = "isAllAction", SearchBy = "isAllAction", OrderBy = "isAllAction",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean isAllAction  {set; get;}  
   
		[Display(Name = "PluralName", Order = 0, ResourceType = typeof(msg_ActionControllerApp))]
		[GAppDataTable(PropertyPath = "ActionControllerApps", FilterBy = "", SearchBy = "", OrderBy = "ActionControllerApps.Count",  AutoGenerateFilter = false,isColumn = true )]
		[SelectFilter(Filter_HTML_Id = "ControllerAppId" , PropertyType = typeof(ActionControllerApp))]
		public List<ActionControllerApp> ActionControllerApps  {set; get;}  
   
    }
}    
