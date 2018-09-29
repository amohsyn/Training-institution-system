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
using GApp.Entities.Resources.EntityPropertyShortcutResources;  
using GApp.Entities.Resources.AppResources;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(EntityPropertyShortcut))]
	[CreateView(typeof(EntityPropertyShortcut))]
    public class Default_Form_EntityPropertyShortcut_Model : BaseModel
    {
		[Required]
		[Display(Name = "EntityName", ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EntityName", SearchBy = "EntityName", OrderBy = "EntityName",  PropertyPath = "EntityName")]
		public String EntityName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyName", ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "PropertyName", SearchBy = "PropertyName", OrderBy = "PropertyName",  PropertyPath = "PropertyName")]
		public String PropertyName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyShortcutName", ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "PropertyShortcutName", SearchBy = "PropertyShortcutName", OrderBy = "PropertyShortcutName",  PropertyPath = "PropertyShortcutName")]
		public String PropertyShortcutName  {set; get;}  
   
		[Display(Name = "Description", ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
