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
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
	[EditView(typeof(EntityPropertyShortcut))]
	[CreateView(typeof(EntityPropertyShortcut))]
    public class Default_Form_EntityPropertyShortcut_Model : BaseModel
    {
		[Required]
		[Display(Name = "EntityName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(PropertyPath = "EntityName", FilterBy = "EntityName", SearchBy = "EntityName", OrderBy = "EntityName",  AutoGenerateFilter = false,isColumn = true )]
		public String EntityName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(PropertyPath = "PropertyName", FilterBy = "PropertyName", SearchBy = "PropertyName", OrderBy = "PropertyName",  AutoGenerateFilter = false,isColumn = true )]
		public String PropertyName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyShortcutName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(PropertyPath = "PropertyShortcutName", FilterBy = "PropertyShortcutName", SearchBy = "PropertyShortcutName", OrderBy = "PropertyShortcutName",  AutoGenerateFilter = false,isColumn = true )]
		public String PropertyShortcutName  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
