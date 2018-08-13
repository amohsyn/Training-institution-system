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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Edit_EntityPropertyShortcutsControllerTests : ManagerControllerTests
    {
		EntityPropertyShortcutsControllerTests_Service TestService = new EntityPropertyShortcutsControllerTests_Service();

		[TestMethod()]
        public void EditGet_EntityPropertyShortcut_Not_Exist_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_EntityPropertyShortcut_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut =  TestService.CreateOrLouadFirstEntityPropertyShortcut(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(entitypropertyshortcut.Id) as ViewResult;
            var EntityPropertyShortcutDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, typeof(Default_EntityPropertyShortcutFormView));
        }

        [TestMethod()]
        public void Edit_Valide_EntityPropertyShortcut_Post_Test()
        {

            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            EntityPropertyShortcut entitypropertyshortcut = TestService.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork());
			 
       

            // Acte
            EntityPropertyShortcutsControllerTests_Service.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Edit));
            EntityPropertyShortcutsControllerTests_Service.ValidateViewModel(controller, entitypropertyshortcut);

			Default_EntityPropertyShortcutFormView Default_EntityPropertyShortcutFormView = new Default_EntityPropertyShortcutFormViewBLM(controller._UnitOfWork).ConverTo_Default_EntityPropertyShortcutFormView(entitypropertyshortcut);
            var result = controller.Edit(Default_EntityPropertyShortcutFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_EntityPropertyShortcut_Post_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = TestService.CreateInValideEntityPropertyShortcutInstance_ForEdit(new UnitOfWork());
            if (entitypropertyshortcut == null) return;
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(controller._UnitOfWork);

            // Acte
            EntityPropertyShortcutsControllerTests_Service.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Edit));
            List<ValidationResult> ls_validation_errors = EntityPropertyShortcutsControllerTests_Service
                .ValidateViewModel(controller, entitypropertyshortcut);

			Default_EntityPropertyShortcutFormView Default_EntityPropertyShortcutFormView = new Default_EntityPropertyShortcutFormViewBLM(controller._UnitOfWork).ConverTo_Default_EntityPropertyShortcutFormView(entitypropertyshortcut);
            var result = controller.Edit(Default_EntityPropertyShortcutFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = entitypropertyshortcutBLO.Validate(entitypropertyshortcut);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}
