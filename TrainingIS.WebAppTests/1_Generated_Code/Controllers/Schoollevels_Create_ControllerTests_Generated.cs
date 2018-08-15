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
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_SchoollevelsControllerTests : ManagerControllerTests
    {
		SchoollevelsControllerTests_Service TestService = new SchoollevelsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            SchoollevelsController SchoollevelsController = new SchoollevelsController();

            ViewResult viewResult = SchoollevelsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Schoollevel_Post_Test()
        {
            //--Arrange--
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = TestService.CreateValideSchoollevelInstance();

            //--Acte--
            //
            SchoollevelsControllerTests_Service.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Create));
            SchoollevelsControllerTests_Service.ValidateViewModel(controller,schoollevel);

			Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model = new Default_Form_Schoollevel_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Schoollevel_Model(schoollevel);
            var result = controller.Create(Default_Form_Schoollevel_Model);
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
        public void Create_InValide_Schoollevel_Post_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = TestService.CreateInValideSchoollevelInstance();
            if (schoollevel == null) return;
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(controller._UnitOfWork);

            // Acte
            SchoollevelsControllerTests_Service.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Create));
            List<ValidationResult>  ls_validation_errors = SchoollevelsControllerTests_Service
                .ValidateViewModel(controller, schoollevel);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model = new Default_Form_Schoollevel_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Schoollevel_Model(schoollevel);
            var result = controller.Create(Default_Form_Schoollevel_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = schoollevelBLO.Validate(schoollevel);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

