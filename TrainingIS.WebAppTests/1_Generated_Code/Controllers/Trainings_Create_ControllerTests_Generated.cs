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
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.BLL.VO;
using GApp.BLL.Enums;
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_TrainingsControllerTests : ManagerControllerTests
    {
		TrainingsControllerTests_Service TestService = new TrainingsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            TrainingsController TrainingsController = new TrainingsController();

            ViewResult viewResult = TrainingsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Training_Post_Test()
        {
            //--Arrange--
            TrainingsController controller = new TrainingsController();
            Training training = TestService.CreateValideTrainingInstance();

            //--Acte--
            //
            TrainingsControllerTests_Service.PreBindModel(controller, training, nameof(TrainingsController.Create));
            TrainingsControllerTests_Service.ValidateViewModel(controller,training);

			Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Training_Model(training);
            var result = controller.Create(Default_Form_Training_Model);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_Training_Post_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = TestService.CreateInValideTrainingInstance();
            if (training == null) return;
            TrainingBLO trainingBLO = new TrainingBLO(controller._UnitOfWork);

            // Acte
            TrainingsControllerTests_Service.PreBindModel(controller, training, nameof(TrainingsController.Create));
            List<ValidationResult>  ls_validation_errors = TrainingsControllerTests_Service
                .ValidateViewModel(controller, training);

			Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Training_Model(training);
            var result = controller.Create(Default_Form_Training_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingBLO.Validate(training);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

