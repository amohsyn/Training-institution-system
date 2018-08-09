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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Delete_EntityPropertyShortcutsControllerTests : ManagerControllerTests
    {
		EntityPropertyShortcutsControllerTests_Service TestService = new EntityPropertyShortcutsControllerTests_Service();

		[TestMethod()]
        public void Delete_EntityPropertyShortcut_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(EntityPropertyShortcut));
			 
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = TestService.CreateOrLouadFirstEntityPropertyShortcut(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(entitypropertyshortcut.Id) as ViewResult;
            var EntityPropertyShortcutDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, typeof(Default_EntityPropertyShortcutDetailsView));
        }

        [TestMethod()]
        public void Delete_EntityPropertyShortcut_Post_Test()
        {
            // Arrange
            //
            // Create EntityPropertyShortcut to Delete
            EntityPropertyShortcut entitypropertyshortcut_to_delete = TestService.CreateValideEntityPropertyShortcutInstance();
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(new UnitOfWork());
            entitypropertyshortcutBLO.Save(entitypropertyshortcut_to_delete);
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte
            var result = controller.DeleteConfirmed(entitypropertyshortcut_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
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
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        } 
    }
}

