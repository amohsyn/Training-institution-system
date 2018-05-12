using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TrainingIS.WebApp.Models;

namespace shanuMVCUserRoles.Controllers
{
	[Authorize(Roles ="Admin")]
	public class UsersController : Controller
    {

        ApplicationDbContext context;

        public UsersController()
        {
            context = new ApplicationDbContext();
        }


        // GET: Users
        public Boolean isAdminUser()
		{
			if (User.Identity.IsAuthenticated)
			{
				var user = User.Identity;
				ApplicationDbContext context = new ApplicationDbContext();
				var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
				var s = UserManager.GetRoles(user.GetUserId());
				if (s.Count > 0 &&   s[0].ToString() == "Admin")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}


		public ActionResult Index()
		{

            var role = (from r in context.Roles where r.Name.Contains("Trainee") select r).FirstOrDefault();
            var users = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var userVM = users.Select(user => new UserViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                RoleName = "Trainee"
            }).ToList();


            var role2 = (from r in context.Roles where r.Name.Contains("Admin") select r).FirstOrDefault();
            var admins = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role2.Id)).ToList();

            var adminVM = admins.Select(user => new UserViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                RoleName = "Admin"
            }).ToList();


            var model = new GroupedUserViewModel { Users = userVM, Admins = adminVM };
            return View(model);

            //if (User.Identity.IsAuthenticated)
            //{
            //	var user = User.Identity;
            //	ViewBag.Name = user.Name;
            //	//	ApplicationDbContext context = new ApplicationDbContext();
            //	//	var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //	//var s=	UserManager.GetRoles(user.GetUserId());
            //	ViewBag.displayMenu = "No";

            //	if (isAdminUser())
            //	{
            //		ViewBag.displayMenu = "Yes";
            //	}
            //	return View();
            //}
            //else
            //{
            //	ViewBag.Name = "Not Logged IN";
            //}


            //return View();


        }


       
    }
}