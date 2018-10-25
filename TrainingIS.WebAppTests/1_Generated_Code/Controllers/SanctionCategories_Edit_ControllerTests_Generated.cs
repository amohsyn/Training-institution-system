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
    public class SanctionCategories_Edit_ControllerTests : ManagerControllerTests
    {
		SanctionCategoriesControllerTests_Service TestService = new SanctionCategoriesControllerTests_Service();

		[TestMethod()]
        public void EditGet_SanctionCategory_Not_Exist_Test()
        {
            // Arrange
            SanctionCategoriesController controller = new SanctionCategoriesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SanctionCategory_Test()
        {
            // Arrange
            SanctionCategoriesController controller = new SanctionCategoriesController();
            SanctionCategory sanctioncategory =  TestService.CreateOrLouadFirstSanctionCategory(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(sanctioncategory.Id) as ViewResult;
            var SanctionCategoryDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SanctionCategoryDetailModelView, typeof(Default_Form_SanctionCategory_Model));
        }

        [TestMethod()]
        public void Edit_Valide_SanctionCategory_Post_Test()
        {

            // Arrange
            SanctionCategoriesController controller = new SanctionCategoriesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SanctionCategory sanctioncategory = TestService.CreateOrLouadFirstSanctionCategory(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            SanctionCategoriesControllerTests_Service.PreBindModel(controller, sanctioncategory, nameof(SanctionCategoriesController.Edit));
            SanctionCategoriesControllerTests_Service.ValidateViewModel(controller, sanctioncategory);

			Default_Form_SanctionCategory_Model Default_Form_SanctionCategory_Model = new Default_Form_SanctionCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_SanctionCategory_Model(sanctioncategory);
            var result = controller.Edit(Default_Form_SanctionCategory_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SanctionCategory_Post_Test()
        {
            // Arrange
            SanctionCategoriesController controller = new SanctionCategoriesController();
            SanctionCategory sanctioncategory = TestService.CreateInValideSanctionCategoryInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (sanctioncategory == null) return;
            SanctionCategoryBLO sanctioncategoryBLO = new SanctionCategoryBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SanctionCategoriesControllerTests_Service.PreBindModel(controller, sanctioncategory, nameof(SanctionCategoriesController.Edit));
            List<ValidationResult> ls_validation_errors = SanctionCategoriesControllerTests_Service
                .ValidateViewModel(controller, sanctioncategory);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_SanctionCategory_Model Default_Form_SanctionCategory_Model = new Default_Form_SanctionCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_SanctionCategory_Model(sanctioncategory);
            var result = controller.Edit(Default_Form_SanctionCategory_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = sanctioncategoryBLO.Validate(sanctioncategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

