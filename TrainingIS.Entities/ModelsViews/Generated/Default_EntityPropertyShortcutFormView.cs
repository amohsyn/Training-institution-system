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

namespace TrainingIS.Entities.ModelsViews
{
    
    public class Default_EntityPropertyShortcutFormView : BaseModelView
    {
		[Required]
		public String EntityName  {set; get;}  
   
		[Required]
		public String PropertyName  {set; get;}  
   
		[Required]
		public String PropertyShortcutName  {set; get;}  
   
		public String Description  {set; get;}  
   
    }
}
