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
using GApp.Entities;
using GApp.BLL.Enums;
using GApp.BLL.VO;
using GApp.DAL;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Nationalities_Edit_ControllerTests : ManagerControllerTests
    {
		NationalitiesControllerTests_Service TestService = new NationalitiesControllerTests_Service();

		[TestMethod()]
        public void EditGet_Nationality_Not_Exist_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Nationality_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality =  TestService.CreateOrLouadFirstNationality(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(nationality.Id) as ViewResult;
            var NationalityDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(NationalityDetailModelView, typeof(Default_Form_Nationality_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Nationality_Post_Test()
        {

            // Arrange
            NationalitiesController controller = new NationalitiesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Nationality nationality = TestService.CreateOrLouadFirstNationality(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            NationalitiesControllerTests_Service.PreBindModel(controller, nationality, nameof(NationalitiesController.Edit));
            NationalitiesControllerTests_Service.ValidateViewModel(controller, nationality);

			Default_Form_Nationality_Model Default_Form_Nationality_Model = new Default_Form_Nationality_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Nationality_Model(nationality);
            var result = controller.Edit(Default_Form_Nationality_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Nationality_Post_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality = TestService.CreateInValideNationalityInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (nationality == null) return;
            NationalityBLO nationalityBLO = new NationalityBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            NationalitiesControllerTests_Service.PreBindModel(controller, nationality, nameof(NationalitiesController.Edit));
            List<ValidationResult> ls_validation_errors = NationalitiesControllerTests_Service
                .ValidateViewModel(controller, nationality);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Nationality_Model Default_Form_Nationality_Model = new Default_Form_Nationality_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Nationality_Model(nationality);
            var result = controller.Edit(Default_Form_Nationality_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = nationalityBLO.Validate(nationality);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

