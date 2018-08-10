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
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Delete_TrainingsControllerTests : ManagerControllerTests
    {
		TrainingsControllerTests_Service TestService = new TrainingsControllerTests_Service();

		[TestMethod()]
        public void Delete_Training_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = TestService.CreateOrLouadFirstTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(training.Id) as ViewResult;
            var TrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingDetailModelView, typeof(Default_TrainingDetailsView));
        }

        [TestMethod()]
        public void Delete_Training_Post_Test()
        {
            // Arrange
            //
            // Create Training to Delete
            Training training_to_delete = TestService.CreateValideTrainingInstance();
            TrainingBLO trainingBLO = new TrainingBLO(new UnitOfWork());
            trainingBLO.Save(training_to_delete);
            TrainingsController controller = new TrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(training_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Training_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();

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

