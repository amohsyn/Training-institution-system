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
    public class WorkGroups_Delete_ControllerTests : ManagerControllerTests
    {
		WorkGroupsControllerTests_Service TestService = new WorkGroupsControllerTests_Service();

		[TestMethod()]
        public void WorkGroups_Delete_ControllerTests_Test()
        {
            // Arrange
            WorkGroupsController controller = new WorkGroupsController();
            WorkGroup workgroup = TestService.CreateOrLouadFirstWorkGroup(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(workgroup.Id) as ViewResult;
            var WorkGroupDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(WorkGroupDetailModelView, typeof(Default_Details_WorkGroup_Model));
        }

        [TestMethod()]
        public void Delete_WorkGroup_Post_Test()
        {
            // Arrange
            //
            // Create WorkGroup to Delete
			            WorkGroupsController controller = new WorkGroupsController();
            WorkGroup workgroup_to_delete = TestService.CreateValideWorkGroupInstance(controller._UnitOfWork,controller.GAppContext);
            WorkGroupBLO workgroupBLO = new WorkGroupBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            workgroupBLO.Save(workgroup_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(workgroup_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_WorkGroup_Test()
        {
            // Arrange
            WorkGroupsController controller = new WorkGroupsController();

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

