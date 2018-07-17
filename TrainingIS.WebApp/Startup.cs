﻿using System;
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

        private void CreateDefaultRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(RoleBLO.Admin_ROLE))
            {
                var role = new IdentityRole();
                role.Name = RoleBLO.Admin_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.Trainee_ROLE))
            {
                var role = new IdentityRole();
                role.Name = RoleBLO.Trainee_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.Supervisor_ROLE))
            {
                var role = new IdentityRole();
                role.Name = RoleBLO.Supervisor_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.Former_ROLE))
            {
                var role = new IdentityRole();
                role.Name = RoleBLO.Former_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.PedagogicalDirector_ROLE))
            {
                var role = new IdentityRole();
                role.Name = RoleBLO.PedagogicalDirector_ROLE;
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists(RoleBLO.Director_ROLE))
            {
                var role = new IdentityRole();
                role.Name = RoleBLO.Director_ROLE;
                roleManager.Create(role);
            }
        }

        private void CreateDefaultUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Create Admin user
            string AdminUserName = "Admin";
            ApplicationUser AdminUser = userManager.FindByName(AdminUserName);
            if (AdminUser == null)
            {
                AdminUser = new ApplicationUser();
                AdminUser.UserName = AdminUserName;
                string userPWD = "Admin@123456";
                var chkUser = userManager.Create(AdminUser, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(AdminUser.Id, RoleBLO.Admin_ROLE);

                }
            }

            // Create Supervisor user
            string SupervisorUserName = "Supervisor";
            ApplicationUser SupervisorUser = userManager.FindByName(SupervisorUserName);
            if (SupervisorUser == null)
            {
                SupervisorUser = new ApplicationUser();
                SupervisorUser.UserName = SupervisorUserName;
                string userPWD = "Supervisor@123456";
                var chkUser = userManager.Create(SupervisorUser, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(SupervisorUser.Id, RoleBLO.Supervisor_ROLE);

                }
            }

            // Create PedagogicalDirector 
            string PedagogicalDirectorUserName = "PedagogicalDirector";
            ApplicationUser PedagogicalDirectorUser = userManager.FindByName(PedagogicalDirectorUserName);
            if (PedagogicalDirectorUser == null)
            {
                PedagogicalDirectorUser = new ApplicationUser();
                PedagogicalDirectorUser.UserName = PedagogicalDirectorUserName;
                string userPWD = "PedagogicalDirector@123456";
                var chkUser = userManager.Create(PedagogicalDirectorUser, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(PedagogicalDirectorUser.Id, RoleBLO.PedagogicalDirector_ROLE);

                }
            }

            // Create Director 
            string DirectorUserName = "Director";
            ApplicationUser DirectorUser = userManager.FindByName(DirectorUserName);
            if (DirectorUser == null)
            {
                DirectorUser = new ApplicationUser();
                DirectorUser.UserName = PedagogicalDirectorUserName;
                string userPWD = "Director@123456";
                var chkUser = userManager.Create(DirectorUser, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(DirectorUser.Id, RoleBLO.Director_ROLE);

                }
            }
        }

       

        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
           


        }
    }
}
