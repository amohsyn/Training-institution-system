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
    public class CalendarDays_Delete_ControllerTests : ManagerControllerTests
    {
		CalendarDaysControllerTests_Service TestService = new CalendarDaysControllerTests_Service();

		[TestMethod()]
        public void CalendarDays_Delete_ControllerTests_Test()
        {
            // Arrange
            CalendarDaysController controller = new CalendarDaysController();
            CalendarDay calendarday = TestService.CreateOrLouadFirstCalendarDay(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(calendarday.Id) as ViewResult;
            var CalendarDayDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(CalendarDayDetailModelView, typeof(Default_Details_CalendarDay_Model));
        }

        [TestMethod()]
        public void Delete_CalendarDay_Post_Test()
        {
            // Arrange
            //
            // Create CalendarDay to Delete
			            CalendarDaysController controller = new CalendarDaysController();
            CalendarDay calendarday_to_delete = TestService.CreateValideCalendarDayInstance(controller._UnitOfWork,controller.GAppContext);
            CalendarDayBLO calendardayBLO = new CalendarDayBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            calendardayBLO.Save(calendarday_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(calendarday_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_CalendarDay_Test()
        {
            // Arrange
            CalendarDaysController controller = new CalendarDaysController();

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

