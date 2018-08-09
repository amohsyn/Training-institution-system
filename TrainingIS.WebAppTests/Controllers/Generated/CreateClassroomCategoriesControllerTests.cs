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
    public class Create_ClassroomCategoriesControllerTests : ManagerControllerTests
    {
		ClassroomCategoriesControllerTests_Service TestService = new ClassroomCategoriesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            ClassroomCategoriesController ClassroomCategoriesController = new ClassroomCategoriesController();

            ViewResult viewResult = ClassroomCategoriesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ClassroomCategory_Post_Test()
        {
            //--Arrange--
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = TestService.CreateValideClassroomCategoryInstance();

            //--Acte--
            //
            ClassroomCategoriesControllerTests_Service.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Create));
            ClassroomCategoriesControllerTests_Service.ValidateViewModel(controller,classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Create(Default_ClassroomCategoryFormView);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_ClassroomCategory_Post_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = TestService.CreateInValideClassroomCategoryInstance();
            if (classroomcategory == null) return;
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(controller._UnitOfWork);

            // Acte
            ClassroomCategoriesControllerTests_Service.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Create));
            List<ValidationResult>  ls_validation_errors = ClassroomCategoriesControllerTests_Service
                .ValidateViewModel(controller, classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Create(Default_ClassroomCategoryFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomcategoryBLO.Validate(classroomcategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

