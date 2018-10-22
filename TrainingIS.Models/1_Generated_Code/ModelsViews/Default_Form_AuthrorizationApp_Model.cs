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
	[EditView(typeof(AuthrorizationApp))]
	[CreateView(typeof(AuthrorizationApp))]
    public class Default_Form_AuthrorizationApp_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_RoleApp))]
		[GAppDataTable(PropertyPath = "RoleAppId", FilterBy = "RoleAppId", SearchBy = "RoleAppId", OrderBy = "RoleAppId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 RoleAppId  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_ControllerApp))]
		[GAppDataTable(PropertyPath = "ControllerAppId", FilterBy = "ControllerAppId", SearchBy = "ControllerAppId", OrderBy = "ControllerAppId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 ControllerAppId  {set; get;}  
   
		[Required]
		[Display(Name = "isAllAction", Order = 0, ResourceType = typeof(msg_AuthrorizationApp))]
		[GAppDataTable(PropertyPath = "isAllAction", FilterBy = "isAllAction", SearchBy = "isAllAction", OrderBy = "isAllAction",  AutoGenerateFilter = false,isColumn = true )]
		public Boolean isAllAction  {set; get;}  
   
		[Many(userInterfaces = UserInterfaces.Checkbox , TypeOfEntity = typeof(ActionControllerApp))]
		[Display(Name = "PluralName", Order = 0, ResourceType = typeof(msg_ActionControllerApp))]
		[GAppDataTable(PropertyPath = "ActionControllerApps", FilterBy = "", SearchBy = "", OrderBy = "ActionControllerApps.Count",  AutoGenerateFilter = false,isColumn = true )]
		[SelectFilter(Filter_HTML_Id = "ControllerAppId" , PropertyType = typeof(ActionControllerApp))]
		public List<String> Selected_ActionControllerApps {set; get;}  
   
    }
}    
