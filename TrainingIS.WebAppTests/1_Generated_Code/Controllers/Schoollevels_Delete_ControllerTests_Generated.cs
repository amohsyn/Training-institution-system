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
    public class Schoollevels_Delete_ControllerTests : ManagerControllerTests
    {
		SchoollevelsControllerTests_Service TestService = new SchoollevelsControllerTests_Service();

		[TestMethod()]
        public void Schoollevels_Delete_ControllerTests_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = TestService.CreateOrLouadFirstSchoollevel(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(schoollevel.Id) as ViewResult;
            var SchoollevelDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SchoollevelDetailModelView, typeof(Default_Details_Schoollevel_Model));
        }

        [TestMethod()]
        public void Delete_Schoollevel_Post_Test()
        {
            // Arrange
            //
            // Create Schoollevel to Delete
			            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel_to_delete = TestService.CreateValideSchoollevelInstance(controller._UnitOfWork,controller.GAppContext);
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            schoollevelBLO.Save(schoollevel_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(schoollevel_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Schoollevel_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();

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

