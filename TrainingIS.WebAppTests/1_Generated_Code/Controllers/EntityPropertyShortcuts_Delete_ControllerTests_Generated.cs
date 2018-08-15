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
    public class EntityPropertyShortcuts_Delete_ControllerTests : ManagerControllerTests
    {
		EntityPropertyShortcutsControllerTests_Service TestService = new EntityPropertyShortcutsControllerTests_Service();

		[TestMethod()]
        public void EntityPropertyShortcuts_Delete_ControllerTests_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = TestService.CreateOrLouadFirstEntityPropertyShortcut(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(entitypropertyshortcut.Id) as ViewResult;
            var EntityPropertyShortcutDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, typeof(Default_Details_EntityPropertyShortcut_Model));
        }

        [TestMethod()]
        public void Delete_EntityPropertyShortcut_Post_Test()
        {
            // Arrange
            //
            // Create EntityPropertyShortcut to Delete
            EntityPropertyShortcut entitypropertyshortcut_to_delete = TestService.CreateValideEntityPropertyShortcutInstance();
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(new UnitOfWork<TrainingISModel>());
            entitypropertyshortcutBLO.Save(entitypropertyshortcut_to_delete);
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte
            var result = controller.DeleteConfirmed(entitypropertyshortcut_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_EntityPropertyShortcut_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

