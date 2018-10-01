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
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.BLL.VO;
using GApp.BLL.Enums;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Create_Category_JustificationAbsencesControllerTests : ManagerControllerTests
    {
		Category_JustificationAbsencesControllerTests_Service TestService = new Category_JustificationAbsencesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            Category_JustificationAbsencesController Category_JustificationAbsencesController = new Category_JustificationAbsencesController();

            ViewResult viewResult = Category_JustificationAbsencesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Category_JustificationAbsence_Post_Test()
        {
            //--Arrange--
            Category_JustificationAbsencesController controller = new Category_JustificationAbsencesController();
            Category_JustificationAbsence category_justificationabsence = TestService.CreateValideCategory_JustificationAbsenceInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            Category_JustificationAbsencesControllerTests_Service.PreBindModel(controller, category_justificationabsence, nameof(Category_JustificationAbsencesController.Create));
            Category_JustificationAbsencesControllerTests_Service.ValidateViewModel(controller,category_justificationabsence);

			Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = new Default_Form_Category_JustificationAbsence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Category_JustificationAbsence_Model(category_justificationabsence);
            var result = controller.Create(Default_Form_Category_JustificationAbsence_Model);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_Category_JustificationAbsence_Post_Test()
        {
            // Arrange
            Category_JustificationAbsencesController controller = new Category_JustificationAbsencesController();
            Category_JustificationAbsence category_justificationabsence = TestService.CreateInValideCategory_JustificationAbsenceInstance(controller._UnitOfWork,controller.GAppContext);
            if (category_justificationabsence == null) return;
            Category_JustificationAbsenceBLO category_justificationabsenceBLO = new Category_JustificationAbsenceBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            Category_JustificationAbsencesControllerTests_Service.PreBindModel(controller, category_justificationabsence, nameof(Category_JustificationAbsencesController.Create));
            List<ValidationResult>  ls_validation_errors = Category_JustificationAbsencesControllerTests_Service
                .ValidateViewModel(controller, category_justificationabsence);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Category_JustificationAbsence_Model Default_Form_Category_JustificationAbsence_Model = new Default_Form_Category_JustificationAbsence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Category_JustificationAbsence_Model(category_justificationabsence);
            var result = controller.Create(Default_Form_Category_JustificationAbsence_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = category_justificationabsenceBLO.Validate(category_justificationabsence);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

