using GApp.Core.Context;
using GApp.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.Resources.UserBLO_Resources;
using TrainingIS.BLL.Services.Identity;
using TrainingIS.DAL;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public class UserBLO : Base_NotDb_BLO
    {
        public static string ApplicationUserManager_Key = "ApplicationUserManager";

        public void ThrowException_If_ApplicationUserManager_Not_In_GAppContext_Session()
        {
            // Find UnitOfWork from GAppContext
            if (!this.GAppContext.Session.ContainsKey(UserBLO.ApplicationUserManager_Key))
            {
                string msg_ex = string.Format("The GAppContext Session does not have '{0}' key ", UserBLO.ApplicationUserManager_Key);
                throw new ArgumentException(msg_ex, nameof(GAppContext));
            }
        }

      

        ApplicationUserManager UserManager = null;

        /// <summary>
        ///  applicationUserManager is created by  HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        ///  in the controller
        /// </summary>
        public UserBLO(GAppContext GAppContext): base(GAppContext)
        {
            this.ThrowException_If_ApplicationUserManager_Not_In_GAppContext_Session();
            UserManager = this.GAppContext.Session[UserBLO.ApplicationUserManager_Key] as ApplicationUserManager;
        
        }
        public UserBLO(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) : base(GAppContext)
        {
            this.UnitOfWork = unitOfWork;
            this.ThrowException_If_ApplicationUserManager_Not_In_GAppContext_Session();
            UserManager = this.GAppContext.Session[UserBLO.ApplicationUserManager_Key] as ApplicationUserManager;
        }

        public ApplicationUser FindByLogin(string userName)
        {
            // ApplicationUser user =  UserManager.FindByName(userName);
            var query = from u in this.UnitOfWork.context.Users where u.UserName == userName select u;
            var user = query.FirstOrDefault();
            return user;
        }

        public void CreateUser(ApplicationUser applicationUser, string password, string role)
        {
           IdentityResult identityResult =  UserManager.Create(applicationUser, password);
            if (identityResult.Succeeded)
            {
                UserManager.AddToRole(applicationUser.Id, role);
            }
            else
            {
                string msg = msg_UserBLO.Create_user_errors;
                msg += "-" +  String.Join(",", identityResult.Errors.ToList<string>());
                throw new CreateUserException(msg);
            }
        }

        public void DeleteUser(string userName)
        {
            ApplicationUser user = UserManager.FindByName(userName);
            UserManager.Delete(user);
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="UserName">User Id</param>
        /// <returns>The new password</returns>
        public IdentityResult ResetPassword(ApplicationUser User, out string new_password)
        {
            new_password = "User@123456";
            ApplicationUser ApplicationUser = this.FindByLogin(User.UserName);
            if (ApplicationUser == null) throw new ArgumentNullException(nameof(User.UserName));

            string Role = this.UserManager.GetRoles(User.Id)?.First();
            if(Role != null)
            {
                new_password = new_password.Replace("User", Role);
            }

            this.UserManager.RemovePassword(User.Id);
            IdentityResult identityResult = this.UserManager.AddPassword(User.Id, new_password);
            return identityResult;
        }


        public bool Is_Current_User_Has_Role(string former_ROLE)
        {
            string Current_User_Name = this.GAppContext.Current_User_Name;
            ApplicationUser ApplicationUser = this.FindByLogin(Current_User_Name);
            var roles = this.UserManager.GetRoles(ApplicationUser.Id);
            return roles.Contains(former_ROLE);
        }


    }
}
