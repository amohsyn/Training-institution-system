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
    public class Create_TrainingYearsControllerTests : ManagerControllerTests
    {
		TrainingYearsControllerTests_Service TestService = new TrainingYearsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            TrainingYearsController TrainingYearsController = new TrainingYearsController();

            ViewResult viewResult = TrainingYearsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_TrainingYear_Post_Test()
        {
            //--Arrange--
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = TestService.CreateValideTrainingYearInstance();

            //--Acte--
            //
            TrainingYearsControllerTests_Service.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Create));
            TrainingYearsControllerTests_Service.ValidateViewModel(controller,trainingyear);

			Default_TrainingYearFormView Default_TrainingYearFormView = new Default_TrainingYearFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingYearFormView(trainingyear);
            var result = controller.Create(Default_TrainingYearFormView);
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
        public void Create_InValide_TrainingYear_Post_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = TestService.CreateInValideTrainingYearInstance();
            if (trainingyear == null) return;
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(controller._UnitOfWork);

            // Acte
            TrainingYearsControllerTests_Service.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Create));
            List<ValidationResult>  ls_validation_errors = TrainingYearsControllerTests_Service
                .ValidateViewModel(controller, trainingyear);

			Default_TrainingYearFormView Default_TrainingYearFormView = new Default_TrainingYearFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingYearFormView(trainingyear);
            var result = controller.Create(Default_TrainingYearFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingyearBLO.Validate(trainingyear);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}
