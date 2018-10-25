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
    public class Meetings_Delete_ControllerTests : ManagerControllerTests
    {
		MeetingsControllerTests_Service TestService = new MeetingsControllerTests_Service();

		[TestMethod()]
        public void Meetings_Delete_ControllerTests_Test()
        {
            // Arrange
            MeetingsController controller = new MeetingsController();
            Meeting meeting = TestService.CreateOrLouadFirstMeeting(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(meeting.Id) as ViewResult;
            var MeetingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(MeetingDetailModelView, typeof(Details_Meeting_Model));
        }

        [TestMethod()]
        public void Delete_Meeting_Post_Test()
        {
            // Arrange
            //
            // Create Meeting to Delete
			            MeetingsController controller = new MeetingsController();
            Meeting meeting_to_delete = TestService.CreateValideMeetingInstance(controller._UnitOfWork,controller.GAppContext);
            MeetingBLO meetingBLO = new MeetingBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            meetingBLO.Save(meeting_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(meeting_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Meeting_Test()
        {
            // Arrange
            MeetingsController controller = new MeetingsController();

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

