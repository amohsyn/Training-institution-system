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
	[DetailsView(typeof(ActionControllerApp))]
    public class Default_Details_ActionControllerApp_Model : BaseModel
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
   
    }
}    
