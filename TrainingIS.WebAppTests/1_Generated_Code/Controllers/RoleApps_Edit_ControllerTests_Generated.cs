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
    public class RoleApps_Edit_ControllerTests : ManagerControllerTests
    {
		RoleAppsControllerTests_Service TestService = new RoleAppsControllerTests_Service();

		[TestMethod()]
        public void EditGet_RoleApp_Not_Exist_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_RoleApp_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp =  TestService.CreateOrLouadFirstRoleApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(roleapp.Id) as ViewResult;
            var RoleAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(RoleAppDetailModelView, typeof(Default_Form_RoleApp_Model));
        }

        [TestMethod()]
        public void Edit_Valide_RoleApp_Post_Test()
        {

            // Arrange
            RoleAppsController controller = new RoleAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            RoleApp roleapp = TestService.CreateOrLouadFirstRoleApp(new UnitOfWork<TrainingISModel>());
			 
       

            // Acte
            RoleAppsControllerTests_Service.PreBindModel(controller, roleapp, nameof(RoleAppsController.Edit));
            RoleAppsControllerTests_Service.ValidateViewModel(controller, roleapp);

			Default_Form_RoleApp_Model Default_Form_RoleApp_Model = new Default_Form_RoleApp_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_RoleApp_Model(roleapp);
            var result = controller.Edit(Default_Form_RoleApp_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_RoleApp_Post_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = TestService.CreateInValideRoleAppInstance_ForEdit(new UnitOfWork<TrainingISModel>());
            if (roleapp == null) return;
            RoleAppBLO roleappBLO = new RoleAppBLO(controller._UnitOfWork);

            // Acte
            RoleAppsControllerTests_Service.PreBindModel(controller, roleapp, nameof(RoleAppsController.Edit));
            List<ValidationResult> ls_validation_errors = RoleAppsControllerTests_Service
                .ValidateViewModel(controller, roleapp);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_RoleApp_Model Default_Form_RoleApp_Model = new Default_Form_RoleApp_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_RoleApp_Model(roleapp);
            var result = controller.Edit(Default_Form_RoleApp_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = roleappBLO.Validate(roleapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

