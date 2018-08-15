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
    public class SeancePlannings_Delete_ControllerTests : ManagerControllerTests
    {
		SeancePlanningsControllerTests_Service TestService = new SeancePlanningsControllerTests_Service();

		[TestMethod()]
        public void SeancePlannings_Delete_ControllerTests_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = TestService.CreateOrLouadFirstSeancePlanning(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(seanceplanning.Id) as ViewResult;
            var SeancePlanningDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeancePlanningDetailModelView, typeof(Default_Details_SeancePlanning_Model));
        }

        [TestMethod()]
        public void Delete_SeancePlanning_Post_Test()
        {
            // Arrange
            //
            // Create SeancePlanning to Delete
            SeancePlanning seanceplanning_to_delete = TestService.CreateValideSeancePlanningInstance();
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(new UnitOfWork<TrainingISModel>());
            seanceplanningBLO.Save(seanceplanning_to_delete);
            SeancePlanningsController controller = new SeancePlanningsController();

            // Acte
            var result = controller.DeleteConfirmed(seanceplanning_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeancePlanning_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();

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

