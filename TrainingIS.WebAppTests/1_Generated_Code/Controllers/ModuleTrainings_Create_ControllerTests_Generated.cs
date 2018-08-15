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
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_ModuleTrainingsControllerTests : ManagerControllerTests
    {
		ModuleTrainingsControllerTests_Service TestService = new ModuleTrainingsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            ModuleTrainingsController ModuleTrainingsController = new ModuleTrainingsController();

            ViewResult viewResult = ModuleTrainingsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ModuleTraining_Post_Test()
        {
            //--Arrange--
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = TestService.CreateValideModuleTrainingInstance();

            //--Acte--
            //
            ModuleTrainingsControllerTests_Service.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Create));
            ModuleTrainingsControllerTests_Service.ValidateViewModel(controller,moduletraining);

			Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = new Default_Form_ModuleTraining_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_ModuleTraining_Model(moduletraining);
            var result = controller.Create(Default_Form_ModuleTraining_Model);
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
        public void Create_InValide_ModuleTraining_Post_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = TestService.CreateInValideModuleTrainingInstance();
            if (moduletraining == null) return;
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(controller._UnitOfWork);

            // Acte
            ModuleTrainingsControllerTests_Service.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Create));
            List<ValidationResult>  ls_validation_errors = ModuleTrainingsControllerTests_Service
                .ValidateViewModel(controller, moduletraining);

			Default_Form_ModuleTraining_Model Default_Form_ModuleTraining_Model = new Default_Form_ModuleTraining_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_ModuleTraining_Model(moduletraining);
            var result = controller.Create(Default_Form_ModuleTraining_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = moduletrainingBLO.Validate(moduletraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

