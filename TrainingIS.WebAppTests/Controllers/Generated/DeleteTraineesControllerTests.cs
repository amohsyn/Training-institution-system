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
    public class Delete_TraineesControllerTests : ManagerControllerTests
    {
		TraineesControllerTests_Service TestService = new TraineesControllerTests_Service();

		[TestMethod()]
        public void Delete_Trainee_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Trainee));
			 
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = TestService.CreateOrLouadFirstTrainee(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(trainee.Id) as ViewResult;
            var TraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Default_TraineeDetailsView));
        }

        [TestMethod()]
        public void Delete_Trainee_Post_Test()
        {
            // Arrange
            //
            // Create Trainee to Delete
            Trainee trainee_to_delete = TestService.CreateValideTraineeInstance();
            TraineeBLO traineeBLO = new TraineeBLO(new UnitOfWork());
            traineeBLO.Save(trainee_to_delete);
            TraineesController controller = new TraineesController();

            // Acte
            var result = controller.DeleteConfirmed(trainee_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Trainee_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        } 
    }
}

