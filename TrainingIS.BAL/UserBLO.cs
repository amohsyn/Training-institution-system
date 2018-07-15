using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
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
            UserManager.Create(applicationUser, password);
            UserManager.AddToRole(applicationUser.Id, role);
        }
    }
}
