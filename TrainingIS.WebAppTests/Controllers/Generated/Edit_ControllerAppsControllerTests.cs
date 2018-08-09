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
    public class Edit_ControllerAppsControllerTests : ManagerControllerTests
    {
		ControllerAppsControllerTests_Service TestService = new ControllerAppsControllerTests_Service();

		[TestMethod()]
        public void EditGet_ControllerApp_Not_Exist_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ControllerApp_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp =  TestService.CreateOrLouadFirstControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(controllerapp.Id) as ViewResult;
            var ControllerAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ControllerAppDetailModelView, typeof(Default_ControllerAppFormView));
        }

        [TestMethod()]
        public void Edit_Valide_ControllerApp_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ControllerApp));

            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ControllerApp controllerapp = TestService.CreateOrLouadFirstControllerApp(new UnitOfWork());
			 
       

            // Acte
            ControllerAppsControllerTests_Service.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Edit));
            ControllerAppsControllerTests_Service.ValidateViewModel(controller, controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Edit(Default_ControllerAppFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ControllerApp_Post_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = TestService.CreateInValideControllerAppInstance_ForEdit(new UnitOfWork());
            if (controllerapp == null) return;
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(controller._UnitOfWork);

            // Acte
            ControllerAppsControllerTests_Service.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Edit));
            List<ValidationResult> ls_validation_errors = ControllerAppsControllerTests_Service
                .ValidateViewModel(controller, controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Edit(Default_ControllerAppFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = controllerappBLO.Validate(controllerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

