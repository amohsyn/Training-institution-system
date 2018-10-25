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
using TrainingIS.Models.WorkGroups;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class WorkGroups_Edit_ControllerTests : ManagerControllerTests
    {
		WorkGroupsControllerTests_Service TestService = new WorkGroupsControllerTests_Service();

		[TestMethod()]
        public void EditGet_WorkGroup_Not_Exist_Test()
        {
            // Arrange
            WorkGroupsController controller = new WorkGroupsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_WorkGroup_Test()
        {
            // Arrange
            WorkGroupsController controller = new WorkGroupsController();
            WorkGroup workgroup =  TestService.CreateOrLouadFirstWorkGroup(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(workgroup.Id) as ViewResult;
            var WorkGroupDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(WorkGroupDetailModelView, typeof(Form_WorkGroup_Model));
        }

        [TestMethod()]
        public void Edit_Valide_WorkGroup_Post_Test()
        {

            // Arrange
            WorkGroupsController controller = new WorkGroupsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            WorkGroup workgroup = TestService.CreateOrLouadFirstWorkGroup(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            WorkGroupsControllerTests_Service.PreBindModel(controller, workgroup, nameof(WorkGroupsController.Edit));
            WorkGroupsControllerTests_Service.ValidateViewModel(controller, workgroup);

			Form_WorkGroup_Model Form_WorkGroup_Model = new Form_WorkGroup_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_WorkGroup_Model(workgroup);
            var result = controller.Edit(Form_WorkGroup_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_WorkGroup_Post_Test()
        {
            // Arrange
            WorkGroupsController controller = new WorkGroupsController();
            WorkGroup workgroup = TestService.CreateInValideWorkGroupInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (workgroup == null) return;
            WorkGroupBLO workgroupBLO = new WorkGroupBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            WorkGroupsControllerTests_Service.PreBindModel(controller, workgroup, nameof(WorkGroupsController.Edit));
            List<ValidationResult> ls_validation_errors = WorkGroupsControllerTests_Service
                .ValidateViewModel(controller, workgroup);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Form_WorkGroup_Model Form_WorkGroup_Model = new Form_WorkGroup_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_WorkGroup_Model(workgroup);
            var result = controller.Edit(Form_WorkGroup_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = workgroupBLO.Validate(workgroup);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

