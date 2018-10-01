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
    public class WarningTrainees_Delete_ControllerTests : ManagerControllerTests
    {
		WarningTraineesControllerTests_Service TestService = new WarningTraineesControllerTests_Service();

		[TestMethod()]
        public void WarningTrainees_Delete_ControllerTests_Test()
        {
            // Arrange
            WarningTraineesController controller = new WarningTraineesController();
            WarningTrainee warningtrainee = TestService.CreateOrLouadFirstWarningTrainee(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(warningtrainee.Id) as ViewResult;
            var WarningTraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(WarningTraineeDetailModelView, typeof(Default_Details_WarningTrainee_Model));
        }

        [TestMethod()]
        public void Delete_WarningTrainee_Post_Test()
        {
            // Arrange
            //
            // Create WarningTrainee to Delete
			            WarningTraineesController controller = new WarningTraineesController();
            WarningTrainee warningtrainee_to_delete = TestService.CreateValideWarningTraineeInstance(controller._UnitOfWork,controller.GAppContext);
            WarningTraineeBLO warningtraineeBLO = new WarningTraineeBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            warningtraineeBLO.Save(warningtrainee_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(warningtrainee_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_WarningTrainee_Test()
        {
            // Arrange
            WarningTraineesController controller = new WarningTraineesController();

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

