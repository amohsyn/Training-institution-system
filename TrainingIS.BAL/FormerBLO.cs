using GApp.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
using TrainingIS.DAL;
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
            Former.Birthdate = DateTime.Now;
            return Former;
        }

        public override int Save(Former item)
        {
            if (item.CreateUserAccount)
            {
                //if (!this.GAppContext.Session.ContainsKey("ApplicationUserManager"))
                //{
                //    string msg = string.Format("You must add un instance of {0} in the GAppContext with the key {1} befor you call the save method", 
                //        nameof(ApplicationUserManager),
                //        nameof(ApplicationUserManager)
                //        );
                //    throw new ArgumentNullException("ApplicationUserManager", msg);
                //}

                ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(this._UnitOfWork.context));

              
       

                this.CreateAccount_IfNotExit(item.Login, item.Password, manager);
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

            UserBLO userBLO = new UserBLO(this._UnitOfWork, this.GAppContext);
            ApplicationUser user = userBLO.FindByLogin(Login);
            if (user == null)
            {
                user = new ApplicationUser();
                user.UserName = Login;
                user.Email = Login;
                userBLO.CreateUser(user, Password, RoleBLO.Former_ROLE);


            }

        }

        public override int Delete(Former former)
        {
            int return_value = base.Delete(former);

            // Delete the Former User
            UserBLO userBLO = new UserBLO(this._UnitOfWork, this.GAppContext);
            userBLO.DeleteUser(former.Login);
            return return_value;
        }

        public Former Get_Current_Former()
        {
            string Current_User_Name = this.GAppContext.Current_User_Name;
            Former Current_Former = this._UnitOfWork.context.Formers
                .Where(f => f.Login == Current_User_Name).FirstOrDefault();

            return Current_Former;
        }

        #region Find
        public  Former Find_By_Full_Name(string Full_Name)
        {
            Full_Name = Full_Name.RemoveWhitespace();
            Full_Name = Full_Name.Replace("-", "").ToUpper();

            foreach (Former f in this.FindAll())
            {
                string db_Full_Name = f.FirstName + f.LastName;
                db_Full_Name = db_Full_Name.RemoveWhitespace();
                db_Full_Name = db_Full_Name.Replace("-", "").ToUpper();

                if (db_Full_Name == Full_Name)
                    return f;

            }
            return null;
        }

        public Former Find_By_Email(string email)
        {
            Former former = this._UnitOfWork.context.Formers
               .Where(f => f.Email == email)
               .FirstOrDefault();
            return former;
        }
        public List<Former> Find_By_FormerSpecialtyId(long formerSpecialtyId)
        {
            return this._UnitOfWork.context.Formers
               .Where(f => f.FormerSpecialtyId == formerSpecialtyId)
              .ToList();
        }
        #endregion
    }
}
