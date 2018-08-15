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
    public class Create_TraineesControllerTests : ManagerControllerTests
    {
		TraineesControllerTests_Service TestService = new TraineesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            TraineesController TraineesController = new TraineesController();

            ViewResult viewResult = TraineesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Trainee_Post_Test()
        {
            //--Arrange--
            TraineesController controller = new TraineesController();
            Trainee trainee = TestService.CreateValideTraineeInstance();

            //--Acte--
            //
            TraineesControllerTests_Service.PreBindModel(controller, trainee, nameof(TraineesController.Create));
            TraineesControllerTests_Service.ValidateViewModel(controller,trainee);

			Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Trainee_Model(trainee);
            var result = controller.Create(Default_Form_Trainee_Model);
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
        public void Create_InValide_Trainee_Post_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = TestService.CreateInValideTraineeInstance();
            if (trainee == null) return;
            TraineeBLO traineeBLO = new TraineeBLO(controller._UnitOfWork);

            // Acte
            TraineesControllerTests_Service.PreBindModel(controller, trainee, nameof(TraineesController.Create));
            List<ValidationResult>  ls_validation_errors = TraineesControllerTests_Service
                .ValidateViewModel(controller, trainee);

			Default_Form_Trainee_Model Default_Form_Trainee_Model = new Default_Form_Trainee_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Trainee_Model(trainee);
            var result = controller.Create(Default_Form_Trainee_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = traineeBLO.Validate(trainee);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

