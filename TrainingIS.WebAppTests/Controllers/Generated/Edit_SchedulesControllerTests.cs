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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Edit_SchedulesControllerTests : ManagerControllerTests
    {
		SchedulesControllerTests_Service TestService = new SchedulesControllerTests_Service();

		[TestMethod()]
        public void EditGet_Schedule_Not_Exist_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Schedule_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();
            Schedule schedule =  TestService.CreateOrLouadFirstSchedule(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(schedule.Id) as ViewResult;
            var ScheduleDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ScheduleDetailModelView, typeof(Default_ScheduleFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Schedule_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Schedule));

            // Arrange
            SchedulesController controller = new SchedulesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Schedule schedule = TestService.CreateOrLouadFirstSchedule(new UnitOfWork());
			 
       

            // Acte
            SchedulesControllerTests_Service.PreBindModel(controller, schedule, nameof(SchedulesController.Edit));
            SchedulesControllerTests_Service.ValidateViewModel(controller, schedule);

			Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormViewBLM(controller._UnitOfWork).ConverTo_Default_ScheduleFormView(schedule);
            var result = controller.Edit(Default_ScheduleFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Schedule_Post_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();
            Schedule schedule = TestService.CreateInValideScheduleInstance_ForEdit(new UnitOfWork());
            if (schedule == null) return;
            ScheduleBLO scheduleBLO = new ScheduleBLO(controller._UnitOfWork);

            // Acte
            SchedulesControllerTests_Service.PreBindModel(controller, schedule, nameof(SchedulesController.Edit));
            List<ValidationResult> ls_validation_errors = SchedulesControllerTests_Service
                .ValidateViewModel(controller, schedule);

			Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormViewBLM(controller._UnitOfWork).ConverTo_Default_ScheduleFormView(schedule);
            var result = controller.Edit(Default_ScheduleFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = scheduleBLO.Validate(schedule);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

