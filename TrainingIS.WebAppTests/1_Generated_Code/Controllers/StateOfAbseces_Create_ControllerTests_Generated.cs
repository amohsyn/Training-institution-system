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
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_StateOfAbsecesControllerTests : ManagerControllerTests
    {
		StateOfAbsecesControllerTests_Service TestService = new StateOfAbsecesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            StateOfAbsecesController StateOfAbsecesController = new StateOfAbsecesController();

            ViewResult viewResult = StateOfAbsecesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_StateOfAbsece_Post_Test()
        {
            //--Arrange--
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = TestService.CreateValideStateOfAbseceInstance();

            //--Acte--
            //
            StateOfAbsecesControllerTests_Service.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Create));
            StateOfAbsecesControllerTests_Service.ValidateViewModel(controller,stateofabsece);

			Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = new Default_Form_StateOfAbsece_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_StateOfAbsece_Model(stateofabsece);
            var result = controller.Create(Default_Form_StateOfAbsece_Model);
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
        public void Create_InValide_StateOfAbsece_Post_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = TestService.CreateInValideStateOfAbseceInstance();
            if (stateofabsece == null) return;
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(controller._UnitOfWork);

            // Acte
            StateOfAbsecesControllerTests_Service.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Create));
            List<ValidationResult>  ls_validation_errors = StateOfAbsecesControllerTests_Service
                .ValidateViewModel(controller, stateofabsece);

			Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = new Default_Form_StateOfAbsece_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_StateOfAbsece_Model(stateofabsece);
            var result = controller.Create(Default_Form_StateOfAbsece_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = stateofabseceBLO.Validate(stateofabsece);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

