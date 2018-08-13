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
    public class Create_TrainingTypesControllerTests : ManagerControllerTests
    {
		TrainingTypesControllerTests_Service TestService = new TrainingTypesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            TrainingTypesController TrainingTypesController = new TrainingTypesController();

            ViewResult viewResult = TrainingTypesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_TrainingType_Post_Test()
        {
            //--Arrange--
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = TestService.CreateValideTrainingTypeInstance();

            //--Acte--
            //
            TrainingTypesControllerTests_Service.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Create));
            TrainingTypesControllerTests_Service.ValidateViewModel(controller,trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Create(Default_TrainingTypeFormView);
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
        public void Create_InValide_TrainingType_Post_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = TestService.CreateInValideTrainingTypeInstance();
            if (trainingtype == null) return;
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(controller._UnitOfWork);

            // Acte
            TrainingTypesControllerTests_Service.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Create));
            List<ValidationResult>  ls_validation_errors = TrainingTypesControllerTests_Service
                .ValidateViewModel(controller, trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Create(Default_TrainingTypeFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingtypeBLO.Validate(trainingtype);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}
