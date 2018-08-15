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

using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class ApplicationParams_Edit_ControllerTests : ManagerControllerTests
    {
		ApplicationParamsControllerTests_Service TestService = new ApplicationParamsControllerTests_Service();

		[TestMethod()]
        public void EditGet_ApplicationParam_Not_Exist_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ApplicationParam_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam =  TestService.CreateOrLouadFirstApplicationParam(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(applicationparam.Id) as ViewResult;
            var ApplicationParamDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ApplicationParamDetailModelView, typeof(Default_Form_ApplicationParam_Model));
        }

        [TestMethod()]
        public void Edit_Valide_ApplicationParam_Post_Test()
        {

            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ApplicationParam applicationparam = TestService.CreateOrLouadFirstApplicationParam(new UnitOfWork<TrainingISModel>());
			 
       

            // Acte
            ApplicationParamsControllerTests_Service.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Edit));
            ApplicationParamsControllerTests_Service.ValidateViewModel(controller, applicationparam);

			Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_ApplicationParam_Model(applicationparam);
            var result = controller.Edit(Default_Form_ApplicationParam_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ApplicationParam_Post_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = TestService.CreateInValideApplicationParamInstance_ForEdit(new UnitOfWork<TrainingISModel>());
            if (applicationparam == null) return;
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(controller._UnitOfWork);

            // Acte
            ApplicationParamsControllerTests_Service.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Edit));
            List<ValidationResult> ls_validation_errors = ApplicationParamsControllerTests_Service
                .ValidateViewModel(controller, applicationparam);

			Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_ApplicationParam_Model(applicationparam);
            var result = controller.Edit(Default_Form_ApplicationParam_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = applicationparamBLO.Validate(applicationparam);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

