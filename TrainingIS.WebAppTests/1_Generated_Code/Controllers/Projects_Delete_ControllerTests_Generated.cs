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
    public class Projects_Delete_ControllerTests : ManagerControllerTests
    {
		ProjectsControllerTests_Service TestService = new ProjectsControllerTests_Service();

		[TestMethod()]
        public void Projects_Delete_ControllerTests_Test()
        {
            // Arrange
            ProjectsController controller = new ProjectsController();
            Project project = TestService.CreateOrLouadFirstProject(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(project.Id) as ViewResult;
            var ProjectDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ProjectDetailModelView, typeof(Default_Details_Project_Model));
        }

        [TestMethod()]
        public void Delete_Project_Post_Test()
        {
            // Arrange
            //
            // Create Project to Delete
			            ProjectsController controller = new ProjectsController();
            Project project_to_delete = TestService.CreateValideProjectInstance(controller._UnitOfWork,controller.GAppContext);
            ProjectBLO projectBLO = new ProjectBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            projectBLO.Save(project_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(project_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Project_Test()
        {
            // Arrange
            ProjectsController controller = new ProjectsController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

