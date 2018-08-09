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
    public class Create_SpecialtiesControllerTests : ManagerControllerTests
    {
		SpecialtiesControllerTests_Service TestService = new SpecialtiesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            SpecialtiesController SpecialtiesController = new SpecialtiesController();

            ViewResult viewResult = SpecialtiesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Specialty_Post_Test()
        {
            //--Arrange--
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = TestService.CreateValideSpecialtyInstance();

            //--Acte--
            //
            SpecialtiesControllerTests_Service.PreBindModel(controller, specialty, nameof(SpecialtiesController.Create));
            SpecialtiesControllerTests_Service.ValidateViewModel(controller,specialty);

			Default_SpecialtyFormView Default_SpecialtyFormView = new Default_SpecialtyFormViewBLM(controller._UnitOfWork).ConverTo_Default_SpecialtyFormView(specialty);
            var result = controller.Create(Default_SpecialtyFormView);
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
        public void Create_InValide_Specialty_Post_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = TestService.CreateInValideSpecialtyInstance();
            if (specialty == null) return;
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(controller._UnitOfWork);

            // Acte
            SpecialtiesControllerTests_Service.PreBindModel(controller, specialty, nameof(SpecialtiesController.Create));
            List<ValidationResult>  ls_validation_errors = SpecialtiesControllerTests_Service
                .ValidateViewModel(controller, specialty);

			Default_SpecialtyFormView Default_SpecialtyFormView = new Default_SpecialtyFormViewBLM(controller._UnitOfWork).ConverTo_Default_SpecialtyFormView(specialty);
            var result = controller.Create(Default_SpecialtyFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = specialtyBLO.Validate(specialty);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

