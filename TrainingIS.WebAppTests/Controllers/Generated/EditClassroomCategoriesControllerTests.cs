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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Edit_ClassroomCategoriesControllerTests : ManagerControllerTests
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
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ClassroomCategory_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory =  TestService.CreateOrLouadFirstClassroomCategory(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(classroomcategory.Id) as ViewResult;
            var ClassroomCategoryDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ClassroomCategoryDetailModelView, typeof(Default_ClassroomCategoryFormView));
        }

        [TestMethod()]
        public void Edit_Valide_ClassroomCategory_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ClassroomCategory));

            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ClassroomCategory classroomcategory = TestService.CreateOrLouadFirstClassroomCategory(new UnitOfWork());
			 
       

            // Acte
            ClassroomCategoriesControllerTests_Service.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Edit));
            ClassroomCategoriesControllerTests_Service.ValidateViewModel(controller, classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Edit(Default_ClassroomCategoryFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ClassroomCategory_Post_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = TestService.CreateInValideClassroomCategoryInstance_ForEdit(new UnitOfWork());
            if (classroomcategory == null) return;
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(controller._UnitOfWork);

            // Acte
            ClassroomCategoriesControllerTests_Service.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Edit));
            List<ValidationResult> ls_validation_errors = ClassroomCategoriesControllerTests_Service
                .ValidateViewModel(controller, classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Edit(Default_ClassroomCategoryFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomcategoryBLO.Validate(classroomcategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

