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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
    public class SeanceTrainings_Delete_ControllerTests : ManagerControllerTests
    {
		SeanceTrainingsControllerTests_Service TestService = new SeanceTrainingsControllerTests_Service();

		[TestMethod()]
        public void SeanceTrainings_Delete_ControllerTests_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = TestService.CreateOrLouadFirstSeanceTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(seancetraining.Id) as ViewResult;
            var SeanceTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceTrainingDetailModelView, typeof(Default_Details_SeanceTraining_Model));
        }

        [TestMethod()]
        public void Delete_SeanceTraining_Post_Test()
        {
            // Arrange
            //
            // Create SeanceTraining to Delete
            SeanceTraining seancetraining_to_delete = TestService.CreateValideSeanceTrainingInstance();
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(new UnitOfWork<TrainingISModel>());
            seancetrainingBLO.Save(seancetraining_to_delete);
            SeanceTrainingsController controller = new SeanceTrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(seancetraining_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeanceTraining_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();

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

