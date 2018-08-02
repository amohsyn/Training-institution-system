using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AuthrorizationAppResources;
using TrainingIS.Entities.Resources.ControllerAppResources;
using TrainingIS.Entities.Resources.RoleAppResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class AuthrorizationApp : BaseEntity
    {
        // RoleApp
        [Display(Name = "SingularName", ResourceType = typeof(msg_RoleApp))]
        public virtual RoleApp RoleApp { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_RoleApp))]
        public virtual Int64 RoleAppId { set; get; }

        // ControllerApp
        [Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
        public virtual ControllerApp ControllerApp { set; get; }
        [Required]
        [Display(Name = "SingularName", ResourceType = typeof(msg_ControllerApp))]
        public virtual Int64 ControllerAppId { set; get; }

        [Required]
        [Display(Name = "isAllAction", ResourceType = typeof(msg_AuthrorizationApp))]
        public bool isAllAction { set; get; }

        [Display(Name = "ActionControllerApps", ResourceType = typeof(msg_app))]
        public virtual List<ActionControllerApp> ActionControllerApps { get; set; }
    }
}
