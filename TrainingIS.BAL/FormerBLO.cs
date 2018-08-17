using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Services.Identity;
using TrainingIS.BLL.Services.Import;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;
using TrainingIS.Entities.Resources.FormerResources;

namespace TrainingIS.BLL
{
    public partial class FormerBLO
    {

        public override Former CreateInstance()
        {
            var Former =  base.CreateInstance();
            Former.CreateUserAccount = false;
            return Former;
        }

        public override int Save(Former item)
        {
            if (item.CreateUserAccount)
            {
                ApplicationUserManager applicationUserManager = this.GAppContext.Session["ApplicationUserManager"] as ApplicationUserManager;
                if (applicationUserManager == null) throw new ArgumentNullException("ApplicationUserManager");
                this.CreateAccount_IfNotExit(item.Login, item.Password, applicationUserManager);
            }
            return base.Save(item);
        }

        /// <summary>
        /// After Save we create the user account for the former if not yet exist
        /// </summary>
        /// <param name="former"></param>
        /// <returns></returns>
        public void CreateAccount_IfNotExit(string Login, string Password, ApplicationUserManager ApplicationUserManager)
        {
            // Create User if not yet created

            // By default we create a user for the former that have email
            // login : email
            // password : matricule
            if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
            {
                string msg = string.Format("le mot de passe ou le nom d'utilisateur est null");
                throw new ArgumentException(msg);
            }

            UserBLO userBLO = new UserBLO(ApplicationUserManager);
            ApplicationUser user = userBLO.FindByLogin(Login);
            if (user == null)
            {
                user = new ApplicationUser();
                user.UserName = Login;
                user.Email = Login;
                userBLO.CreateUser(user, Password, RoleBLO.Former_ROLE);


            }

        }

        public int Delete(Former former, ApplicationUserManager ApplicationUserManager)
        {
            int return_value = base.Delete(former);

            // Delete the Former User

            UserBLO userBLO = new UserBLO(ApplicationUserManager);
            userBLO.DeleteUser(former.Login);
            return return_value;
        }
 
    }
}
