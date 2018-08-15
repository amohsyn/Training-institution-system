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
    public class SeanceDays_Delete_ControllerTests : ManagerControllerTests
    {
		SeanceDaysControllerTests_Service TestService = new SeanceDaysControllerTests_Service();

		[TestMethod()]
        public void SeanceDays_Delete_ControllerTests_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = TestService.CreateOrLouadFirstSeanceDay(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(seanceday.Id) as ViewResult;
            var SeanceDayDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceDayDetailModelView, typeof(Default_Details_SeanceDay_Model));
        }

        [TestMethod()]
        public void Delete_SeanceDay_Post_Test()
        {
            // Arrange
            //
            // Create SeanceDay to Delete
            SeanceDay seanceday_to_delete = TestService.CreateValideSeanceDayInstance();
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(new UnitOfWork<TrainingISModel>());
            seancedayBLO.Save(seanceday_to_delete);
            SeanceDaysController controller = new SeanceDaysController();

            // Acte
            var result = controller.DeleteConfirmed(seanceday_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeanceDay_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();

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

