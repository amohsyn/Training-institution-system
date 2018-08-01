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

namespace TrainingIS.Entities.ModelsViews
{
    
    public class AppControllerFormView : BaseModelView
    {
        [Required]
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(msg_app))]
        public string Description { set; get; }


        [Display(Name = "PluralName", ResourceType = typeof(msg_AppRole) , AutoGenerateField = false)]
        public virtual List<string> RolesIds { set; get; }
        [Display(AutoGenerateField = false)]
        public virtual List<SelectListItem> Roles { set; get; }
    }
}
