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
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.BLL.VO;
using GApp.BLL.Enums;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Create_MeetingsControllerTests : ManagerControllerTests
    {
		MeetingsControllerTests_Service TestService = new MeetingsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            MeetingsController MeetingsController = new MeetingsController();

            ViewResult viewResult = MeetingsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Meeting_Post_Test()
        {
            //--Arrange--
            MeetingsController controller = new MeetingsController();
            Meeting meeting = TestService.CreateValideMeetingInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            MeetingsControllerTests_Service.PreBindModel(controller, meeting, nameof(MeetingsController.Create));
            MeetingsControllerTests_Service.ValidateViewModel(controller,meeting);

			Form_Meeting_Model Form_Meeting_Model = new Form_Meeting_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_Meeting_Model(meeting);
            var result = controller.Create(Form_Meeting_Model);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_Meeting_Post_Test()
        {
            // Arrange
            MeetingsController controller = new MeetingsController();
            Meeting meeting = TestService.CreateInValideMeetingInstance(controller._UnitOfWork,controller.GAppContext);
            if (meeting == null) return;
            MeetingBLO meetingBLO = new MeetingBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            MeetingsControllerTests_Service.PreBindModel(controller, meeting, nameof(MeetingsController.Create));
            List<ValidationResult>  ls_validation_errors = MeetingsControllerTests_Service
                .ValidateViewModel(controller, meeting);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Form_Meeting_Model Form_Meeting_Model = new Form_Meeting_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_Meeting_Model(meeting);
            var result = controller.Create(Form_Meeting_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = meetingBLO.Validate(meeting);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

