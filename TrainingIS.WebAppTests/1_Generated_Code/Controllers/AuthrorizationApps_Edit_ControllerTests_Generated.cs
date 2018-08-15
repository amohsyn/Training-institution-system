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
    public class AuthrorizationApps_Edit_ControllerTests : ManagerControllerTests
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
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
			Assert.IsInstanceOfType(AuthrorizationAppDetailModelView, typeof(Default_Form_AuthrorizationApp_Model));
        }

        [TestMethod()]
        public void Edit_Valide_AuthrorizationApp_Post_Test()
        {

            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            AuthrorizationApp authrorizationapp = TestService.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork<TrainingISModel>());
			 
       

            // Acte
            AuthrorizationAppsControllerTests_Service.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Edit));
            AuthrorizationAppsControllerTests_Service.ValidateViewModel(controller, authrorizationapp);

			Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_AuthrorizationApp_Model(authrorizationapp);
            var result = controller.Edit(Default_Form_AuthrorizationApp_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_AuthrorizationApp_Post_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = TestService.CreateInValideAuthrorizationAppInstance_ForEdit(new UnitOfWork<TrainingISModel>());
            if (authrorizationapp == null) return;
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(controller._UnitOfWork);

            // Acte
            AuthrorizationAppsControllerTests_Service.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Edit));
            List<ValidationResult> ls_validation_errors = AuthrorizationAppsControllerTests_Service
                .ValidateViewModel(controller, authrorizationapp);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_AuthrorizationApp_Model(authrorizationapp);
            var result = controller.Edit(Default_Form_AuthrorizationApp_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = authrorizationappBLO.Validate(authrorizationapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

