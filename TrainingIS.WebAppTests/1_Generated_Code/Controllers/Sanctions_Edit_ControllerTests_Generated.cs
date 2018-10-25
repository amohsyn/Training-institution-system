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
    public class Sanctions_Edit_ControllerTests : ManagerControllerTests
    {
		SanctionsControllerTests_Service TestService = new SanctionsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Sanction_Not_Exist_Test()
        {
            // Arrange
            SanctionsController controller = new SanctionsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Sanction_Test()
        {
            // Arrange
            SanctionsController controller = new SanctionsController();
            Sanction sanction =  TestService.CreateOrLouadFirstSanction(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(sanction.Id) as ViewResult;
            var SanctionDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SanctionDetailModelView, typeof(Default_Form_Sanction_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Sanction_Post_Test()
        {

            // Arrange
            SanctionsController controller = new SanctionsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Sanction sanction = TestService.CreateOrLouadFirstSanction(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            SanctionsControllerTests_Service.PreBindModel(controller, sanction, nameof(SanctionsController.Edit));
            SanctionsControllerTests_Service.ValidateViewModel(controller, sanction);

			Default_Form_Sanction_Model Default_Form_Sanction_Model = new Default_Form_Sanction_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Sanction_Model(sanction);
            var result = controller.Edit(Default_Form_Sanction_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Sanction_Post_Test()
        {
            // Arrange
            SanctionsController controller = new SanctionsController();
            Sanction sanction = TestService.CreateInValideSanctionInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (sanction == null) return;
            SanctionBLO sanctionBLO = new SanctionBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SanctionsControllerTests_Service.PreBindModel(controller, sanction, nameof(SanctionsController.Edit));
            List<ValidationResult> ls_validation_errors = SanctionsControllerTests_Service
                .ValidateViewModel(controller, sanction);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Sanction_Model Default_Form_Sanction_Model = new Default_Form_Sanction_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Sanction_Model(sanction);
            var result = controller.Edit(Default_Form_Sanction_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = sanctionBLO.Validate(sanction);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

