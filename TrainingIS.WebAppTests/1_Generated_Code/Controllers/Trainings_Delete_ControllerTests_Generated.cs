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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
    public class Trainings_Delete_ControllerTests : ManagerControllerTests
    {
		TrainingsControllerTests_Service TestService = new TrainingsControllerTests_Service();

		[TestMethod()]
        public void Trainings_Delete_ControllerTests_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = TestService.CreateOrLouadFirstTraining(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(training.Id) as ViewResult;
            var TrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingDetailModelView, typeof(Default_Details_Training_Model));
        }

        [TestMethod()]
        public void Delete_Training_Post_Test()
        {
            // Arrange
            //
            // Create Training to Delete
			            TrainingsController controller = new TrainingsController();
            Training training_to_delete = TestService.CreateValideTrainingInstance(controller._UnitOfWork,controller.GAppContext);
            TrainingBLO trainingBLO = new TrainingBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            trainingBLO.Save(training_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(training_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

