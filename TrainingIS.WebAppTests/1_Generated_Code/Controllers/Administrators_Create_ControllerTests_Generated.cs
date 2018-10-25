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
    public class Create_AdministratorsControllerTests : ManagerControllerTests
    {
		AdministratorsControllerTests_Service TestService = new AdministratorsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            AdministratorsController AdministratorsController = new AdministratorsController();

            ViewResult viewResult = AdministratorsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Administrator_Post_Test()
        {
            //--Arrange--
            AdministratorsController controller = new AdministratorsController();
            Administrator administrator = TestService.CreateValideAdministratorInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            AdministratorsControllerTests_Service.PreBindModel(controller, administrator, nameof(AdministratorsController.Create));
            AdministratorsControllerTests_Service.ValidateViewModel(controller,administrator);

			Default_Form_Administrator_Model Default_Form_Administrator_Model = new Default_Form_Administrator_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Administrator_Model(administrator);
            var result = controller.Create(Default_Form_Administrator_Model);
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
        public void Create_InValide_Administrator_Post_Test()
        {
            // Arrange
            AdministratorsController controller = new AdministratorsController();
            Administrator administrator = TestService.CreateInValideAdministratorInstance(controller._UnitOfWork,controller.GAppContext);
            if (administrator == null) return;
            AdministratorBLO administratorBLO = new AdministratorBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            AdministratorsControllerTests_Service.PreBindModel(controller, administrator, nameof(AdministratorsController.Create));
            List<ValidationResult>  ls_validation_errors = AdministratorsControllerTests_Service
                .ValidateViewModel(controller, administrator);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Administrator_Model Default_Form_Administrator_Model = new Default_Form_Administrator_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Administrator_Model(administrator);
            var result = controller.Create(Default_Form_Administrator_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = administratorBLO.Validate(administrator);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

