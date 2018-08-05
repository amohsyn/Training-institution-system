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
    /// <summary>
    /// Authorization permission manager
    /// it well be instantiated by the filter SecrurituFilter in the Controller instance
    /// if the filtter is not called the controller well create an empty HasPermission instance with All autorizations 
    /// </summary>
    public class HasPermission
    {
        private List<RoleApp> _UserRoles = null;
        private List<AuthrorizationApp> _AuthrorizationApps = null;
        private string CurrentControllerName { get; set; }
        private IPrincipal User { get; set; }
        private bool Allow_All { get; set; }


        public HasPermission()
        {
            this.Allow_All = true;
        } 

        public HasPermission(IPrincipal User, string CurrentControllerName)
        {
            // Allow All for Root User
            if(User.IsInRole(RoleBLO.Root_ROLE)) {
                this.Allow_All = true;
            }else {
                this.Allow_All = false;
            }
            
            if (string.IsNullOrEmpty(CurrentControllerName))
            {
                string msg = string.Format("You can't create instance with empty {0} " , nameof(Security.HasPermission));
                throw new ArgumentException(msg);
            }
               
            this.CurrentControllerName = CurrentControllerName;
            this.User = User;
            this.InitUserRole();
            this.InitAuthrorizationApps();

        }

        private void InitAuthrorizationApps()
        {
            this._AuthrorizationApps = new List<AuthrorizationApp>();
            AuthrorizationAppBLO authrorizationAppBLO = new AuthrorizationAppBLO(new UnitOfWork());

            foreach (var rolleApp in this._UserRoles)
            {
                this._AuthrorizationApps.AddRange(authrorizationAppBLO.FindAll(rolleApp));
            }
            
        }

        private void InitUserRole()
        {
            if(this.User != null)
            {
                RoleAppBLO appRoleBLO = new RoleAppBLO(new UnitOfWork());
                this._UserRoles = appRoleBLO
                    .FindAll()
                    .Where(R => this.User.IsInRole(R.Code))
                    .ToList();
            }
            if (this.User == null || this._UserRoles == null) this._UserRoles = new List<RoleApp>();
        }

        public bool ToController(String ControllerName)
        {
            if (this.Allow_All) return this.Allow_All;

            bool permission = (this._AuthrorizationApps
                .Where(authrorizationApp => authrorizationApp.ControllerApp.Code == ControllerName)
                .FirstOrDefault() != null);
             
            return permission;
        }
        public bool ToAction(String ActionName)
        {
            if (this.Allow_All) return this.Allow_All;

            bool permission = (this._AuthrorizationApps
                .Where(authrorizationApp => authrorizationApp.ControllerApp.Code == this.CurrentControllerName)
                .FirstOrDefault() != null);


            return false;
        }
        public bool ToAction(String ControllerName, String ActionName)
        {
            if (this.Allow_All) return this.Allow_All;

            bool permission = (this._AuthrorizationApps
               .Where(authrorizationApp => authrorizationApp.ControllerApp.Code == ControllerName)
               .Where(authrorizationApp => authrorizationApp.isAllAction ==true)
               .FirstOrDefault() != null);

            if(permission) return permission;

            permission = (this._AuthrorizationApps
               .Where(authrorizationApp => authrorizationApp.ControllerApp.Code == this.CurrentControllerName)
               .Where(authrorizationApp => authrorizationApp.ActionControllerApps.Select(action => action.Code).Contains(ActionName))
               .FirstOrDefault() != null);


            return permission;
        }

    }
}