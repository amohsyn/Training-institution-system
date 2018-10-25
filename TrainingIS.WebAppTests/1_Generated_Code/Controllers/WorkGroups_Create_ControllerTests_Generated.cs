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
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.BLL.VO;
using GApp.BLL.Enums;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Models.WorkGroups;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Create_WorkGroupsControllerTests : ManagerControllerTests
    {
		WorkGroupsControllerTests_Service TestService = new WorkGroupsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            WorkGroupsController WorkGroupsController = new WorkGroupsController();

            ViewResult viewResult = WorkGroupsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_WorkGroup_Post_Test()
        {
            //--Arrange--
            WorkGroupsController controller = new WorkGroupsController();
            WorkGroup workgroup = TestService.CreateValideWorkGroupInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            WorkGroupsControllerTests_Service.PreBindModel(controller, workgroup, nameof(WorkGroupsController.Create));
            WorkGroupsControllerTests_Service.ValidateViewModel(controller,workgroup);

			Form_WorkGroup_Model Form_WorkGroup_Model = new Form_WorkGroup_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_WorkGroup_Model(workgroup);
            var result = controller.Create(Form_WorkGroup_Model);
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
        public void Create_InValide_WorkGroup_Post_Test()
        {
            // Arrange
            WorkGroupsController controller = new WorkGroupsController();
            WorkGroup workgroup = TestService.CreateInValideWorkGroupInstance(controller._UnitOfWork,controller.GAppContext);
            if (workgroup == null) return;
            WorkGroupBLO workgroupBLO = new WorkGroupBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            WorkGroupsControllerTests_Service.PreBindModel(controller, workgroup, nameof(WorkGroupsController.Create));
            List<ValidationResult>  ls_validation_errors = WorkGroupsControllerTests_Service
                .ValidateViewModel(controller, workgroup);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Form_WorkGroup_Model Form_WorkGroup_Model = new Form_WorkGroup_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Form_WorkGroup_Model(workgroup);
            var result = controller.Create(Form_WorkGroup_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = workgroupBLO.Validate(workgroup);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

