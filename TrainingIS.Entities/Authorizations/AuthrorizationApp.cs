using GApp.Core.MetaDatas.Attributes;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.Resources.ActionControllerAppResources;
using TrainingIS.Entities.Resources.AppResources;
using TrainingIS.Entities.Resources.AuthrorizationAppResources;
using TrainingIS.Entities.Resources.ControllerAppResources;
using TrainingIS.Entities.Resources.RoleAppResources;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = false)]
    public class AuthrorizationApp : BaseEntity
    {
        public override string ToString()
        {
            return this.Reference;
        }

        public override string CalculateReference()
        {
            string display_format = "{0}-{1}{2}";
            string role = this.RoleApp?.Code;
            string controller = this.ControllerApp?.Code;

            string actions = "";
            if (this.isAllAction == true)
                actions = msg_AuthrorizationApp.isAllAction;
            else
            {
                string actions_list = string.Join(",", this.ActionControllerApps?.Select(action => action.Name).ToList());
                actions = string.Format("[0]", actions_list);
            }

            string display = string.Format(display_format, role, controller, actions);
            return display;
        }

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

        [Display(Name = "PluralName", ResourceType = typeof(msg_ActionControllerApp))]
        [Many(userInterfaces = UserInterfaces.Checkbox)]
        
        public virtual List<ActionControllerApp> ActionControllerApps { get; set; }
    }
}
