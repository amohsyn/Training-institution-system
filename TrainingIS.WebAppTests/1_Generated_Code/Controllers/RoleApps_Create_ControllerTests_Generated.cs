﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class Create_RoleAppsControllerTests : ManagerControllerTests
    {
		RoleAppsControllerTests_Service TestService = new RoleAppsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            RoleAppsController RoleAppsController = new RoleAppsController();

            ViewResult viewResult = RoleAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_RoleApp_Post_Test()
        {
            //--Arrange--
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = TestService.CreateValideRoleAppInstance();

            //--Acte--
            //
            RoleAppsControllerTests_Service.PreBindModel(controller, roleapp, nameof(RoleAppsController.Create));
            RoleAppsControllerTests_Service.ValidateViewModel(controller,roleapp);

			Default_Form_RoleApp_Model Default_Form_RoleApp_Model = new Default_Form_RoleApp_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_RoleApp_Model(roleapp);
            var result = controller.Create(Default_Form_RoleApp_Model);
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
        public void Create_InValide_RoleApp_Post_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = TestService.CreateInValideRoleAppInstance();
            if (roleapp == null) return;
            RoleAppBLO roleappBLO = new RoleAppBLO(controller._UnitOfWork);

            // Acte
            RoleAppsControllerTests_Service.PreBindModel(controller, roleapp, nameof(RoleAppsController.Create));
            List<ValidationResult>  ls_validation_errors = RoleAppsControllerTests_Service
                .ValidateViewModel(controller, roleapp);

			Default_Form_RoleApp_Model Default_Form_RoleApp_Model = new Default_Form_RoleApp_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_RoleApp_Model(roleapp);
            var result = controller.Create(Default_Form_RoleApp_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = roleappBLO.Validate(roleapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}
