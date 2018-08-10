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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.Authorizations;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Delete_AuthrorizationAppsControllerTests : ManagerControllerTests
    {
		AuthrorizationAppsControllerTests_Service TestService = new AuthrorizationAppsControllerTests_Service();

		[TestMethod()]
        public void Delete_AuthrorizationApp_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = TestService.CreateOrLouadFirstAuthrorizationApp(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(authrorizationapp.Id) as ViewResult;
            var AuthrorizationAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AuthrorizationAppDetailModelView, typeof(Default_AuthrorizationAppDetailsView));
        }

        [TestMethod()]
        public void Delete_AuthrorizationApp_Post_Test()
        {
            // Arrange
            //
            // Create AuthrorizationApp to Delete
            AuthrorizationApp authrorizationapp_to_delete = TestService.CreateValideAuthrorizationAppInstance();
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(new UnitOfWork());
            authrorizationappBLO.Save(authrorizationapp_to_delete);
            AuthrorizationAppsController controller = new AuthrorizationAppsController();

            // Acte
            var result = controller.DeleteConfirmed(authrorizationapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_AuthrorizationApp_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        } 
    }
}

