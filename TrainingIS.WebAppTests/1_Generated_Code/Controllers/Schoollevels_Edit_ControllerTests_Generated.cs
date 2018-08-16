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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Schoollevels_Edit_ControllerTests : ManagerControllerTests
    {
		SchoollevelsControllerTests_Service TestService = new SchoollevelsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Schoollevel_Not_Exist_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Schoollevel_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel =  TestService.CreateOrLouadFirstSchoollevel(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(schoollevel.Id) as ViewResult;
            var SchoollevelDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SchoollevelDetailModelView, typeof(Default_Form_Schoollevel_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Schoollevel_Post_Test()
        {

            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Schoollevel schoollevel = TestService.CreateOrLouadFirstSchoollevel(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            SchoollevelsControllerTests_Service.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Edit));
            SchoollevelsControllerTests_Service.ValidateViewModel(controller, schoollevel);

			Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model = new Default_Form_Schoollevel_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Schoollevel_Model(schoollevel);
            var result = controller.Edit(Default_Form_Schoollevel_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Schoollevel_Post_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = TestService.CreateInValideSchoollevelInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (schoollevel == null) return;
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SchoollevelsControllerTests_Service.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Edit));
            List<ValidationResult> ls_validation_errors = SchoollevelsControllerTests_Service
                .ValidateViewModel(controller, schoollevel);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Schoollevel_Model Default_Form_Schoollevel_Model = new Default_Form_Schoollevel_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Schoollevel_Model(schoollevel);
            var result = controller.Edit(Default_Form_Schoollevel_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = schoollevelBLO.Validate(schoollevel);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

