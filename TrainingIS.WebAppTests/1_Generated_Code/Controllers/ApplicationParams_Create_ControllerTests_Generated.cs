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
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_ApplicationParamsControllerTests : ManagerControllerTests
    {
		ApplicationParamsControllerTests_Service TestService = new ApplicationParamsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            ApplicationParamsController ApplicationParamsController = new ApplicationParamsController();

            ViewResult viewResult = ApplicationParamsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ApplicationParam_Post_Test()
        {
            //--Arrange--
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = TestService.CreateValideApplicationParamInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            ApplicationParamsControllerTests_Service.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Create));
            ApplicationParamsControllerTests_Service.ValidateViewModel(controller,applicationparam);

			Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_ApplicationParam_Model(applicationparam);
            var result = controller.Create(Default_Form_ApplicationParam_Model);
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
        public void Create_InValide_ApplicationParam_Post_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = TestService.CreateInValideApplicationParamInstance(controller._UnitOfWork,controller.GAppContext);
            if (applicationparam == null) return;
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            ApplicationParamsControllerTests_Service.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Create));
            List<ValidationResult>  ls_validation_errors = ApplicationParamsControllerTests_Service
                .ValidateViewModel(controller, applicationparam);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_ApplicationParam_Model(applicationparam);
            var result = controller.Create(Default_Form_ApplicationParam_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = applicationparamBLO.Validate(applicationparam);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

