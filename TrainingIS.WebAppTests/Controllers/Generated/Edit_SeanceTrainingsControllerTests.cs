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
    public class Edit_SeanceTrainingsControllerTests : ManagerControllerTests
    {
		SeanceTrainingsControllerTests_Service TestService = new SeanceTrainingsControllerTests_Service();

		[TestMethod()]
        public void EditGet_SeanceTraining_Not_Exist_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceTraining_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining =  TestService.CreateOrLouadFirstSeanceTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seancetraining.Id) as ViewResult;
            var SeanceTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceTrainingDetailModelView, typeof(Default_SeanceTrainingFormView));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceTraining_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceTraining));

            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceTraining seancetraining = TestService.CreateOrLouadFirstSeanceTraining(new UnitOfWork());
			 
       

            // Acte
            SeanceTrainingsControllerTests_Service.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Edit));
            SeanceTrainingsControllerTests_Service.ValidateViewModel(controller, seancetraining);

			Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceTrainingFormView(seancetraining);
            var result = controller.Edit(Default_SeanceTrainingFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceTraining_Post_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = TestService.CreateInValideSeanceTrainingInstance_ForEdit(new UnitOfWork());
            if (seancetraining == null) return;
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(controller._UnitOfWork);

            // Acte
            SeanceTrainingsControllerTests_Service.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceTrainingsControllerTests_Service
                .ValidateViewModel(controller, seancetraining);

			Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceTrainingFormView(seancetraining);
            var result = controller.Edit(Default_SeanceTrainingFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancetrainingBLO.Validate(seancetraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

