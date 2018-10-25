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
    public class Administrators_Edit_ControllerTests : ManagerControllerTests
    {
		AdministratorsControllerTests_Service TestService = new AdministratorsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Administrator_Not_Exist_Test()
        {
            // Arrange
            AdministratorsController controller = new AdministratorsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Administrator_Test()
        {
            // Arrange
            AdministratorsController controller = new AdministratorsController();
            Administrator administrator =  TestService.CreateOrLouadFirstAdministrator(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(administrator.Id) as ViewResult;
            var AdministratorDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AdministratorDetailModelView, typeof(Default_Form_Administrator_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Administrator_Post_Test()
        {

            // Arrange
            AdministratorsController controller = new AdministratorsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Administrator administrator = TestService.CreateOrLouadFirstAdministrator(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            AdministratorsControllerTests_Service.PreBindModel(controller, administrator, nameof(AdministratorsController.Edit));
            AdministratorsControllerTests_Service.ValidateViewModel(controller, administrator);

			Default_Form_Administrator_Model Default_Form_Administrator_Model = new Default_Form_Administrator_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Administrator_Model(administrator);
            var result = controller.Edit(Default_Form_Administrator_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Administrator_Post_Test()
        {
            // Arrange
            AdministratorsController controller = new AdministratorsController();
            Administrator administrator = TestService.CreateInValideAdministratorInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (administrator == null) return;
            AdministratorBLO administratorBLO = new AdministratorBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            AdministratorsControllerTests_Service.PreBindModel(controller, administrator, nameof(AdministratorsController.Edit));
            List<ValidationResult> ls_validation_errors = AdministratorsControllerTests_Service
                .ValidateViewModel(controller, administrator);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Administrator_Model Default_Form_Administrator_Model = new Default_Form_Administrator_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Administrator_Model(administrator);
            var result = controller.Edit(Default_Form_Administrator_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = administratorBLO.Validate(administrator);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

