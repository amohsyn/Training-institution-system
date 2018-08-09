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
    public class Create_ControllerAppsControllerTests : ManagerControllerTests
    {
		ControllerAppsControllerTests_Service TestService = new ControllerAppsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            ControllerAppsController ControllerAppsController = new ControllerAppsController();

            ViewResult viewResult = ControllerAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ControllerApp_Post_Test()
        {
            //--Arrange--
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = TestService.CreateValideControllerAppInstance();

            //--Acte--
            //
            ControllerAppsControllerTests_Service.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Create));
            ControllerAppsControllerTests_Service.ValidateViewModel(controller,controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Create(Default_ControllerAppFormView);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_ControllerApp_Post_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = TestService.CreateInValideControllerAppInstance();
            if (controllerapp == null) return;
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(controller._UnitOfWork);

            // Acte
            ControllerAppsControllerTests_Service.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Create));
            List<ValidationResult>  ls_validation_errors = ControllerAppsControllerTests_Service
                .ValidateViewModel(controller, controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Create(Default_ControllerAppFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = controllerappBLO.Validate(controllerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

