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
    public class Delete_LogWorksControllerTests : ManagerControllerTests
    {
		LogWorksControllerTests_Service TestService = new LogWorksControllerTests_Service();

		[TestMethod()]
        public void Delete_LogWork_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(LogWork));
			 
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork = TestService.CreateOrLouadFirstLogWork(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(logwork.Id) as ViewResult;
            var LogWorkDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(LogWorkDetailModelView, typeof(Default_LogWorkDetailsView));
        }

        [TestMethod()]
        public void Delete_LogWork_Post_Test()
        {
            // Arrange
            //
            // Create LogWork to Delete
            LogWork logwork_to_delete = TestService.CreateValideLogWorkInstance();
            LogWorkBLO logworkBLO = new LogWorkBLO(new UnitOfWork());
            logworkBLO.Save(logwork_to_delete);
            LogWorksController controller = new LogWorksController();

            // Acte
            var result = controller.DeleteConfirmed(logwork_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_LogWork_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();

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

