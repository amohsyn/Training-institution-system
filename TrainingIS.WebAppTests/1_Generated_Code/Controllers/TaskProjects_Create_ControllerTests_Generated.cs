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
    public class Create_TaskProjectsControllerTests : ManagerControllerTests
    {
		TaskProjectsControllerTests_Service TestService = new TaskProjectsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            TaskProjectsController TaskProjectsController = new TaskProjectsController();

            ViewResult viewResult = TaskProjectsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_TaskProject_Post_Test()
        {
            //--Arrange--
            TaskProjectsController controller = new TaskProjectsController();
            TaskProject taskproject = TestService.CreateValideTaskProjectInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            TaskProjectsControllerTests_Service.PreBindModel(controller, taskproject, nameof(TaskProjectsController.Create));
            TaskProjectsControllerTests_Service.ValidateViewModel(controller,taskproject);

			Default_Form_TaskProject_Model Default_Form_TaskProject_Model = new Default_Form_TaskProject_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TaskProject_Model(taskproject);
            var result = controller.Create(Default_Form_TaskProject_Model);
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
        public void Create_InValide_TaskProject_Post_Test()
        {
            // Arrange
            TaskProjectsController controller = new TaskProjectsController();
            TaskProject taskproject = TestService.CreateInValideTaskProjectInstance(controller._UnitOfWork,controller.GAppContext);
            if (taskproject == null) return;
            TaskProjectBLO taskprojectBLO = new TaskProjectBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            TaskProjectsControllerTests_Service.PreBindModel(controller, taskproject, nameof(TaskProjectsController.Create));
            List<ValidationResult>  ls_validation_errors = TaskProjectsControllerTests_Service
                .ValidateViewModel(controller, taskproject);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_TaskProject_Model Default_Form_TaskProject_Model = new Default_Form_TaskProject_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TaskProject_Model(taskproject);
            var result = controller.Create(Default_Form_TaskProject_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = taskprojectBLO.Validate(taskproject);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

