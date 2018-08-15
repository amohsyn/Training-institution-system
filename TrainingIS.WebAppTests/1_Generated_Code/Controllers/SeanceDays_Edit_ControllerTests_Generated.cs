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
    public class SeanceDays_Edit_ControllerTests : ManagerControllerTests
    {
		SeanceDaysControllerTests_Service TestService = new SeanceDaysControllerTests_Service();

		[TestMethod()]
        public void EditGet_SeanceDay_Not_Exist_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceDay_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday =  TestService.CreateOrLouadFirstSeanceDay(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seanceday.Id) as ViewResult;
            var SeanceDayDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceDayDetailModelView, typeof(Default_Form_SeanceDay_Model));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceDay_Post_Test()
        {

            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceDay seanceday = TestService.CreateOrLouadFirstSeanceDay(new UnitOfWork<TrainingISModel>());
			 
       

            // Acte
            SeanceDaysControllerTests_Service.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Edit));
            SeanceDaysControllerTests_Service.ValidateViewModel(controller, seanceday);

			Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model = new Default_Form_SeanceDay_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_SeanceDay_Model(seanceday);
            var result = controller.Edit(Default_Form_SeanceDay_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceDay_Post_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = TestService.CreateInValideSeanceDayInstance_ForEdit(new UnitOfWork<TrainingISModel>());
            if (seanceday == null) return;
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(controller._UnitOfWork);

            // Acte
            SeanceDaysControllerTests_Service.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceDaysControllerTests_Service
                .ValidateViewModel(controller, seanceday);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_SeanceDay_Model Default_Form_SeanceDay_Model = new Default_Form_SeanceDay_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_SeanceDay_Model(seanceday);
            var result = controller.Edit(Default_Form_SeanceDay_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancedayBLO.Validate(seanceday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

