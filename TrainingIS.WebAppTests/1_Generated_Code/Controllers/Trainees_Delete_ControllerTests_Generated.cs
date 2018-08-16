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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
    public class Trainees_Delete_ControllerTests : ManagerControllerTests
    {
		TraineesControllerTests_Service TestService = new TraineesControllerTests_Service();

		[TestMethod()]
        public void Trainees_Delete_ControllerTests_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = TestService.CreateOrLouadFirstTrainee(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(trainee.Id) as ViewResult;
            var TraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Default_Details_Trainee_Model));
        }

        [TestMethod()]
        public void Delete_Trainee_Post_Test()
        {
            // Arrange
            //
            // Create Trainee to Delete
			            TraineesController controller = new TraineesController();
            Trainee trainee_to_delete = TestService.CreateValideTraineeInstance(controller._UnitOfWork,controller.GAppContext);
            TraineeBLO traineeBLO = new TraineeBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            traineeBLO.Save(trainee_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(trainee_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

