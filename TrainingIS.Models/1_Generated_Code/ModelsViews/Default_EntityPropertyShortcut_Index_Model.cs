﻿using GApp.Core.Entities.ModelsViews;
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
	[IndexView(typeof(EntityPropertyShortcut))]
	[SearchBy("Reference")]
    public class Default_EntityPropertyShortcut_Index_Model : BaseModel
    {
		[Required]
		[Display(Name = "EntityName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(PropertyPath = "EntityName", FilterBy = "EntityName", SearchBy = "EntityName", OrderBy = "EntityName",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String EntityName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(PropertyPath = "PropertyName", FilterBy = "PropertyName", SearchBy = "PropertyName", OrderBy = "PropertyName",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String PropertyName  {set; get;}  
   
		[Required]
		[Display(Name = "PropertyShortcutName", Order = 0, ResourceType = typeof(msg_EntityPropertyShortcut))]
		[GAppDataTable(PropertyPath = "PropertyShortcutName", FilterBy = "PropertyShortcutName", SearchBy = "PropertyShortcutName", OrderBy = "PropertyShortcutName",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String PropertyShortcutName  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true , isOrderBy = true, isSeachBy =true)]
		public String Description  {set; get;}  
   
    }
}    
