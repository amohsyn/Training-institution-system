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
using GApp.Entities.Resources.AppResources;  
using GApp.Entities.Resources.ControllerAppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(ActionControllerApp))]
	[CreateView(typeof(ActionControllerApp))]
    public class Default_Form_ActionControllerApp_Model : BaseModel
    {
		[Required]
		[Display(Name = "Code", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = true, FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_ControllerApp))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "ControllerAppId", SearchBy = "ControllerAppId", OrderBy = "ControllerAppId",  PropertyPath = "ControllerAppId")]
		public Int64 ControllerAppId  {set; get;}  
   
    }
}    
