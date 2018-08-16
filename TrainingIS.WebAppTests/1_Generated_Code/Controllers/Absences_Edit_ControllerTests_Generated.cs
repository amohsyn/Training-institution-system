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
    public class Absences_Edit_ControllerTests : ManagerControllerTests
    {
		AbsencesControllerTests_Service TestService = new AbsencesControllerTests_Service();

		[TestMethod()]
        public void EditGet_Absence_Not_Exist_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Absence_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence =  TestService.CreateOrLouadFirstAbsence(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(absence.Id) as ViewResult;
            var AbsenceDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AbsenceDetailModelView, typeof(Default_Form_Absence_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Absence_Post_Test()
        {

            // Arrange
            AbsencesController controller = new AbsencesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Absence absence = TestService.CreateOrLouadFirstAbsence(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            AbsencesControllerTests_Service.PreBindModel(controller, absence, nameof(AbsencesController.Edit));
            AbsencesControllerTests_Service.ValidateViewModel(controller, absence);

			Default_Form_Absence_Model Default_Form_Absence_Model = new Default_Form_Absence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Absence_Model(absence);
            var result = controller.Edit(Default_Form_Absence_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Absence_Post_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence = TestService.CreateInValideAbsenceInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (absence == null) return;
            AbsenceBLO absenceBLO = new AbsenceBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            AbsencesControllerTests_Service.PreBindModel(controller, absence, nameof(AbsencesController.Edit));
            List<ValidationResult> ls_validation_errors = AbsencesControllerTests_Service
                .ValidateViewModel(controller, absence);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Absence_Model Default_Form_Absence_Model = new Default_Form_Absence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Absence_Model(absence);
            var result = controller.Edit(Default_Form_Absence_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = absenceBLO.Validate(absence);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

