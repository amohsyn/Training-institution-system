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
    public class Edit_ActionControllerAppsControllerTests : ManagerControllerTests
    {
		ActionControllerAppsControllerTests_Service TestService = new ActionControllerAppsControllerTests_Service();

		[TestMethod()]
        public void EditGet_ActionControllerApp_Not_Exist_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ActionControllerApp_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp =  TestService.CreateOrLouadFirstActionControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(actioncontrollerapp.Id) as ViewResult;
            var ActionControllerAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ActionControllerAppDetailModelView, typeof(Default_ActionControllerAppFormView));
        }

        [TestMethod()]
        public void Edit_Valide_ActionControllerApp_Post_Test()
        {

            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ActionControllerApp actioncontrollerapp = TestService.CreateOrLouadFirstActionControllerApp(new UnitOfWork());
			 
       

            // Acte
            ActionControllerAppsControllerTests_Service.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Edit));
            ActionControllerAppsControllerTests_Service.ValidateViewModel(controller, actioncontrollerapp);

			Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ActionControllerAppFormView(actioncontrollerapp);
            var result = controller.Edit(Default_ActionControllerAppFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ActionControllerApp_Post_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = TestService.CreateInValideActionControllerAppInstance_ForEdit(new UnitOfWork());
            if (actioncontrollerapp == null) return;
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(controller._UnitOfWork);

            // Acte
            ActionControllerAppsControllerTests_Service.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Edit));
            List<ValidationResult> ls_validation_errors = ActionControllerAppsControllerTests_Service
                .ValidateViewModel(controller, actioncontrollerapp);

			Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ActionControllerAppFormView(actioncontrollerapp);
            var result = controller.Edit(Default_ActionControllerAppFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = actioncontrollerappBLO.Validate(actioncontrollerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

