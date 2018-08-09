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
    public class Edit_ModuleTrainingsControllerTests : ManagerControllerTests
    {
		ModuleTrainingsControllerTests_Service TestService = new ModuleTrainingsControllerTests_Service();

		[TestMethod()]
        public void EditGet_ModuleTraining_Not_Exist_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ModuleTraining_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining =  TestService.CreateOrLouadFirstModuleTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(moduletraining.Id) as ViewResult;
            var ModuleTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ModuleTrainingDetailModelView, typeof(Default_ModuleTrainingFormView));
        }

        [TestMethod()]
        public void Edit_Valide_ModuleTraining_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ModuleTraining));

            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ModuleTraining moduletraining = TestService.CreateOrLouadFirstModuleTraining(new UnitOfWork());
			 
       

            // Acte
            ModuleTrainingsControllerTests_Service.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Edit));
            ModuleTrainingsControllerTests_Service.ValidateViewModel(controller, moduletraining);

			Default_ModuleTrainingFormView Default_ModuleTrainingFormView = new Default_ModuleTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_ModuleTrainingFormView(moduletraining);
            var result = controller.Edit(Default_ModuleTrainingFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ModuleTraining_Post_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = TestService.CreateInValideModuleTrainingInstance_ForEdit(new UnitOfWork());
            if (moduletraining == null) return;
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(controller._UnitOfWork);

            // Acte
            ModuleTrainingsControllerTests_Service.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Edit));
            List<ValidationResult> ls_validation_errors = ModuleTrainingsControllerTests_Service
                .ValidateViewModel(controller, moduletraining);

			Default_ModuleTrainingFormView Default_ModuleTrainingFormView = new Default_ModuleTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_ModuleTrainingFormView(moduletraining);
            var result = controller.Edit(Default_ModuleTrainingFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = moduletrainingBLO.Validate(moduletraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

