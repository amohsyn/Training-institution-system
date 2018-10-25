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
    public class Mission_Working_Groups_Delete_ControllerTests : ManagerControllerTests
    {
		Mission_Working_GroupsControllerTests_Service TestService = new Mission_Working_GroupsControllerTests_Service();

		[TestMethod()]
        public void Mission_Working_Groups_Delete_ControllerTests_Test()
        {
            // Arrange
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();
            Mission_Working_Group mission_working_group = TestService.CreateOrLouadFirstMission_Working_Group(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(mission_working_group.Id) as ViewResult;
            var Mission_Working_GroupDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(Mission_Working_GroupDetailModelView, typeof(Default_Details_Mission_Working_Group_Model));
        }

        [TestMethod()]
        public void Delete_Mission_Working_Group_Post_Test()
        {
            // Arrange
            //
            // Create Mission_Working_Group to Delete
			            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();
            Mission_Working_Group mission_working_group_to_delete = TestService.CreateValideMission_Working_GroupInstance(controller._UnitOfWork,controller.GAppContext);
            Mission_Working_GroupBLO mission_working_groupBLO = new Mission_Working_GroupBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            mission_working_groupBLO.Save(mission_working_group_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(mission_working_group_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Mission_Working_Group_Test()
        {
            // Arrange
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();

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

