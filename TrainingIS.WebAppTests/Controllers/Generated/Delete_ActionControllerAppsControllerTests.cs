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
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Delete_ActionControllerAppsControllerTests : ManagerControllerTests
    {
		ActionControllerAppsControllerTests_Service TestService = new ActionControllerAppsControllerTests_Service();

		[TestMethod()]
        public void Delete_ActionControllerApp_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ActionControllerApp));
			 
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = TestService.CreateOrLouadFirstActionControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(actioncontrollerapp.Id) as ViewResult;
            var ActionControllerAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ActionControllerAppDetailModelView, typeof(Default_ActionControllerAppDetailsView));
        }

        [TestMethod()]
        public void Delete_ActionControllerApp_Post_Test()
        {
            // Arrange
            //
            // Create ActionControllerApp to Delete
            ActionControllerApp actioncontrollerapp_to_delete = TestService.CreateValideActionControllerAppInstance();
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(new UnitOfWork());
            actioncontrollerappBLO.Save(actioncontrollerapp_to_delete);
            ActionControllerAppsController controller = new ActionControllerAppsController();

            // Acte
            var result = controller.DeleteConfirmed(actioncontrollerapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ActionControllerApp_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();

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

