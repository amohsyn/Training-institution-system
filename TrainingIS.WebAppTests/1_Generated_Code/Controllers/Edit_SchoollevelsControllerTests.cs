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
    public class Edit_SchoollevelsControllerTests : ManagerControllerTests
    {
		SchoollevelsControllerTests_Service TestService = new SchoollevelsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Schoollevel_Not_Exist_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Schoollevel_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel =  TestService.CreateOrLouadFirstSchoollevel(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(schoollevel.Id) as ViewResult;
            var SchoollevelDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SchoollevelDetailModelView, typeof(Default_SchoollevelFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Schoollevel_Post_Test()
        {

            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Schoollevel schoollevel = TestService.CreateOrLouadFirstSchoollevel(new UnitOfWork());
			 
       

            // Acte
            SchoollevelsControllerTests_Service.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Edit));
            SchoollevelsControllerTests_Service.ValidateViewModel(controller, schoollevel);

			Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormViewBLM(controller._UnitOfWork).ConverTo_Default_SchoollevelFormView(schoollevel);
            var result = controller.Edit(Default_SchoollevelFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Schoollevel_Post_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = TestService.CreateInValideSchoollevelInstance_ForEdit(new UnitOfWork());
            if (schoollevel == null) return;
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(controller._UnitOfWork);

            // Acte
            SchoollevelsControllerTests_Service.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Edit));
            List<ValidationResult> ls_validation_errors = SchoollevelsControllerTests_Service
                .ValidateViewModel(controller, schoollevel);

			Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormViewBLM(controller._UnitOfWork).ConverTo_Default_SchoollevelFormView(schoollevel);
            var result = controller.Edit(Default_SchoollevelFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = schoollevelBLO.Validate(schoollevel);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

