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
    public class Create_DisciplineCategoriesControllerTests : ManagerControllerTests
    {
		DisciplineCategoriesControllerTests_Service TestService = new DisciplineCategoriesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            DisciplineCategoriesController DisciplineCategoriesController = new DisciplineCategoriesController();

            ViewResult viewResult = DisciplineCategoriesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_DisciplineCategory_Post_Test()
        {
            //--Arrange--
            DisciplineCategoriesController controller = new DisciplineCategoriesController();
            DisciplineCategory disciplinecategory = TestService.CreateValideDisciplineCategoryInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            DisciplineCategoriesControllerTests_Service.PreBindModel(controller, disciplinecategory, nameof(DisciplineCategoriesController.Create));
            DisciplineCategoriesControllerTests_Service.ValidateViewModel(controller,disciplinecategory);

			Default_Form_DisciplineCategory_Model Default_Form_DisciplineCategory_Model = new Default_Form_DisciplineCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_DisciplineCategory_Model(disciplinecategory);
            var result = controller.Create(Default_Form_DisciplineCategory_Model);
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
        public void Create_InValide_DisciplineCategory_Post_Test()
        {
            // Arrange
            DisciplineCategoriesController controller = new DisciplineCategoriesController();
            DisciplineCategory disciplinecategory = TestService.CreateInValideDisciplineCategoryInstance(controller._UnitOfWork,controller.GAppContext);
            if (disciplinecategory == null) return;
            DisciplineCategoryBLO disciplinecategoryBLO = new DisciplineCategoryBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            DisciplineCategoriesControllerTests_Service.PreBindModel(controller, disciplinecategory, nameof(DisciplineCategoriesController.Create));
            List<ValidationResult>  ls_validation_errors = DisciplineCategoriesControllerTests_Service
                .ValidateViewModel(controller, disciplinecategory);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_DisciplineCategory_Model Default_Form_DisciplineCategory_Model = new Default_Form_DisciplineCategory_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_DisciplineCategory_Model(disciplinecategory);
            var result = controller.Create(Default_Form_DisciplineCategory_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = disciplinecategoryBLO.Validate(disciplinecategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

