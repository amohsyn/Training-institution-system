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
    public class Schedules_Delete_ControllerTests : ManagerControllerTests
    {
		SchedulesControllerTests_Service TestService = new SchedulesControllerTests_Service();

		[TestMethod()]
        public void Schedules_Delete_ControllerTests_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();
            Schedule schedule = TestService.CreateOrLouadFirstSchedule(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(schedule.Id) as ViewResult;
            var ScheduleDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ScheduleDetailModelView, typeof(Details_Schedule_Model));
        }

        [TestMethod()]
        public void Delete_Schedule_Post_Test()
        {
            // Arrange
            //
            // Create Schedule to Delete
			            SchedulesController controller = new SchedulesController();
            Schedule schedule_to_delete = TestService.CreateValideScheduleInstance(controller._UnitOfWork,controller.GAppContext);
            ScheduleBLO scheduleBLO = new ScheduleBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            scheduleBLO.Save(schedule_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(schedule_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Schedule_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();

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

