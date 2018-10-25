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
    public class DisciplineCategories_Edit_ControllerTests : ManagerControllerTests
    {
		DisciplineCategoriesControllerTests_Service TestService = new DisciplineCategoriesControllerTests_Service();

		[TestMethod()]
        public void EditGet_DisciplineCategory_Not_Exist_Test()
        {
            // Arrange
            DisciplineCategoriesController controller = new DisciplineCategoriesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_DisciplineCategory_Test()
        {
            // Arrange
            DisciplineCategoriesController controller = new DisciplineCategoriesController();
            DisciplineCategory disciplinecategory =  TestService.CreateOrLouadFirstDisciplineCategory(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(disciplinecategory.Id) as ViewResult;
            var DisciplineCategoryDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(DisciplineCategoryDetailModelView, typeof(Default_Form_DisciplineCategory_Model));
        }

        [TestMethod()]
        public void Edit_Valide_DisciplineCategory_Post_Test()
        {

            // Arrange
            DisciplineCategoriesController controller = new DisciplineCategoriesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            DisciplineCategory disciplinecategory = TestService.CreateOrLouadFirstDisciplineCategory(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            DisciplineCategoriesControllerTests_Service.PreBindModel(controller, disciplinecategory, nameof(DisciplineCategoriesController.Edit));
            DisciplineCategoriesControllerTests_Service.ValidateViewModel(controller, disciplinecategory);

			Default_Form_DisciplineCategory_Model Default_Form_DisciplineCategory_Model = new Default_Form_DisciplineCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_DisciplineCategory_Model(disciplinecategory);
            var result = controller.Edit(Default_Form_DisciplineCategory_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_DisciplineCategory_Post_Test()
        {
            // Arrange
            DisciplineCategoriesController controller = new DisciplineCategoriesController();
            DisciplineCategory disciplinecategory = TestService.CreateInValideDisciplineCategoryInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (disciplinecategory == null) return;
            DisciplineCategoryBLO disciplinecategoryBLO = new DisciplineCategoryBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            DisciplineCategoriesControllerTests_Service.PreBindModel(controller, disciplinecategory, nameof(DisciplineCategoriesController.Edit));
            List<ValidationResult> ls_validation_errors = DisciplineCategoriesControllerTests_Service
                .ValidateViewModel(controller, disciplinecategory);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_DisciplineCategory_Model Default_Form_DisciplineCategory_Model = new Default_Form_DisciplineCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_DisciplineCategory_Model(disciplinecategory);
            var result = controller.Edit(Default_Form_DisciplineCategory_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = disciplinecategoryBLO.Validate(disciplinecategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

