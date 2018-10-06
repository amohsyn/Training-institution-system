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
    public class CalendarDays_Edit_ControllerTests : ManagerControllerTests
    {
		CalendarDaysControllerTests_Service TestService = new CalendarDaysControllerTests_Service();

		[TestMethod()]
        public void EditGet_CalendarDay_Not_Exist_Test()
        {
            // Arrange
            CalendarDaysController controller = new CalendarDaysController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_CalendarDay_Test()
        {
            // Arrange
            CalendarDaysController controller = new CalendarDaysController();
            CalendarDay calendarday =  TestService.CreateOrLouadFirstCalendarDay(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(calendarday.Id) as ViewResult;
            var CalendarDayDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(CalendarDayDetailModelView, typeof(Default_Form_CalendarDay_Model));
        }

        [TestMethod()]
        public void Edit_Valide_CalendarDay_Post_Test()
        {

            // Arrange
            CalendarDaysController controller = new CalendarDaysController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            CalendarDay calendarday = TestService.CreateOrLouadFirstCalendarDay(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            CalendarDaysControllerTests_Service.PreBindModel(controller, calendarday, nameof(CalendarDaysController.Edit));
            CalendarDaysControllerTests_Service.ValidateViewModel(controller, calendarday);

			Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = new Default_Form_CalendarDay_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_CalendarDay_Model(calendarday);
            var result = controller.Edit(Default_Form_CalendarDay_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_CalendarDay_Post_Test()
        {
            // Arrange
            CalendarDaysController controller = new CalendarDaysController();
            CalendarDay calendarday = TestService.CreateInValideCalendarDayInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (calendarday == null) return;
            CalendarDayBLO calendardayBLO = new CalendarDayBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            CalendarDaysControllerTests_Service.PreBindModel(controller, calendarday, nameof(CalendarDaysController.Edit));
            List<ValidationResult> ls_validation_errors = CalendarDaysControllerTests_Service
                .ValidateViewModel(controller, calendarday);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = new Default_Form_CalendarDay_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_CalendarDay_Model(calendarday);
            var result = controller.Edit(Default_Form_CalendarDay_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = calendardayBLO.Validate(calendarday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

