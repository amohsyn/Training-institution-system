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
    public class Edit_TrainingTypesControllerTests : ManagerControllerTests
    {
		TrainingTypesControllerTests_Service TestService = new TrainingTypesControllerTests_Service();

		[TestMethod()]
        public void EditGet_TrainingType_Not_Exist_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_TrainingType_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype =  TestService.CreateOrLouadFirstTrainingType(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(trainingtype.Id) as ViewResult;
            var TrainingTypeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingTypeDetailModelView, typeof(Default_TrainingTypeFormView));
        }

        [TestMethod()]
        public void Edit_Valide_TrainingType_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(TrainingType));

            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            TrainingType trainingtype = TestService.CreateOrLouadFirstTrainingType(new UnitOfWork());
			 
       

            // Acte
            TrainingTypesControllerTests_Service.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Edit));
            TrainingTypesControllerTests_Service.ValidateViewModel(controller, trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Edit(Default_TrainingTypeFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_TrainingType_Post_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = TestService.CreateInValideTrainingTypeInstance_ForEdit(new UnitOfWork());
            if (trainingtype == null) return;
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(controller._UnitOfWork);

            // Acte
            TrainingTypesControllerTests_Service.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingTypesControllerTests_Service
                .ValidateViewModel(controller, trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Edit(Default_TrainingTypeFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingtypeBLO.Validate(trainingtype);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

