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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Edit_ClassroomsControllerTests : ManagerControllerTests
    {
		ClassroomsControllerTests_Service TestService = new ClassroomsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Classroom_Not_Exist_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Classroom_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom =  TestService.CreateOrLouadFirstClassroom(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(classroom.Id) as ViewResult;
            var ClassroomDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ClassroomDetailModelView, typeof(Default_ClassroomFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Classroom_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Classroom));

            // Arrange
            ClassroomsController controller = new ClassroomsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Classroom classroom = TestService.CreateOrLouadFirstClassroom(new UnitOfWork());
			 
       

            // Acte
            ClassroomsControllerTests_Service.PreBindModel(controller, classroom, nameof(ClassroomsController.Edit));
            ClassroomsControllerTests_Service.ValidateViewModel(controller, classroom);

			Default_ClassroomFormView Default_ClassroomFormView = new Default_ClassroomFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomFormView(classroom);
            var result = controller.Edit(Default_ClassroomFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Classroom_Post_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = TestService.CreateInValideClassroomInstance_ForEdit(new UnitOfWork());
            if (classroom == null) return;
            ClassroomBLO classroomBLO = new ClassroomBLO(controller._UnitOfWork);

            // Acte
            ClassroomsControllerTests_Service.PreBindModel(controller, classroom, nameof(ClassroomsController.Edit));
            List<ValidationResult> ls_validation_errors = ClassroomsControllerTests_Service
                .ValidateViewModel(controller, classroom);

			Default_ClassroomFormView Default_ClassroomFormView = new Default_ClassroomFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomFormView(classroom);
            var result = controller.Edit(Default_ClassroomFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomBLO.Validate(classroom);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

