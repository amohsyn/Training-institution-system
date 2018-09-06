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
    public class ClassroomCategories_Edit_ControllerTests : ManagerControllerTests
    {
		ClassroomCategoriesControllerTests_Service TestService = new ClassroomCategoriesControllerTests_Service();

		[TestMethod()]
        public void EditGet_ClassroomCategory_Not_Exist_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ClassroomCategory_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory =  TestService.CreateOrLouadFirstClassroomCategory(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(classroomcategory.Id) as ViewResult;
            var ClassroomCategoryDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ClassroomCategoryDetailModelView, typeof(Default_Form_ClassroomCategory_Model));
        }

        [TestMethod()]
        public void Edit_Valide_ClassroomCategory_Post_Test()
        {

            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ClassroomCategory classroomcategory = TestService.CreateOrLouadFirstClassroomCategory(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            ClassroomCategoriesControllerTests_Service.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Edit));
            ClassroomCategoriesControllerTests_Service.ValidateViewModel(controller, classroomcategory);

			Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model = new Default_Form_ClassroomCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_ClassroomCategory_Model(classroomcategory);
            var result = controller.Edit(Default_Form_ClassroomCategory_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ClassroomCategory_Post_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = TestService.CreateInValideClassroomCategoryInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (classroomcategory == null) return;
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            ClassroomCategoriesControllerTests_Service.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Edit));
            List<ValidationResult> ls_validation_errors = ClassroomCategoriesControllerTests_Service
                .ValidateViewModel(controller, classroomcategory);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_ClassroomCategory_Model Default_Form_ClassroomCategory_Model = new Default_Form_ClassroomCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_ClassroomCategory_Model(classroomcategory);
            var result = controller.Edit(Default_Form_ClassroomCategory_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomcategoryBLO.Validate(classroomcategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

