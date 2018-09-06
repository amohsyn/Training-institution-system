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
    public class Create_LogWorksControllerTests : ManagerControllerTests
    {
		LogWorksControllerTests_Service TestService = new LogWorksControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            LogWorksController LogWorksController = new LogWorksController();

            ViewResult viewResult = LogWorksController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_LogWork_Post_Test()
        {
            //--Arrange--
            LogWorksController controller = new LogWorksController();
            LogWork logwork = TestService.CreateValideLogWorkInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            LogWorksControllerTests_Service.PreBindModel(controller, logwork, nameof(LogWorksController.Create));
            LogWorksControllerTests_Service.ValidateViewModel(controller,logwork);

			Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_LogWork_Model(logwork);
            var result = controller.Create(Default_Form_LogWork_Model);
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
        public void Create_InValide_LogWork_Post_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork = TestService.CreateInValideLogWorkInstance(controller._UnitOfWork,controller.GAppContext);
            if (logwork == null) return;
            LogWorkBLO logworkBLO = new LogWorkBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            LogWorksControllerTests_Service.PreBindModel(controller, logwork, nameof(LogWorksController.Create));
            List<ValidationResult>  ls_validation_errors = LogWorksControllerTests_Service
                .ValidateViewModel(controller, logwork);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_LogWork_Model(logwork);
            var result = controller.Create(Default_Form_LogWork_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = logworkBLO.Validate(logwork);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

