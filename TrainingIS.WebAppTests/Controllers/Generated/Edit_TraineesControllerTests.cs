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
    public class Edit_TraineesControllerTests : ManagerControllerTests
    {
		TraineesControllerTests_Service TestService = new TraineesControllerTests_Service();

		[TestMethod()]
        public void EditGet_Trainee_Not_Exist_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Trainee_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee =  TestService.CreateOrLouadFirstTrainee(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(trainee.Id) as ViewResult;
            var TraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Default_TraineeFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Trainee_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Trainee));

            // Arrange
            TraineesController controller = new TraineesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Trainee trainee = TestService.CreateOrLouadFirstTrainee(new UnitOfWork());
			 
       

            // Acte
            TraineesControllerTests_Service.PreBindModel(controller, trainee, nameof(TraineesController.Edit));
            TraineesControllerTests_Service.ValidateViewModel(controller, trainee);

			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TraineeFormView(trainee);
            var result = controller.Edit(Default_TraineeFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Trainee_Post_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = TestService.CreateInValideTraineeInstance_ForEdit(new UnitOfWork());
            if (trainee == null) return;
            TraineeBLO traineeBLO = new TraineeBLO(controller._UnitOfWork);

            // Acte
            TraineesControllerTests_Service.PreBindModel(controller, trainee, nameof(TraineesController.Edit));
            List<ValidationResult> ls_validation_errors = TraineesControllerTests_Service
                .ValidateViewModel(controller, trainee);

			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TraineeFormView(trainee);
            var result = controller.Edit(Default_TraineeFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = traineeBLO.Validate(trainee);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

