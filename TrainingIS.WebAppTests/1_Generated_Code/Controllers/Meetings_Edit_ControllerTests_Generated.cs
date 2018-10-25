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
    public class Meetings_Edit_ControllerTests : ManagerControllerTests
    {
		MeetingsControllerTests_Service TestService = new MeetingsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Meeting_Not_Exist_Test()
        {
            // Arrange
            MeetingsController controller = new MeetingsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Meeting_Test()
        {
            // Arrange
            MeetingsController controller = new MeetingsController();
            Meeting meeting =  TestService.CreateOrLouadFirstMeeting(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(meeting.Id) as ViewResult;
            var MeetingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(MeetingDetailModelView, typeof(Form_Meeting_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Meeting_Post_Test()
        {

            // Arrange
            MeetingsController controller = new MeetingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Meeting meeting = TestService.CreateOrLouadFirstMeeting(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            MeetingsControllerTests_Service.PreBindModel(controller, meeting, nameof(MeetingsController.Edit));
            MeetingsControllerTests_Service.ValidateViewModel(controller, meeting);

			Form_Meeting_Model Form_Meeting_Model = new Form_Meeting_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_Meeting_Model(meeting);
            var result = controller.Edit(Form_Meeting_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Meeting_Post_Test()
        {
            // Arrange
            MeetingsController controller = new MeetingsController();
            Meeting meeting = TestService.CreateInValideMeetingInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (meeting == null) return;
            MeetingBLO meetingBLO = new MeetingBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            MeetingsControllerTests_Service.PreBindModel(controller, meeting, nameof(MeetingsController.Edit));
            List<ValidationResult> ls_validation_errors = MeetingsControllerTests_Service
                .ValidateViewModel(controller, meeting);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Form_Meeting_Model Form_Meeting_Model = new Form_Meeting_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_Meeting_Model(meeting);
            var result = controller.Edit(Form_Meeting_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = meetingBLO.Validate(meeting);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

