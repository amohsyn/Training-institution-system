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
    public class Create_AuthrorizationAppsControllerTests : ManagerControllerTests
    {
		AuthrorizationAppsControllerTests_Service TestService = new AuthrorizationAppsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            AuthrorizationAppsController AuthrorizationAppsController = new AuthrorizationAppsController();

            ViewResult viewResult = AuthrorizationAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_AuthrorizationApp_Post_Test()
        {
            //--Arrange--
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = TestService.CreateValideAuthrorizationAppInstance();

            //--Acte--
            //
            AuthrorizationAppsControllerTests_Service.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Create));
            AuthrorizationAppsControllerTests_Service.ValidateViewModel(controller,authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Create(AuthrorizationAppFormView);
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
        public void Create_InValide_AuthrorizationApp_Post_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = TestService.CreateInValideAuthrorizationAppInstance();
            if (authrorizationapp == null) return;
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(controller._UnitOfWork);

            // Acte
            AuthrorizationAppsControllerTests_Service.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Create));
            List<ValidationResult>  ls_validation_errors = AuthrorizationAppsControllerTests_Service
                .ValidateViewModel(controller, authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Create(AuthrorizationAppFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = authrorizationappBLO.Validate(authrorizationapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

