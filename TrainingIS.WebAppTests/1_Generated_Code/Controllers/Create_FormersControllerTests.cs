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
    public class Create_FormersControllerTests : ManagerControllerTests
    {
		FormersControllerTests_Service TestService = new FormersControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            FormersController FormersController = new FormersController();

            ViewResult viewResult = FormersController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Former_Post_Test()
        {
            //--Arrange--
            FormersController controller = new FormersController();
            Former former = TestService.CreateValideFormerInstance();

            //--Acte--
            //
            FormersControllerTests_Service.PreBindModel(controller, former, nameof(FormersController.Create));
            FormersControllerTests_Service.ValidateViewModel(controller,former);

			FormerFormView FormerFormView = new FormerFormViewBLM(controller._UnitOfWork).ConverTo_FormerFormView(former);
            var result = controller.Create(FormerFormView);
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
        public void Create_InValide_Former_Post_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former = TestService.CreateInValideFormerInstance();
            if (former == null) return;
            FormerBLO formerBLO = new FormerBLO(controller._UnitOfWork);

            // Acte
            FormersControllerTests_Service.PreBindModel(controller, former, nameof(FormersController.Create));
            List<ValidationResult>  ls_validation_errors = FormersControllerTests_Service
                .ValidateViewModel(controller, former);

			FormerFormView FormerFormView = new FormerFormViewBLM(controller._UnitOfWork).ConverTo_FormerFormView(former);
            var result = controller.Create(FormerFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = formerBLO.Validate(former);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}
