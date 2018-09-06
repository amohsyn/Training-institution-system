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
    public class Create_SeancePlanningsControllerTests : ManagerControllerTests
    {
		SeancePlanningsControllerTests_Service TestService = new SeancePlanningsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            SeancePlanningsController SeancePlanningsController = new SeancePlanningsController();

            ViewResult viewResult = SeancePlanningsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_SeancePlanning_Post_Test()
        {
            //--Arrange--
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = TestService.CreateValideSeancePlanningInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Create));
            SeancePlanningsControllerTests_Service.ValidateViewModel(controller,seanceplanning);

			Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model = new Default_Form_SeancePlanning_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_SeancePlanning_Model(seanceplanning);
            var result = controller.Create(Default_Form_SeancePlanning_Model);
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
        public void Create_InValide_SeancePlanning_Post_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = TestService.CreateInValideSeancePlanningInstance(controller._UnitOfWork,controller.GAppContext);
            if (seanceplanning == null) return;
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SeancePlanningsControllerTests_Service.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Create));
            List<ValidationResult>  ls_validation_errors = SeancePlanningsControllerTests_Service
                .ValidateViewModel(controller, seanceplanning);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_SeancePlanning_Model Default_Form_SeancePlanning_Model = new Default_Form_SeancePlanning_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_SeancePlanning_Model(seanceplanning);
            var result = controller.Create(Default_Form_SeancePlanning_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seanceplanningBLO.Validate(seanceplanning);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

