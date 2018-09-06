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
    public class EntityPropertyShortcuts_Edit_ControllerTests : ManagerControllerTests
    {
		EntityPropertyShortcutsControllerTests_Service TestService = new EntityPropertyShortcutsControllerTests_Service();

		[TestMethod()]
        public void EditGet_EntityPropertyShortcut_Not_Exist_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_EntityPropertyShortcut_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut =  TestService.CreateOrLouadFirstEntityPropertyShortcut(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(entitypropertyshortcut.Id) as ViewResult;
            var EntityPropertyShortcutDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, typeof(Default_Form_EntityPropertyShortcut_Model));
        }

        [TestMethod()]
        public void Edit_Valide_EntityPropertyShortcut_Post_Test()
        {

            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            EntityPropertyShortcut entitypropertyshortcut = TestService.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            EntityPropertyShortcutsControllerTests_Service.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Edit));
            EntityPropertyShortcutsControllerTests_Service.ValidateViewModel(controller, entitypropertyshortcut);

			Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model = new Default_Form_EntityPropertyShortcut_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_EntityPropertyShortcut_Model(entitypropertyshortcut);
            var result = controller.Edit(Default_Form_EntityPropertyShortcut_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_EntityPropertyShortcut_Post_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = TestService.CreateInValideEntityPropertyShortcutInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (entitypropertyshortcut == null) return;
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            EntityPropertyShortcutsControllerTests_Service.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Edit));
            List<ValidationResult> ls_validation_errors = EntityPropertyShortcutsControllerTests_Service
                .ValidateViewModel(controller, entitypropertyshortcut);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model = new Default_Form_EntityPropertyShortcut_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_EntityPropertyShortcut_Model(entitypropertyshortcut);
            var result = controller.Edit(Default_Form_EntityPropertyShortcut_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = entitypropertyshortcutBLO.Validate(entitypropertyshortcut);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

