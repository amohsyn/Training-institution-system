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
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Create_ActionControllerAppsControllerTests : ManagerControllerTests
    {
		ActionControllerAppsControllerTests_Service TestService = new ActionControllerAppsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            ActionControllerAppsController ActionControllerAppsController = new ActionControllerAppsController();

            ViewResult viewResult = ActionControllerAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ActionControllerApp_Post_Test()
        {
            //--Arrange--
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = TestService.CreateValideActionControllerAppInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            ActionControllerAppsControllerTests_Service.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Create));
            ActionControllerAppsControllerTests_Service.ValidateViewModel(controller,actioncontrollerapp);

			Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = new Default_Form_ActionControllerApp_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_ActionControllerApp_Model(actioncontrollerapp);
            var result = controller.Create(Default_Form_ActionControllerApp_Model);
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
        public void Create_InValide_ActionControllerApp_Post_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = TestService.CreateInValideActionControllerAppInstance(controller._UnitOfWork,controller.GAppContext);
            if (actioncontrollerapp == null) return;
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            ActionControllerAppsControllerTests_Service.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Create));
            List<ValidationResult>  ls_validation_errors = ActionControllerAppsControllerTests_Service
                .ValidateViewModel(controller, actioncontrollerapp);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = new Default_Form_ActionControllerApp_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_ActionControllerApp_Model(actioncontrollerapp);
            var result = controller.Create(Default_Form_ActionControllerApp_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = actioncontrollerappBLO.Validate(actioncontrollerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

