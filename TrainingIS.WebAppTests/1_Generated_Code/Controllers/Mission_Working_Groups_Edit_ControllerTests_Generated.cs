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
    public class Mission_Working_Groups_Edit_ControllerTests : ManagerControllerTests
    {
		Mission_Working_GroupsControllerTests_Service TestService = new Mission_Working_GroupsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Mission_Working_Group_Not_Exist_Test()
        {
            // Arrange
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Mission_Working_Group_Test()
        {
            // Arrange
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();
            Mission_Working_Group mission_working_group =  TestService.CreateOrLouadFirstMission_Working_Group(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(mission_working_group.Id) as ViewResult;
            var Mission_Working_GroupDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(Mission_Working_GroupDetailModelView, typeof(Default_Form_Mission_Working_Group_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Mission_Working_Group_Post_Test()
        {

            // Arrange
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Mission_Working_Group mission_working_group = TestService.CreateOrLouadFirstMission_Working_Group(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            Mission_Working_GroupsControllerTests_Service.PreBindModel(controller, mission_working_group, nameof(Mission_Working_GroupsController.Edit));
            Mission_Working_GroupsControllerTests_Service.ValidateViewModel(controller, mission_working_group);

			Default_Form_Mission_Working_Group_Model Default_Form_Mission_Working_Group_Model = new Default_Form_Mission_Working_Group_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Mission_Working_Group_Model(mission_working_group);
            var result = controller.Edit(Default_Form_Mission_Working_Group_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Mission_Working_Group_Post_Test()
        {
            // Arrange
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();
            Mission_Working_Group mission_working_group = TestService.CreateInValideMission_Working_GroupInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (mission_working_group == null) return;
            Mission_Working_GroupBLO mission_working_groupBLO = new Mission_Working_GroupBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            Mission_Working_GroupsControllerTests_Service.PreBindModel(controller, mission_working_group, nameof(Mission_Working_GroupsController.Edit));
            List<ValidationResult> ls_validation_errors = Mission_Working_GroupsControllerTests_Service
                .ValidateViewModel(controller, mission_working_group);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Mission_Working_Group_Model Default_Form_Mission_Working_Group_Model = new Default_Form_Mission_Working_Group_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Mission_Working_Group_Model(mission_working_group);
            var result = controller.Edit(Default_Form_Mission_Working_Group_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = mission_working_groupBLO.Validate(mission_working_group);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

