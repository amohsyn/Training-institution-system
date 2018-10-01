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
    public class Category_JustificationAbsences_Edit_ControllerTests : ManagerControllerTests
    {
		Category_JustificationAbsencesControllerTests_Service TestService = new Category_JustificationAbsencesControllerTests_Service();

		[TestMethod()]
        public void EditGet_Category_JustificationAbsence_Not_Exist_Test()
        {
            // Arrange
            Category_JustificationAbsencesController controller = new Category_JustificationAbsencesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Category_JustificationAbsence_Test()
        {
            // Arrange
            Category_JustificationAbsencesController controller = new Category_JustificationAbsencesController();
            Category_JustificationAbsence category_justificationabsence =  TestService.CreateOrLouadFirstCategory_JustificationAbsence(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(category_justificationabsence.Id) as ViewResult;
            var Category_JustificationAbsenceDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(Category_JustificationAbsenceDetailModelView, typeof(Default_Form_Category_JustificationAbsence_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Category_JustificationAbsence_Post_Test()
        {

            // Arrange
            Category_JustificationAbsencesController controller = new Category_JustificationAbsencesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Category_JustificationAbsence category_justificationabsence = TestService.CreateOrLouadFirstCategory_JustificationAbsence(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            Category_JustificationAbsencesControllerTests_Service.PreBindModel(controller, category_justificationabsence, nameof(Category_JustificationAbsencesController.Edit));
            Category_JustificationAbsencesControllerTests_Service.ValidateViewModel(controller, category_justificationabsence);

			Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = new Default_Form_Category_JustificationAbsence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Category_JustificationAbsence_Model(category_justificationabsence);
            var result = controller.Edit(Default_Form_Category_JustificationAbsence_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Category_JustificationAbsence_Post_Test()
        {
            // Arrange
            Category_JustificationAbsencesController controller = new Category_JustificationAbsencesController();
            Category_JustificationAbsence category_justificationabsence = TestService.CreateInValideCategory_JustificationAbsenceInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (category_justificationabsence == null) return;
            Category_JustificationAbsenceBLO category_justificationabsenceBLO = new Category_JustificationAbsenceBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            Category_JustificationAbsencesControllerTests_Service.PreBindModel(controller, category_justificationabsence, nameof(Category_JustificationAbsencesController.Edit));
            List<ValidationResult> ls_validation_errors = Category_JustificationAbsencesControllerTests_Service
                .ValidateViewModel(controller, category_justificationabsence);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = new Default_Form_Category_JustificationAbsence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Category_JustificationAbsence_Model(category_justificationabsence);
            var result = controller.Edit(Default_Form_Category_JustificationAbsence_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = category_justificationabsenceBLO.Validate(category_justificationabsence);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

