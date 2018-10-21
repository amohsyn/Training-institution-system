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
	[DetailsView(typeof(EntityPropertyShortcut))]
	[IndexView(typeof(EntityPropertyShortcut))]
    public class Default_Details_EntityPropertyShortcut_Model : BaseModel
    {
		[Required]
		[Display(Name = "EntityName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "EntityName", SearchBy = "EntityName", OrderBy = "EntityName",  PropertyPath = "EntityName")]
		public String EntityName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "PropertyName", SearchBy = "PropertyName", OrderBy = "PropertyName",  PropertyPath = "PropertyName")]
		public String PropertyName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyShortcutName", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "PropertyShortcutName", SearchBy = "PropertyShortcutName", OrderBy = "PropertyShortcutName",  PropertyPath = "PropertyShortcutName")]
		public String PropertyShortcutName  {set; get;}  
   
		[Display(Name = "Description", GroupName = "SingularName", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(AutoGenerateFilter = false, FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  PropertyPath = "Description")]
		public String Description  {set; get;}  
   
    }
}    
