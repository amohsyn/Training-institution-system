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
using GApp.Entities;
using GApp.BLL.Enums;
using GApp.BLL.VO;
using GApp.DAL;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class TaskProjects_Edit_ControllerTests : ManagerControllerTests
    {
		TaskProjectsControllerTests_Service TestService = new TaskProjectsControllerTests_Service();

		[TestMethod()]
        public void EditGet_TaskProject_Not_Exist_Test()
        {
            // Arrange
            TaskProjectsController controller = new TaskProjectsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_TaskProject_Test()
        {
            // Arrange
            TaskProjectsController controller = new TaskProjectsController();
            TaskProject taskproject =  TestService.CreateOrLouadFirstTaskProject(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(taskproject.Id) as ViewResult;
            var TaskProjectDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TaskProjectDetailModelView, typeof(Default_Form_TaskProject_Model));
        }

        [TestMethod()]
        public void Edit_Valide_TaskProject_Post_Test()
        {

            // Arrange
            TaskProjectsController controller = new TaskProjectsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            TaskProject taskproject = TestService.CreateOrLouadFirstTaskProject(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            TaskProjectsControllerTests_Service.PreBindModel(controller, taskproject, nameof(TaskProjectsController.Edit));
            TaskProjectsControllerTests_Service.ValidateViewModel(controller, taskproject);

			Default_Form_TaskProject_Model Default_Form_TaskProject_Model = new Default_Form_TaskProject_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TaskProject_Model(taskproject);
            var result = controller.Edit(Default_Form_TaskProject_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_TaskProject_Post_Test()
        {
            // Arrange
            TaskProjectsController controller = new TaskProjectsController();
            TaskProject taskproject = TestService.CreateInValideTaskProjectInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (taskproject == null) return;
            TaskProjectBLO taskprojectBLO = new TaskProjectBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            TaskProjectsControllerTests_Service.PreBindModel(controller, taskproject, nameof(TaskProjectsController.Edit));
            List<ValidationResult> ls_validation_errors = TaskProjectsControllerTests_Service
                .ValidateViewModel(controller, taskproject);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_TaskProject_Model Default_Form_TaskProject_Model = new Default_Form_TaskProject_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TaskProject_Model(taskproject);
            var result = controller.Edit(Default_Form_TaskProject_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = taskprojectBLO.Validate(taskproject);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

