using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TrainingIS.BLL;
using TrainingIS.Entities;
using GApp.BLL;
using TrainingIS.DAL;

namespace TrainingIS.WebApp.Security
{
    public class HasPermission
    {
        private List<RoleApp> UserRoles = null;
        private string CurrentControllerName { get; set; }
        private IPrincipal User { get; set; }
        private bool Allow_All { get; set; }


        public HasPermission()
        {
            // By default Allow All Access
            this.Allow_All = true;
        } 

        public void InitAutorizationFor(IPrincipal User, string CurrentControllerName)
        {
            // Allow All for Root User
            if(User.IsInRole(RoleBLO.Root_ROLE)) {
                this.Allow_All = true;
            }else {
                this.Allow_All = false;
            }
            
            if (string.IsNullOrEmpty(CurrentControllerName))
            {
                string msg = string.Format("You can't create instance with empty {0} " , nameof(HasPermission));
                throw new ArgumentException(msg);
            }
               
            this.CurrentControllerName = CurrentControllerName;
            this.User = User;
            this.InitUserRole();

        }

        private void InitUserRole()
        {
            if(this.User != null)
            {
                RoleAppBLO appRoleBLO = new RoleAppBLO(new UnitOfWork());
                this.UserRoles = appRoleBLO
                    .FindAll()
                    .Where(R => this.User.IsInRole(R.Code))
                    .ToList();
            }
            if (this.User == null || this.UserRoles == null) this.UserRoles = new List<RoleApp>();
        }

        public bool ToController(String ControllerName)
        {
            if (this.Allow_All) return this.Allow_All;
            //bool permission = this.UserRoles
            //    .Where(Role => Role.AppControllers
            //                        .Where(controller => controller.Code == ControllerName)
            //                        .FirstOrDefault() != null)
            //    .FirstOrDefault() != null;
            //return permission;
            return false;
        }
        public bool ToAction(String ActionName)
        {
            if (this.Allow_All) return this.Allow_All;
            //bool permission = this.UserRoles
            //     .Where(Role => Role.AppControllerActions
            //                         .Where(Action => Action.Code == ActionName && Action.AppController.Code == this.CurrentControllerName)
            //                         .FirstOrDefault() != null)
            //     .FirstOrDefault() != null;
            //return permission;
            return false;
        }
        public bool ToAction(String ControllerName, String ActionName)
        {
            if (this.Allow_All) return this.Allow_All;
            //bool permission = this.UserRoles
            //    .Where(Role => Role.AppControllerActions
            //                        .Where(Action => Action.Code == ActionName && Action.AppController.Code == ControllerName)
            //                        .FirstOrDefault() != null)
            //    .FirstOrDefault() != null;
            //return permission;
            return false;
        }

    }
}