using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;
using TrainingIS.WebApp.Models;

[assembly: OwinStartupAttribute(typeof(TrainingIS.WebApp.Startup))]
namespace TrainingIS.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            this.CreateDefaultRoles();
            this.CreateDefaultUsers();
             
        }

        private void CreateDefaultUsers()
        {
            throw new NotImplementedException();
        }

        private void CreateDefaultRoles()
        {
            throw new NotImplementedException();
        }

        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);



            }

            string AdminUserNameAndEmail = "essarraj.fouad.csharp@gmail.com";
            ApplicationUser user = userManager.FindByName(AdminUserNameAndEmail);
            if (user == null)
            {			
                user = new ApplicationUser();
                user.UserName = AdminUserNameAndEmail;
                user.Email = AdminUserNameAndEmail;

                string userPWD = "Admin@123456";

                var chkUser = userManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating default roles

            if (!roleManager.RoleExists(RoleBLO.Trainee_ROLE))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleBLO.Trainee_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.Supervisor_ROLE))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleBLO.Supervisor_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.Former_ROLE))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleBLO.Former_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.PedagogicalDirectorFORMER_ROLE))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleBLO.PedagogicalDirectorFORMER_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.Director_ROLE))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = RoleBLO.Director_ROLE;
                roleManager.Create(role);
            }


        }
    }
}
