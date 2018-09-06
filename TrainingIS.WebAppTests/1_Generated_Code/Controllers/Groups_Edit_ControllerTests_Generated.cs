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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Groups_Edit_ControllerTests : ManagerControllerTests
    {
		GroupsControllerTests_Service TestService = new GroupsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Group_Not_Exist_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Group_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();
            Group group =  TestService.CreateOrLouadFirstGroup(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(group.Id) as ViewResult;
            var GroupDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(GroupDetailModelView, typeof(EditGroupView));
        }

        [TestMethod()]
        public void Edit_Valide_Group_Post_Test()
        {

            // Arrange
            GroupsController controller = new GroupsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Group group = TestService.CreateOrLouadFirstGroup(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            GroupsControllerTests_Service.PreBindModel(controller, group, nameof(GroupsController.Edit));
            GroupsControllerTests_Service.ValidateViewModel(controller, group);

			EditGroupView EditGroupView = new EditGroupViewBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_EditGroupView(group);
            var result = controller.Edit(EditGroupView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Group_Post_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();
            Group group = TestService.CreateInValideGroupInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (group == null) return;
            GroupBLO groupBLO = new GroupBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            GroupsControllerTests_Service.PreBindModel(controller, group, nameof(GroupsController.Edit));
            List<ValidationResult> ls_validation_errors = GroupsControllerTests_Service
                .ValidateViewModel(controller, group);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			EditGroupView EditGroupView = new EditGroupViewBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_EditGroupView(group);
            var result = controller.Edit(EditGroupView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = groupBLO.Validate(group);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

