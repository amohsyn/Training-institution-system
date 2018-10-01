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
    public class Create_Category_WarningTraineesControllerTests : ManagerControllerTests
    {
		Category_WarningTraineesControllerTests_Service TestService = new Category_WarningTraineesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            Category_WarningTraineesController Category_WarningTraineesController = new Category_WarningTraineesController();

            ViewResult viewResult = Category_WarningTraineesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Category_WarningTrainee_Post_Test()
        {
            //--Arrange--
            Category_WarningTraineesController controller = new Category_WarningTraineesController();
            Category_WarningTrainee category_warningtrainee = TestService.CreateValideCategory_WarningTraineeInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            Category_WarningTraineesControllerTests_Service.PreBindModel(controller, category_warningtrainee, nameof(Category_WarningTraineesController.Create));
            Category_WarningTraineesControllerTests_Service.ValidateViewModel(controller,category_warningtrainee);

			Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model = new Default_Form_Category_WarningTrainee_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Category_WarningTrainee_Model(category_warningtrainee);
            var result = controller.Create(Default_Form_Category_WarningTrainee_Model);
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
        public void Create_InValide_Category_WarningTrainee_Post_Test()
        {
            // Arrange
            Category_WarningTraineesController controller = new Category_WarningTraineesController();
            Category_WarningTrainee category_warningtrainee = TestService.CreateInValideCategory_WarningTraineeInstance(controller._UnitOfWork,controller.GAppContext);
            if (category_warningtrainee == null) return;
            Category_WarningTraineeBLO category_warningtraineeBLO = new Category_WarningTraineeBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            Category_WarningTraineesControllerTests_Service.PreBindModel(controller, category_warningtrainee, nameof(Category_WarningTraineesController.Create));
            List<ValidationResult>  ls_validation_errors = Category_WarningTraineesControllerTests_Service
                .ValidateViewModel(controller, category_warningtrainee);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model = new Default_Form_Category_WarningTrainee_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Category_WarningTrainee_Model(category_warningtrainee);
            var result = controller.Create(Default_Form_Category_WarningTrainee_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = category_warningtraineeBLO.Validate(category_warningtrainee);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

