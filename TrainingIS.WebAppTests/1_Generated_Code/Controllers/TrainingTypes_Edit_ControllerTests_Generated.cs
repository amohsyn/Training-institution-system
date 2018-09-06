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
    public class TrainingTypes_Edit_ControllerTests : ManagerControllerTests
    {
		TrainingTypesControllerTests_Service TestService = new TrainingTypesControllerTests_Service();

		[TestMethod()]
        public void EditGet_TrainingType_Not_Exist_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_TrainingType_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype =  TestService.CreateOrLouadFirstTrainingType(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(trainingtype.Id) as ViewResult;
            var TrainingTypeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingTypeDetailModelView, typeof(Default_Form_TrainingType_Model));
        }

        [TestMethod()]
        public void Edit_Valide_TrainingType_Post_Test()
        {

            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            TrainingType trainingtype = TestService.CreateOrLouadFirstTrainingType(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            TrainingTypesControllerTests_Service.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Edit));
            TrainingTypesControllerTests_Service.ValidateViewModel(controller, trainingtype);

			Default_Form_TrainingType_Model Default_Form_TrainingType_Model = new Default_Form_TrainingType_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TrainingType_Model(trainingtype);
            var result = controller.Edit(Default_Form_TrainingType_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_TrainingType_Post_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = TestService.CreateInValideTrainingTypeInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (trainingtype == null) return;
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            TrainingTypesControllerTests_Service.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingTypesControllerTests_Service
                .ValidateViewModel(controller, trainingtype);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_TrainingType_Model Default_Form_TrainingType_Model = new Default_Form_TrainingType_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TrainingType_Model(trainingtype);
            var result = controller.Edit(Default_Form_TrainingType_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingtypeBLO.Validate(trainingtype);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

