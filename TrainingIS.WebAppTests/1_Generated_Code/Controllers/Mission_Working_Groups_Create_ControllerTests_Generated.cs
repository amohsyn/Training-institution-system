﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.BLL.VO;
using GApp.BLL.Enums;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Create_Mission_Working_GroupsControllerTests : ManagerControllerTests
    {
		Mission_Working_GroupsControllerTests_Service TestService = new Mission_Working_GroupsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            Mission_Working_GroupsController Mission_Working_GroupsController = new Mission_Working_GroupsController();

            ViewResult viewResult = Mission_Working_GroupsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Mission_Working_Group_Post_Test()
        {
            //--Arrange--
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();
            Mission_Working_Group mission_working_group = TestService.CreateValideMission_Working_GroupInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            Mission_Working_GroupsControllerTests_Service.PreBindModel(controller, mission_working_group, nameof(Mission_Working_GroupsController.Create));
            Mission_Working_GroupsControllerTests_Service.ValidateViewModel(controller,mission_working_group);

			Default_Form_Mission_Working_Group_Model Default_Form_Mission_Working_Group_Model = new Default_Form_Mission_Working_Group_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Mission_Working_Group_Model(mission_working_group);
            var result = controller.Create(Default_Form_Mission_Working_Group_Model);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_Mission_Working_Group_Post_Test()
        {
            // Arrange
            Mission_Working_GroupsController controller = new Mission_Working_GroupsController();
            Mission_Working_Group mission_working_group = TestService.CreateInValideMission_Working_GroupInstance(controller._UnitOfWork,controller.GAppContext);
            if (mission_working_group == null) return;
            Mission_Working_GroupBLO mission_working_groupBLO = new Mission_Working_GroupBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            Mission_Working_GroupsControllerTests_Service.PreBindModel(controller, mission_working_group, nameof(Mission_Working_GroupsController.Create));
            List<ValidationResult>  ls_validation_errors = Mission_Working_GroupsControllerTests_Service
                .ValidateViewModel(controller, mission_working_group);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Mission_Working_Group_Model Default_Form_Mission_Working_Group_Model = new Default_Form_Mission_Working_Group_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Mission_Working_Group_Model(mission_working_group);
            var result = controller.Create(Default_Form_Mission_Working_Group_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = mission_working_groupBLO.Validate(mission_working_group);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

