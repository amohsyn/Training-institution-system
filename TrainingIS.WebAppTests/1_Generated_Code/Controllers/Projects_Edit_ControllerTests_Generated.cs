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
    public class Projects_Edit_ControllerTests : ManagerControllerTests
    {
		ProjectsControllerTests_Service TestService = new ProjectsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Project_Not_Exist_Test()
        {
            // Arrange
            ProjectsController controller = new ProjectsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Project_Test()
        {
            // Arrange
            ProjectsController controller = new ProjectsController();
            Project project =  TestService.CreateOrLouadFirstProject(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(project.Id) as ViewResult;
            var ProjectDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ProjectDetailModelView, typeof(Default_Form_Project_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Project_Post_Test()
        {

            // Arrange
            ProjectsController controller = new ProjectsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Project project = TestService.CreateOrLouadFirstProject(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            ProjectsControllerTests_Service.PreBindModel(controller, project, nameof(ProjectsController.Edit));
            ProjectsControllerTests_Service.ValidateViewModel(controller, project);

			Default_Form_Project_Model Default_Form_Project_Model = new Default_Form_Project_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Project_Model(project);
            var result = controller.Edit(Default_Form_Project_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Project_Post_Test()
        {
            // Arrange
            ProjectsController controller = new ProjectsController();
            Project project = TestService.CreateInValideProjectInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (project == null) return;
            ProjectBLO projectBLO = new ProjectBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            ProjectsControllerTests_Service.PreBindModel(controller, project, nameof(ProjectsController.Edit));
            List<ValidationResult> ls_validation_errors = ProjectsControllerTests_Service
                .ValidateViewModel(controller, project);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Project_Model Default_Form_Project_Model = new Default_Form_Project_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Project_Model(project);
            var result = controller.Edit(Default_Form_Project_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = projectBLO.Validate(project);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

