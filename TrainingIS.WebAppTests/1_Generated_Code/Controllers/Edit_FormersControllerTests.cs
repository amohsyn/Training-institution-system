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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Edit_FormersControllerTests : ManagerControllerTests
    {
		FormersControllerTests_Service TestService = new FormersControllerTests_Service();

		[TestMethod()]
        public void EditGet_Former_Not_Exist_Test()
        {
            // Arrange
            FormersController controller = new FormersController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Former_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former =  TestService.CreateOrLouadFirstFormer(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(former.Id) as ViewResult;
            var FormerDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FormerDetailModelView, typeof(FormerFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Former_Post_Test()
        {

            // Arrange
            FormersController controller = new FormersController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Former former = TestService.CreateOrLouadFirstFormer(new UnitOfWork());
			 
       

            // Acte
            FormersControllerTests_Service.PreBindModel(controller, former, nameof(FormersController.Edit));
            FormersControllerTests_Service.ValidateViewModel(controller, former);

			FormerFormView FormerFormView = new FormerFormViewBLM(controller._UnitOfWork).ConverTo_FormerFormView(former);
            var result = controller.Edit(FormerFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Former_Post_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former = TestService.CreateInValideFormerInstance_ForEdit(new UnitOfWork());
            if (former == null) return;
            FormerBLO formerBLO = new FormerBLO(controller._UnitOfWork);

            // Acte
            FormersControllerTests_Service.PreBindModel(controller, former, nameof(FormersController.Edit));
            List<ValidationResult> ls_validation_errors = FormersControllerTests_Service
                .ValidateViewModel(controller, former);

			FormerFormView FormerFormView = new FormerFormViewBLM(controller._UnitOfWork).ConverTo_FormerFormView(former);
            var result = controller.Edit(FormerFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = formerBLO.Validate(former);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}
