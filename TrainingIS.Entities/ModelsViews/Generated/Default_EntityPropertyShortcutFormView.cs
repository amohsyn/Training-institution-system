using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AppRoleResources;
using GApp.Core.MetaDatas.Attributes; 
using TrainingIS.Entities.Resources.EntityPropertyShortcutResources; 
using TrainingIS.Entities.Resources.AppResources; 

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_EntityPropertyShortcutFormView : BaseModelView
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
