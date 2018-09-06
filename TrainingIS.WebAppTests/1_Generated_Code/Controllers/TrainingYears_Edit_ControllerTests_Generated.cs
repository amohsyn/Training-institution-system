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
    public class TrainingYears_Edit_ControllerTests : ManagerControllerTests
    {
		TrainingYearsControllerTests_Service TestService = new TrainingYearsControllerTests_Service();

		[TestMethod()]
        public void EditGet_TrainingYear_Not_Exist_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_TrainingYear_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear =  TestService.CreateOrLouadFirstTrainingYear(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(trainingyear.Id) as ViewResult;
            var TrainingYearDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingYearDetailModelView, typeof(Default_Form_TrainingYear_Model));
        }

        [TestMethod()]
        public void Edit_Valide_TrainingYear_Post_Test()
        {

            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            TrainingYear trainingyear = TestService.CreateOrLouadFirstTrainingYear(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            TrainingYearsControllerTests_Service.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Edit));
            TrainingYearsControllerTests_Service.ValidateViewModel(controller, trainingyear);

			Default_Form_TrainingYear_Model Default_Form_TrainingYear_Model = new Default_Form_TrainingYear_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TrainingYear_Model(trainingyear);
            var result = controller.Edit(Default_Form_TrainingYear_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_TrainingYear_Post_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = TestService.CreateInValideTrainingYearInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (trainingyear == null) return;
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            TrainingYearsControllerTests_Service.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingYearsControllerTests_Service
                .ValidateViewModel(controller, trainingyear);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_TrainingYear_Model Default_Form_TrainingYear_Model = new Default_Form_TrainingYear_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TrainingYear_Model(trainingyear);
            var result = controller.Edit(Default_Form_TrainingYear_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingyearBLO.Validate(trainingyear);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

