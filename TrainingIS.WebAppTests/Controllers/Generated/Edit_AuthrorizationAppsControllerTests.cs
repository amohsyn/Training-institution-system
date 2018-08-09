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
using TrainingIS.Entities.ModelsViews.Authorizations;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Edit_AuthrorizationAppsControllerTests : ManagerControllerTests
    {
		AuthrorizationAppsControllerTests_Service TestService = new AuthrorizationAppsControllerTests_Service();

		[TestMethod()]
        public void EditGet_AuthrorizationApp_Not_Exist_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_AuthrorizationApp_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp =  TestService.CreateOrLouadFirstAuthrorizationApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(authrorizationapp.Id) as ViewResult;
            var AuthrorizationAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AuthrorizationAppDetailModelView, typeof(AuthrorizationAppFormView));
        }

        [TestMethod()]
        public void Edit_Valide_AuthrorizationApp_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AuthrorizationApp));

            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            AuthrorizationApp authrorizationapp = TestService.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork());
			 
       

            // Acte
            AuthrorizationAppsControllerTests_Service.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Edit));
            AuthrorizationAppsControllerTests_Service.ValidateViewModel(controller, authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Edit(AuthrorizationAppFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_AuthrorizationApp_Post_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = TestService.CreateInValideAuthrorizationAppInstance_ForEdit(new UnitOfWork());
            if (authrorizationapp == null) return;
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(controller._UnitOfWork);

            // Acte
            AuthrorizationAppsControllerTests_Service.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Edit));
            List<ValidationResult> ls_validation_errors = AuthrorizationAppsControllerTests_Service
                .ValidateViewModel(controller, authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Edit(AuthrorizationAppFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = authrorizationappBLO.Validate(authrorizationapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

