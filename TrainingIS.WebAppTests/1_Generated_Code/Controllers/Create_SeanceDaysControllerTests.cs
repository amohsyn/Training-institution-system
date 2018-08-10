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
    public class Create_SeanceDaysControllerTests : ManagerControllerTests
    {
		SeanceDaysControllerTests_Service TestService = new SeanceDaysControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            SeanceDaysController SeanceDaysController = new SeanceDaysController();

            ViewResult viewResult = SeanceDaysController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_SeanceDay_Post_Test()
        {
            //--Arrange--
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = TestService.CreateValideSeanceDayInstance();

            //--Acte--
            //
            SeanceDaysControllerTests_Service.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Create));
            SeanceDaysControllerTests_Service.ValidateViewModel(controller,seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Create(Default_SeanceDayFormView);
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
        public void Create_InValide_SeanceDay_Post_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = TestService.CreateInValideSeanceDayInstance();
            if (seanceday == null) return;
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(controller._UnitOfWork);

            // Acte
            SeanceDaysControllerTests_Service.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Create));
            List<ValidationResult>  ls_validation_errors = SeanceDaysControllerTests_Service
                .ValidateViewModel(controller, seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Create(Default_SeanceDayFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancedayBLO.Validate(seanceday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

