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
    public class Edit_SeanceDaysControllerTests : ManagerControllerTests
    {
		SeanceDaysControllerTests_Service TestService = new SeanceDaysControllerTests_Service();

		[TestMethod()]
        public void EditGet_SeanceDay_Not_Exist_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceDay_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday =  TestService.CreateOrLouadFirstSeanceDay(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seanceday.Id) as ViewResult;
            var SeanceDayDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceDayDetailModelView, typeof(Default_SeanceDayFormView));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceDay_Post_Test()
        {

            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceDay seanceday = TestService.CreateOrLouadFirstSeanceDay(new UnitOfWork());
			 
       

            // Acte
            SeanceDaysControllerTests_Service.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Edit));
            SeanceDaysControllerTests_Service.ValidateViewModel(controller, seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Edit(Default_SeanceDayFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceDay_Post_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = TestService.CreateInValideSeanceDayInstance_ForEdit(new UnitOfWork());
            if (seanceday == null) return;
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(controller._UnitOfWork);

            // Acte
            SeanceDaysControllerTests_Service.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceDaysControllerTests_Service
                .ValidateViewModel(controller, seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Edit(Default_SeanceDayFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancedayBLO.Validate(seanceday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

