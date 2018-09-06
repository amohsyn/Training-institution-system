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
    public class StateOfAbseces_Edit_ControllerTests : ManagerControllerTests
    {
		StateOfAbsecesControllerTests_Service TestService = new StateOfAbsecesControllerTests_Service();

		[TestMethod()]
        public void EditGet_StateOfAbsece_Not_Exist_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_StateOfAbsece_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece =  TestService.CreateOrLouadFirstStateOfAbsece(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(stateofabsece.Id) as ViewResult;
            var StateOfAbseceDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(StateOfAbseceDetailModelView, typeof(Default_Form_StateOfAbsece_Model));
        }

        [TestMethod()]
        public void Edit_Valide_StateOfAbsece_Post_Test()
        {

            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            StateOfAbsece stateofabsece = TestService.CreateOrLouadFirstStateOfAbsece(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            StateOfAbsecesControllerTests_Service.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Edit));
            StateOfAbsecesControllerTests_Service.ValidateViewModel(controller, stateofabsece);

			Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = new Default_Form_StateOfAbsece_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_StateOfAbsece_Model(stateofabsece);
            var result = controller.Edit(Default_Form_StateOfAbsece_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_StateOfAbsece_Post_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = TestService.CreateInValideStateOfAbseceInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (stateofabsece == null) return;
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            StateOfAbsecesControllerTests_Service.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Edit));
            List<ValidationResult> ls_validation_errors = StateOfAbsecesControllerTests_Service
                .ValidateViewModel(controller, stateofabsece);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = new Default_Form_StateOfAbsece_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_StateOfAbsece_Model(stateofabsece);
            var result = controller.Edit(Default_Form_StateOfAbsece_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = stateofabseceBLO.Validate(stateofabsece);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

