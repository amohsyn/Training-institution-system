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
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using GApp.Entities;
using GApp.BLL.Enums;
using GApp.BLL.VO;
using GApp.DAL;
using TrainingIS.WebApp.Tests.Services;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Specialties_Edit_ControllerTests : ManagerControllerTests
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
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
			Assert.IsInstanceOfType(SpecialtyDetailModelView, typeof(Default_Form_Specialty_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Specialty_Post_Test()
        {

            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Specialty specialty = TestService.CreateOrLouadFirstSpecialty(new UnitOfWork<TrainingISModel>());
			 
       

            // Acte
            SpecialtiesControllerTests_Service.PreBindModel(controller, specialty, nameof(SpecialtiesController.Edit));
            SpecialtiesControllerTests_Service.ValidateViewModel(controller, specialty);

			Default_Form_Specialty_Model Default_Form_Specialty_Model = new Default_Form_Specialty_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Specialty_Model(specialty);
            var result = controller.Edit(Default_Form_Specialty_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Specialty_Post_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = TestService.CreateInValideSpecialtyInstance_ForEdit(new UnitOfWork<TrainingISModel>());
            if (specialty == null) return;
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(controller._UnitOfWork);

            // Acte
            SpecialtiesControllerTests_Service.PreBindModel(controller, specialty, nameof(SpecialtiesController.Edit));
            List<ValidationResult> ls_validation_errors = SpecialtiesControllerTests_Service
                .ValidateViewModel(controller, specialty);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Specialty_Model Default_Form_Specialty_Model = new Default_Form_Specialty_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Specialty_Model(specialty);
            var result = controller.Edit(Default_Form_Specialty_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = specialtyBLO.Validate(specialty);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

