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
    public class Trainees_Edit_ControllerTests : ManagerControllerTests
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
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
			Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Default_Form_Trainee_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Trainee_Post_Test()
        {

            // Arrange
            TraineesController controller = new TraineesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Trainee trainee = TestService.CreateOrLouadFirstTrainee(new UnitOfWork<TrainingISModel>());
			 
       

            // Acte
            TraineesControllerTests_Service.PreBindModel(controller, trainee, nameof(TraineesController.Edit));
            TraineesControllerTests_Service.ValidateViewModel(controller, trainee);

			Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Trainee_Model(trainee);
            var result = controller.Edit(Default_Form_Trainee_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Trainee_Post_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = TestService.CreateInValideTraineeInstance_ForEdit(new UnitOfWork<TrainingISModel>());
            if (trainee == null) return;
            TraineeBLO traineeBLO = new TraineeBLO(controller._UnitOfWork);

            // Acte
            TraineesControllerTests_Service.PreBindModel(controller, trainee, nameof(TraineesController.Edit));
            List<ValidationResult> ls_validation_errors = TraineesControllerTests_Service
                .ValidateViewModel(controller, trainee);

			Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Trainee_Model(trainee);
            var result = controller.Edit(Default_Form_Trainee_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = traineeBLO.Validate(trainee);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

