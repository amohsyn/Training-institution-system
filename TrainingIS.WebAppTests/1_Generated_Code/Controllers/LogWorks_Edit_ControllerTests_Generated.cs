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
    public class LogWorks_Edit_ControllerTests : ManagerControllerTests
    {
		LogWorksControllerTests_Service TestService = new LogWorksControllerTests_Service();

		[TestMethod()]
        public void EditGet_LogWork_Not_Exist_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_LogWork_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork =  TestService.CreateOrLouadFirstLogWork(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(logwork.Id) as ViewResult;
            var LogWorkDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(LogWorkDetailModelView, typeof(Default_Form_LogWork_Model));
        }

        [TestMethod()]
        public void Edit_Valide_LogWork_Post_Test()
        {

            // Arrange
            LogWorksController controller = new LogWorksController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            LogWork logwork = TestService.CreateOrLouadFirstLogWork(new UnitOfWork<TrainingISModel>());
			 
       

            // Acte
            LogWorksControllerTests_Service.PreBindModel(controller, logwork, nameof(LogWorksController.Edit));
            LogWorksControllerTests_Service.ValidateViewModel(controller, logwork);

			Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_LogWork_Model(logwork);
            var result = controller.Edit(Default_Form_LogWork_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_LogWork_Post_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork = TestService.CreateInValideLogWorkInstance_ForEdit(new UnitOfWork<TrainingISModel>());
            if (logwork == null) return;
            LogWorkBLO logworkBLO = new LogWorkBLO(controller._UnitOfWork);

            // Acte
            LogWorksControllerTests_Service.PreBindModel(controller, logwork, nameof(LogWorksController.Edit));
            List<ValidationResult> ls_validation_errors = LogWorksControllerTests_Service
                .ValidateViewModel(controller, logwork);

			Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_LogWork_Model(logwork);
            var result = controller.Edit(Default_Form_LogWork_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = logworkBLO.Validate(logwork);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

