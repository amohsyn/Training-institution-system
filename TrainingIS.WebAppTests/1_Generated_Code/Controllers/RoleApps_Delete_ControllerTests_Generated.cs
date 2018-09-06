using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.WebApp.Tests.ViewModels;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using GApp.Entities;
using GApp.BLL.Enums;
using GApp.BLL.VO;
using GApp.DAL;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
	[CleanTestDB]
    public class RoleApps_Delete_ControllerTests : ManagerControllerTests
    {
		RoleAppsControllerTests_Service TestService = new RoleAppsControllerTests_Service();

		[TestMethod()]
        public void RoleApps_Delete_ControllerTests_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = TestService.CreateOrLouadFirstRoleApp(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(roleapp.Id) as ViewResult;
            var RoleAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(RoleAppDetailModelView, typeof(Default_Details_RoleApp_Model));
        }

        [TestMethod()]
        public void Delete_RoleApp_Post_Test()
        {
            // Arrange
            //
            // Create RoleApp to Delete
			            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp_to_delete = TestService.CreateValideRoleAppInstance(controller._UnitOfWork,controller.GAppContext);
            RoleAppBLO roleappBLO = new RoleAppBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            roleappBLO.Save(roleapp_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(roleapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_RoleApp_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

