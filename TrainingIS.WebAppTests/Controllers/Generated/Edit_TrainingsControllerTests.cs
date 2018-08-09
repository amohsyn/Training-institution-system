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
    public class Edit_TrainingsControllerTests : ManagerControllerTests
    {
		TrainingsControllerTests_Service TestService = new TrainingsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Training_Not_Exist_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Training_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training =  TestService.CreateOrLouadFirstTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(training.Id) as ViewResult;
            var TrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingDetailModelView, typeof(TrainingFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Training_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Training));

            // Arrange
            TrainingsController controller = new TrainingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Training training = TestService.CreateOrLouadFirstTraining(new UnitOfWork());
			 
       

            // Acte
            TrainingsControllerTests_Service.PreBindModel(controller, training, nameof(TrainingsController.Edit));
            TrainingsControllerTests_Service.ValidateViewModel(controller, training);

			TrainingFormView TrainingFormView = new TrainingFormViewBLM(controller._UnitOfWork).ConverTo_TrainingFormView(training);
            var result = controller.Edit(TrainingFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Training_Post_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = TestService.CreateInValideTrainingInstance_ForEdit(new UnitOfWork());
            if (training == null) return;
            TrainingBLO trainingBLO = new TrainingBLO(controller._UnitOfWork);

            // Acte
            TrainingsControllerTests_Service.PreBindModel(controller, training, nameof(TrainingsController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingsControllerTests_Service
                .ValidateViewModel(controller, training);

			TrainingFormView TrainingFormView = new TrainingFormViewBLM(controller._UnitOfWork).ConverTo_TrainingFormView(training);
            var result = controller.Edit(TrainingFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingBLO.Validate(training);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

