using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class FormerBLO
    {
        /// <summary>
        /// After Save we create the user account for the former if not yet exist
        /// </summary>
        /// <param name="former"></param>
        /// <returns></returns>
        public void CreateAccount_IfNotExit(string Login, string Password)
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

            UserBLO userBLO = new UserBLO();
            ApplicationUser user = userBLO.FindByLogin(Login);
            if (user == null)
            {
                user = new ApplicationUser();
                user.UserName = Login;

                userBLO.CreateUser(user, Password, RoleBLO.Former_ROLE);


            }

        }

        public override int Delete(long id)
        {
            int return_value = base.Delete(id);


            // Delete the Former User
            UserBLO userBLO = new UserBLO();
            userBLO.DeleteUser();

            return return_value;
        }
    }
}
