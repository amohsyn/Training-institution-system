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
    public class UserBLO
    {
        TrainingISModel context = null;
        ApplicationUserManager UserManager = null;
        UnitOfWork<TrainingISModel> UnitOfWork;
        GAppContext GAppContext;

        /// <summary>
        ///  applicationUserManager is created by  HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        ///  in the controller
        /// </summary>
        /// <param name="applicationUserManager"></param>
        public UserBLO(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext, ApplicationUserManager applicationUserManager)
        {
            this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;
            context = this.UnitOfWork.context;
            UserManager = applicationUserManager;
              //  new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public ApplicationUser FindByLogin(string userName)
        {
            ApplicationUser user =  UserManager.FindByName(userName);
            //var query = from u in context.Users where u.UserName == userName select u ;
            //var user = query.FirstOrDefault();
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

        
    }
}
