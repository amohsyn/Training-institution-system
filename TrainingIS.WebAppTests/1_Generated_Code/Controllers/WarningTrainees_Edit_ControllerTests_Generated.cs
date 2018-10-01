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
    public class WarningTrainees_Edit_ControllerTests : ManagerControllerTests
    {
		WarningTraineesControllerTests_Service TestService = new WarningTraineesControllerTests_Service();

		[TestMethod()]
        public void EditGet_WarningTrainee_Not_Exist_Test()
        {
            // Arrange
            WarningTraineesController controller = new WarningTraineesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_WarningTrainee_Test()
        {
            // Arrange
            WarningTraineesController controller = new WarningTraineesController();
            WarningTrainee warningtrainee =  TestService.CreateOrLouadFirstWarningTrainee(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(warningtrainee.Id) as ViewResult;
            var WarningTraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(WarningTraineeDetailModelView, typeof(Default_Form_WarningTrainee_Model));
        }

        [TestMethod()]
        public void Edit_Valide_WarningTrainee_Post_Test()
        {

            // Arrange
            WarningTraineesController controller = new WarningTraineesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            WarningTrainee warningtrainee = TestService.CreateOrLouadFirstWarningTrainee(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            WarningTraineesControllerTests_Service.PreBindModel(controller, warningtrainee, nameof(WarningTraineesController.Edit));
            WarningTraineesControllerTests_Service.ValidateViewModel(controller, warningtrainee);

			Default_Form_WarningTrainee_Model Default_Form_WarningTrainee_Model = new Default_Form_WarningTrainee_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_WarningTrainee_Model(warningtrainee);
            var result = controller.Edit(Default_Form_WarningTrainee_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_WarningTrainee_Post_Test()
        {
            // Arrange
            WarningTraineesController controller = new WarningTraineesController();
            WarningTrainee warningtrainee = TestService.CreateInValideWarningTraineeInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (warningtrainee == null) return;
            WarningTraineeBLO warningtraineeBLO = new WarningTraineeBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            WarningTraineesControllerTests_Service.PreBindModel(controller, warningtrainee, nameof(WarningTraineesController.Edit));
            List<ValidationResult> ls_validation_errors = WarningTraineesControllerTests_Service
                .ValidateViewModel(controller, warningtrainee);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_WarningTrainee_Model Default_Form_WarningTrainee_Model = new Default_Form_WarningTrainee_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_WarningTrainee_Model(warningtrainee);
            var result = controller.Edit(Default_Form_WarningTrainee_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = warningtraineeBLO.Validate(warningtrainee);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

