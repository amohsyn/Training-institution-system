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
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_ClassroomsControllerTests : ManagerControllerTests
    {
		ClassroomsControllerTests_Service TestService = new ClassroomsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            ClassroomsController ClassroomsController = new ClassroomsController();

            ViewResult viewResult = ClassroomsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Classroom_Post_Test()
        {
            //--Arrange--
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = TestService.CreateValideClassroomInstance();

            //--Acte--
            //
            ClassroomsControllerTests_Service.PreBindModel(controller, classroom, nameof(ClassroomsController.Create));
            ClassroomsControllerTests_Service.ValidateViewModel(controller,classroom);

			Default_Form_Classroom_Model Default_Form_Classroom_Model = new Default_Form_Classroom_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Classroom_Model(classroom);
            var result = controller.Create(Default_Form_Classroom_Model);
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
        public void Create_InValide_Classroom_Post_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = TestService.CreateInValideClassroomInstance();
            if (classroom == null) return;
            ClassroomBLO classroomBLO = new ClassroomBLO(controller._UnitOfWork);

            // Acte
            ClassroomsControllerTests_Service.PreBindModel(controller, classroom, nameof(ClassroomsController.Create));
            List<ValidationResult>  ls_validation_errors = ClassroomsControllerTests_Service
                .ValidateViewModel(controller, classroom);

			Default_Form_Classroom_Model Default_Form_Classroom_Model = new Default_Form_Classroom_ModelBLM(controller._UnitOfWork).ConverTo_Default_Form_Classroom_Model(classroom);
            var result = controller.Create(Default_Form_Classroom_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomBLO.Validate(classroom);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

