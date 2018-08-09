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
    public class Create_GroupsControllerTests : ManagerControllerTests
    {
		GroupsControllerTests_Service TestService = new GroupsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            GroupsController GroupsController = new GroupsController();

            ViewResult viewResult = GroupsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Group_Post_Test()
        {
            //--Arrange--
            GroupsController controller = new GroupsController();
            Group group = TestService.CreateValideGroupInstance();

            //--Acte--
            //
            GroupsControllerTests_Service.PreBindModel(controller, group, nameof(GroupsController.Create));
            GroupsControllerTests_Service.ValidateViewModel(controller,group);

			CreateGroupView CreateGroupView = new CreateGroupViewBLM(controller._UnitOfWork).ConverTo_CreateGroupView(group);
            var result = controller.Create(CreateGroupView);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_Group_Post_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();
            Group group = TestService.CreateInValideGroupInstance();
            if (group == null) return;
            GroupBLO groupBLO = new GroupBLO(controller._UnitOfWork);

            // Acte
            GroupsControllerTests_Service.PreBindModel(controller, group, nameof(GroupsController.Create));
            List<ValidationResult>  ls_validation_errors = GroupsControllerTests_Service
                .ValidateViewModel(controller, group);

			CreateGroupView CreateGroupView = new CreateGroupViewBLM(controller._UnitOfWork).ConverTo_CreateGroupView(group);
            var result = controller.Create(CreateGroupView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = groupBLO.Validate(group);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

