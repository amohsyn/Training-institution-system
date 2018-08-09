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
    public class Edit_SeancePlanningsControllerTests : ManagerControllerTests
    {
		SeancePlanningsControllerTests_Service TestService = new SeancePlanningsControllerTests_Service();

		[TestMethod()]
        public void EditGet_SeancePlanning_Not_Exist_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeancePlanning_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning =  TestService.CreateOrLouadFirstSeancePlanning(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seanceplanning.Id) as ViewResult;
            var SeancePlanningDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeancePlanningDetailModelView, typeof(Default_SeancePlanningFormView));
        }

        [TestMethod()]
        public void Edit_Valide_SeancePlanning_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeancePlanning));

            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeancePlanning seanceplanning = TestService.CreateOrLouadFirstSeancePlanning(new UnitOfWork());
			 
       

            // Acte
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Edit));
            SeancePlanningsControllerTests_Service.ValidateViewModel(controller, seanceplanning);

			Default_SeancePlanningFormView Default_SeancePlanningFormView = new Default_SeancePlanningFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeancePlanningFormView(seanceplanning);
            var result = controller.Edit(Default_SeancePlanningFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeancePlanning_Post_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = TestService.CreateInValideSeancePlanningInstance_ForEdit(new UnitOfWork());
            if (seanceplanning == null) return;
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(controller._UnitOfWork);

            // Acte
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Edit));
            List<ValidationResult> ls_validation_errors = SeancePlanningsControllerTests_Service
                .ValidateViewModel(controller, seanceplanning);

			Default_SeancePlanningFormView Default_SeancePlanningFormView = new Default_SeancePlanningFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeancePlanningFormView(seanceplanning);
            var result = controller.Edit(Default_SeancePlanningFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seanceplanningBLO.Validate(seanceplanning);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

