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
    public class JustificationAbsences_Edit_ControllerTests : ManagerControllerTests
    {
		JustificationAbsencesControllerTests_Service TestService = new JustificationAbsencesControllerTests_Service();

		[TestMethod()]
        public void EditGet_JustificationAbsence_Not_Exist_Test()
        {
            // Arrange
            JustificationAbsencesController controller = new JustificationAbsencesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_JustificationAbsence_Test()
        {
            // Arrange
            JustificationAbsencesController controller = new JustificationAbsencesController();
            JustificationAbsence justificationabsence =  TestService.CreateOrLouadFirstJustificationAbsence(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(justificationabsence.Id) as ViewResult;
            var JustificationAbsenceDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(JustificationAbsenceDetailModelView, typeof(Default_Form_JustificationAbsence_Model));
        }

        [TestMethod()]
        public void Edit_Valide_JustificationAbsence_Post_Test()
        {

            // Arrange
            JustificationAbsencesController controller = new JustificationAbsencesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            JustificationAbsence justificationabsence = TestService.CreateOrLouadFirstJustificationAbsence(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            JustificationAbsencesControllerTests_Service.PreBindModel(controller, justificationabsence, nameof(JustificationAbsencesController.Edit));
            JustificationAbsencesControllerTests_Service.ValidateViewModel(controller, justificationabsence);

			Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model = new Default_Form_JustificationAbsence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_JustificationAbsence_Model(justificationabsence);
            var result = controller.Edit(Default_Form_JustificationAbsence_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_JustificationAbsence_Post_Test()
        {
            // Arrange
            JustificationAbsencesController controller = new JustificationAbsencesController();
            JustificationAbsence justificationabsence = TestService.CreateInValideJustificationAbsenceInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (justificationabsence == null) return;
            JustificationAbsenceBLO justificationabsenceBLO = new JustificationAbsenceBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            JustificationAbsencesControllerTests_Service.PreBindModel(controller, justificationabsence, nameof(JustificationAbsencesController.Edit));
            List<ValidationResult> ls_validation_errors = JustificationAbsencesControllerTests_Service
                .ValidateViewModel(controller, justificationabsence);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model = new Default_Form_JustificationAbsence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_JustificationAbsence_Model(justificationabsence);
            var result = controller.Edit(Default_Form_JustificationAbsence_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = justificationabsenceBLO.Validate(justificationabsence);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

