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
    public class SeancePlannings_Edit_ControllerTests : ManagerControllerTests
    {
		SeancePlanningsControllerTests_Service TestService = new SeancePlanningsControllerTests_Service();

		[TestMethod()]
        public void EditGet_SeancePlanning_Not_Exist_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeancePlanning_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning =  TestService.CreateOrLouadFirstSeancePlanning(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(seanceplanning.Id) as ViewResult;
            var SeancePlanningDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeancePlanningDetailModelView, typeof(Default_Form_SeancePlanning_Model));
        }

        [TestMethod()]
        public void Edit_Valide_SeancePlanning_Post_Test()
        {

            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeancePlanning seanceplanning = TestService.CreateOrLouadFirstSeancePlanning(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Edit));
            SeancePlanningsControllerTests_Service.ValidateViewModel(controller, seanceplanning);

			Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model = new Default_Form_SeancePlanning_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_SeancePlanning_Model(seanceplanning);
            var result = controller.Edit(Default_Form_SeancePlanning_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeancePlanning_Post_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = TestService.CreateInValideSeancePlanningInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (seanceplanning == null) return;
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Edit));
            List<ValidationResult> ls_validation_errors = SeancePlanningsControllerTests_Service
                .ValidateViewModel(controller, seanceplanning);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model = new Default_Form_SeancePlanning_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_SeancePlanning_Model(seanceplanning);
            var result = controller.Edit(Default_Form_SeancePlanning_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seanceplanningBLO.Validate(seanceplanning);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

