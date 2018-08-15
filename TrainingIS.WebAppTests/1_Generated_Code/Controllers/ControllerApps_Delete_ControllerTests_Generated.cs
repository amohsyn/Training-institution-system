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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
    public class ControllerApps_Delete_ControllerTests : ManagerControllerTests
    {
		ControllerAppsControllerTests_Service TestService = new ControllerAppsControllerTests_Service();

		[TestMethod()]
        public void ControllerApps_Delete_ControllerTests_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = TestService.CreateOrLouadFirstControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(controllerapp.Id) as ViewResult;
            var ControllerAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ControllerAppDetailModelView, typeof(Default_Details_ControllerApp_Model));
        }

        [TestMethod()]
        public void Delete_ControllerApp_Post_Test()
        {
            // Arrange
            //
            // Create ControllerApp to Delete
            ControllerApp controllerapp_to_delete = TestService.CreateValideControllerAppInstance();
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(new UnitOfWork<TrainingISModel>());
            controllerappBLO.Save(controllerapp_to_delete);
            ControllerAppsController controller = new ControllerAppsController();

            // Acte
            var result = controller.DeleteConfirmed(controllerapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ControllerApp_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();

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

