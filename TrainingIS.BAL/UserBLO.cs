using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.BLL.Resources.UserBLO_Resources;
using TrainingIS.DAL;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public class UserBLO
    {
        ApplicationDbContext context = null;
        UserManager<ApplicationUser> UserManager = null;

        public UserBLO()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
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

        public void Add_Default_Users_And_Roles()
        {


        }
    }
}
