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
    public class Edit_YearStudiesControllerTests : ManagerControllerTests
    {
		YearStudiesControllerTests_Service TestService = new YearStudiesControllerTests_Service();

		[TestMethod()]
        public void EditGet_YearStudy_Not_Exist_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_YearStudy_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy =  TestService.CreateOrLouadFirstYearStudy(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(yearstudy.Id) as ViewResult;
            var YearStudyDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(YearStudyDetailModelView, typeof(Default_YearStudyFormView));
        }

        [TestMethod()]
        public void Edit_Valide_YearStudy_Post_Test()
        {

            // Arrange
            YearStudiesController controller = new YearStudiesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            YearStudy yearstudy = TestService.CreateOrLouadFirstYearStudy(new UnitOfWork());
			 
       

            // Acte
            YearStudiesControllerTests_Service.PreBindModel(controller, yearstudy, nameof(YearStudiesController.Edit));
            YearStudiesControllerTests_Service.ValidateViewModel(controller, yearstudy);

			Default_YearStudyFormView Default_YearStudyFormView = new Default_YearStudyFormViewBLM(controller._UnitOfWork).ConverTo_Default_YearStudyFormView(yearstudy);
            var result = controller.Edit(Default_YearStudyFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_YearStudy_Post_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy = TestService.CreateInValideYearStudyInstance_ForEdit(new UnitOfWork());
            if (yearstudy == null) return;
            YearStudyBLO yearstudyBLO = new YearStudyBLO(controller._UnitOfWork);

            // Acte
            YearStudiesControllerTests_Service.PreBindModel(controller, yearstudy, nameof(YearStudiesController.Edit));
            List<ValidationResult> ls_validation_errors = YearStudiesControllerTests_Service
                .ValidateViewModel(controller, yearstudy);

			Default_YearStudyFormView Default_YearStudyFormView = new Default_YearStudyFormViewBLM(controller._UnitOfWork).ConverTo_Default_YearStudyFormView(yearstudy);
            var result = controller.Edit(Default_YearStudyFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = yearstudyBLO.Validate(yearstudy);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

