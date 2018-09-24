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
using GApp.Entities.Resources.AppResources;  
using GApp.Entities.Resources.ControllerAppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(ActionControllerApp))]
	[CreateView(typeof(ActionControllerApp))]
    public class Default_Form_ActionControllerApp_Model : BaseModel
    {
		[Required]
		[Display(Name = "Code", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Code")]
		public String Code  {set; get;}  
   
		[Required]
		[Display(Name = "Name", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Name")]
		public String Name  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "Description")]
		public String Description  {set; get;}  
   
		[Required]
		[Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
		[GAppDataTable(AutoGenerateFilter =false, PropertyPath = "ControllerAppId")]
		public Int64 ControllerAppId  {set; get;}  
   
    }
}    
