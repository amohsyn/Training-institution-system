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
    public class Functions_Edit_ControllerTests : ManagerControllerTests
    {
		FunctionsControllerTests_Service TestService = new FunctionsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Function_Not_Exist_Test()
        {
            // Arrange
            FunctionsController controller = new FunctionsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Function_Test()
        {
            // Arrange
            FunctionsController controller = new FunctionsController();
            Function function =  TestService.CreateOrLouadFirstFunction(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(function.Id) as ViewResult;
            var FunctionDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FunctionDetailModelView, typeof(Default_Form_Function_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Function_Post_Test()
        {

            // Arrange
            FunctionsController controller = new FunctionsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Function function = TestService.CreateOrLouadFirstFunction(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            FunctionsControllerTests_Service.PreBindModel(controller, function, nameof(FunctionsController.Edit));
            FunctionsControllerTests_Service.ValidateViewModel(controller, function);

			Default_Form_Function_Model Default_Form_Function_Model = new Default_Form_Function_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Function_Model(function);
            var result = controller.Edit(Default_Form_Function_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Function_Post_Test()
        {
            // Arrange
            FunctionsController controller = new FunctionsController();
            Function function = TestService.CreateInValideFunctionInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (function == null) return;
            FunctionBLO functionBLO = new FunctionBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            FunctionsControllerTests_Service.PreBindModel(controller, function, nameof(FunctionsController.Edit));
            List<ValidationResult> ls_validation_errors = FunctionsControllerTests_Service
                .ValidateViewModel(controller, function);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Function_Model Default_Form_Function_Model = new Default_Form_Function_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Function_Model(function);
            var result = controller.Edit(Default_Form_Function_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = functionBLO.Validate(function);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

