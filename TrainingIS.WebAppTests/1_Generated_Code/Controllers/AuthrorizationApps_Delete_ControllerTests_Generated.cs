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
    public class AuthrorizationApps_Delete_ControllerTests : ManagerControllerTests
    {
		AuthrorizationAppsControllerTests_Service TestService = new AuthrorizationAppsControllerTests_Service();

		[TestMethod()]
        public void AuthrorizationApps_Delete_ControllerTests_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = TestService.CreateOrLouadFirstAuthrorizationApp(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(authrorizationapp.Id) as ViewResult;
            var AuthrorizationAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AuthrorizationAppDetailModelView, typeof(Default_Details_AuthrorizationApp_Model));
        }

        [TestMethod()]
        public void Delete_AuthrorizationApp_Post_Test()
        {
            // Arrange
            //
            // Create AuthrorizationApp to Delete
			            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp_to_delete = TestService.CreateValideAuthrorizationAppInstance(controller._UnitOfWork,controller.GAppContext);
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            authrorizationappBLO.Save(authrorizationapp_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(authrorizationapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

