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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Delete_GroupsControllerTests : ManagerControllerTests
    {
		GroupsControllerTests_Service TestService = new GroupsControllerTests_Service();

		[TestMethod()]
        public void Delete_Group_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Group));
			 
            // Arrange
            GroupsController controller = new GroupsController();
            Group group = TestService.CreateOrLouadFirstGroup(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(group.Id) as ViewResult;
            var GroupDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(GroupDetailModelView, typeof(DetailsGroupView));
        }

        [TestMethod()]
        public void Delete_Group_Post_Test()
        {
            // Arrange
            //
            // Create Group to Delete
            Group group_to_delete = TestService.CreateValideGroupInstance();
            GroupBLO groupBLO = new GroupBLO(new UnitOfWork());
            groupBLO.Save(group_to_delete);
            GroupsController controller = new GroupsController();

            // Acte
            var result = controller.DeleteConfirmed(group_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Group_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();

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

