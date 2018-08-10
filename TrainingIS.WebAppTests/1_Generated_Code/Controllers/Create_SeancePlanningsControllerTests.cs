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
    public class Create_SeancePlanningsControllerTests : ManagerControllerTests
    {
		SeancePlanningsControllerTests_Service TestService = new SeancePlanningsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            SeancePlanningsController SeancePlanningsController = new SeancePlanningsController();

            ViewResult viewResult = SeancePlanningsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_SeancePlanning_Post_Test()
        {
            //--Arrange--
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = TestService.CreateValideSeancePlanningInstance();

            //--Acte--
            //
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Create));
            SeancePlanningsControllerTests_Service.ValidateViewModel(controller,seanceplanning);

			Default_SeancePlanningFormView Default_SeancePlanningFormView = new Default_SeancePlanningFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeancePlanningFormView(seanceplanning);
            var result = controller.Create(Default_SeancePlanningFormView);
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
        public void Create_InValide_SeancePlanning_Post_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = TestService.CreateInValideSeancePlanningInstance();
            if (seanceplanning == null) return;
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(controller._UnitOfWork);

            // Acte
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Create));
            List<ValidationResult>  ls_validation_errors = SeancePlanningsControllerTests_Service
                .ValidateViewModel(controller, seanceplanning);

			Default_SeancePlanningFormView Default_SeancePlanningFormView = new Default_SeancePlanningFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeancePlanningFormView(seanceplanning);
            var result = controller.Create(Default_SeancePlanningFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seanceplanningBLO.Validate(seanceplanning);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

