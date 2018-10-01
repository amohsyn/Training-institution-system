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
    public class TaskProjects_Delete_ControllerTests : ManagerControllerTests
    {
		TaskProjectsControllerTests_Service TestService = new TaskProjectsControllerTests_Service();

		[TestMethod()]
        public void TaskProjects_Delete_ControllerTests_Test()
        {
            // Arrange
            TaskProjectsController controller = new TaskProjectsController();
            TaskProject taskproject = TestService.CreateOrLouadFirstTaskProject(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(taskproject.Id) as ViewResult;
            var TaskProjectDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TaskProjectDetailModelView, typeof(Default_Details_TaskProject_Model));
        }

        [TestMethod()]
        public void Delete_TaskProject_Post_Test()
        {
            // Arrange
            //
            // Create TaskProject to Delete
			            TaskProjectsController controller = new TaskProjectsController();
            TaskProject taskproject_to_delete = TestService.CreateValideTaskProjectInstance(controller._UnitOfWork,controller.GAppContext);
            TaskProjectBLO taskprojectBLO = new TaskProjectBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            taskprojectBLO.Save(taskproject_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(taskproject_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_TaskProject_Test()
        {
            // Arrange
            TaskProjectsController controller = new TaskProjectsController();

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

