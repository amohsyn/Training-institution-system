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
using GApp.Entities.Resources.EntityPropertyShortcutResources; 
using GApp.Entities.Resources.AppResources; 
 
namespace TrainingIS.Entities.ModelsViews
{
	[DetailsView(typeof(EntityPropertyShortcut))]
	[IndexView(typeof(EntityPropertyShortcut))]
    public class Default_Details_EntityPropertyShortcut_Model : BaseModel
    {
		[Required]
		[Display(Name = "EntityName", ResourceType = typeof(msg_EntityPropertyShortcut))]
		public String EntityName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyName", ResourceType = typeof(msg_EntityPropertyShortcut))]
		public String PropertyName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyShortcutName", ResourceType = typeof(msg_EntityPropertyShortcut))]
		public String PropertyShortcutName  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		public String Description  {set; get;}  
   
    }
}    
