using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AppRoleResources;

namespace TrainingIS.Entities.ModelsViews
{

    public class AppControllerDetailsView
    {
        [Display(Name = "Code", ResourceType = typeof(msg_app))]
        public string Code { get; set; }

        [Display(Name = "PluralName", ResourceType = typeof(msg_AppRole))]
        public string Roles { set; get; }
    }
}
