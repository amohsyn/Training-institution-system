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
    public class Trainings_Edit_ControllerTests : ManagerControllerTests
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Training_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training =  TestService.CreateOrLouadFirstTraining(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(training.Id) as ViewResult;
            var TrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingDetailModelView, typeof(Default_Form_Training_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Training_Post_Test()
        {

            // Arrange
            TrainingsController controller = new TrainingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Training training = TestService.CreateOrLouadFirstTraining(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            TrainingsControllerTests_Service.PreBindModel(controller, training, nameof(TrainingsController.Edit));
            TrainingsControllerTests_Service.ValidateViewModel(controller, training);

			Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Training_Model(training);
            var result = controller.Edit(Default_Form_Training_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Training_Post_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = TestService.CreateInValideTrainingInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (training == null) return;
            TrainingBLO trainingBLO = new TrainingBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            TrainingsControllerTests_Service.PreBindModel(controller, training, nameof(TrainingsController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingsControllerTests_Service
                .ValidateViewModel(controller, training);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Training_Model(training);
            var result = controller.Edit(Default_Form_Training_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingBLO.Validate(training);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

