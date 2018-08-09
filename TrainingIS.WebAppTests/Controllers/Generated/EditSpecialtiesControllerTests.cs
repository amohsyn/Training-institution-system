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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Edit_SpecialtiesControllerTests : ManagerControllerTests
    {
		SpecialtiesControllerTests_Service TestService = new SpecialtiesControllerTests_Service();

		[TestMethod()]
        public void EditGet_Specialty_Not_Exist_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Specialty_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty =  TestService.CreateOrLouadFirstSpecialty(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(specialty.Id) as ViewResult;
            var SpecialtyDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SpecialtyDetailModelView, typeof(Default_SpecialtyFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Specialty_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Specialty));

            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Specialty specialty = TestService.CreateOrLouadFirstSpecialty(new UnitOfWork());
			 
       

            // Acte
            SpecialtiesControllerTests_Service.PreBindModel(controller, specialty, nameof(SpecialtiesController.Edit));
            SpecialtiesControllerTests_Service.ValidateViewModel(controller, specialty);

			Default_SpecialtyFormView Default_SpecialtyFormView = new Default_SpecialtyFormViewBLM(controller._UnitOfWork).ConverTo_Default_SpecialtyFormView(specialty);
            var result = controller.Edit(Default_SpecialtyFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Specialty_Post_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = TestService.CreateInValideSpecialtyInstance_ForEdit(new UnitOfWork());
            if (specialty == null) return;
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(controller._UnitOfWork);

            // Acte
            SpecialtiesControllerTests_Service.PreBindModel(controller, specialty, nameof(SpecialtiesController.Edit));
            List<ValidationResult> ls_validation_errors = SpecialtiesControllerTests_Service
                .ValidateViewModel(controller, specialty);

			Default_SpecialtyFormView Default_SpecialtyFormView = new Default_SpecialtyFormViewBLM(controller._UnitOfWork).ConverTo_Default_SpecialtyFormView(specialty);
            var result = controller.Edit(Default_SpecialtyFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = specialtyBLO.Validate(specialty);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

