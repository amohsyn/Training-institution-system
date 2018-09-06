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
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class FormerSpecialties_Edit_ControllerTests : ManagerControllerTests
    {
		FormerSpecialtiesControllerTests_Service TestService = new FormerSpecialtiesControllerTests_Service();

		[TestMethod()]
        public void EditGet_FormerSpecialty_Not_Exist_Test()
        {
            // Arrange
            FormerSpecialtiesController controller = new FormerSpecialtiesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_FormerSpecialty_Test()
        {
            // Arrange
            FormerSpecialtiesController controller = new FormerSpecialtiesController();
            FormerSpecialty formerspecialty =  TestService.CreateOrLouadFirstFormerSpecialty(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(formerspecialty.Id) as ViewResult;
            var FormerSpecialtyDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FormerSpecialtyDetailModelView, typeof(Default_Form_FormerSpecialty_Model));
        }

        [TestMethod()]
        public void Edit_Valide_FormerSpecialty_Post_Test()
        {

            // Arrange
            FormerSpecialtiesController controller = new FormerSpecialtiesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            FormerSpecialty formerspecialty = TestService.CreateOrLouadFirstFormerSpecialty(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            FormerSpecialtiesControllerTests_Service.PreBindModel(controller, formerspecialty, nameof(FormerSpecialtiesController.Edit));
            FormerSpecialtiesControllerTests_Service.ValidateViewModel(controller, formerspecialty);

			Default_Form_FormerSpecialty_Model Default_Form_FormerSpecialty_Model = new Default_Form_FormerSpecialty_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_FormerSpecialty_Model(formerspecialty);
            var result = controller.Edit(Default_Form_FormerSpecialty_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_FormerSpecialty_Post_Test()
        {
            // Arrange
            FormerSpecialtiesController controller = new FormerSpecialtiesController();
            FormerSpecialty formerspecialty = TestService.CreateInValideFormerSpecialtyInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (formerspecialty == null) return;
            FormerSpecialtyBLO formerspecialtyBLO = new FormerSpecialtyBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            FormerSpecialtiesControllerTests_Service.PreBindModel(controller, formerspecialty, nameof(FormerSpecialtiesController.Edit));
            List<ValidationResult> ls_validation_errors = FormerSpecialtiesControllerTests_Service
                .ValidateViewModel(controller, formerspecialty);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_FormerSpecialty_Model Default_Form_FormerSpecialty_Model = new Default_Form_FormerSpecialty_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_FormerSpecialty_Model(formerspecialty);
            var result = controller.Edit(Default_Form_FormerSpecialty_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = formerspecialtyBLO.Validate(formerspecialty);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

