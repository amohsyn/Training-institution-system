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
    public class TrainingLevels_Delete_ControllerTests : ManagerControllerTests
    {
		TrainingLevelsControllerTests_Service TestService = new TrainingLevelsControllerTests_Service();

		[TestMethod()]
        public void TrainingLevels_Delete_ControllerTests_Test()
        {
            // Arrange
            TrainingLevelsController controller = new TrainingLevelsController();
            TrainingLevel traininglevel = TestService.CreateOrLouadFirstTrainingLevel(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(traininglevel.Id) as ViewResult;
            var TrainingLevelDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingLevelDetailModelView, typeof(Default_Details_TrainingLevel_Model));
        }

        [TestMethod()]
        public void Delete_TrainingLevel_Post_Test()
        {
            // Arrange
            //
            // Create TrainingLevel to Delete
			            TrainingLevelsController controller = new TrainingLevelsController();
            TrainingLevel traininglevel_to_delete = TestService.CreateValideTrainingLevelInstance(controller._UnitOfWork,controller.GAppContext);
            TrainingLevelBLO traininglevelBLO = new TrainingLevelBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            traininglevelBLO.Save(traininglevel_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(traininglevel_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_TrainingLevel_Test()
        {
            // Arrange
            TrainingLevelsController controller = new TrainingLevelsController();

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

