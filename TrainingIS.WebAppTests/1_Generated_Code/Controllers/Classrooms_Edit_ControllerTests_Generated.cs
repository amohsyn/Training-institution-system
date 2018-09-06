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
    public class Classrooms_Edit_ControllerTests : ManagerControllerTests
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Classroom_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom =  TestService.CreateOrLouadFirstClassroom(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(classroom.Id) as ViewResult;
            var ClassroomDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ClassroomDetailModelView, typeof(Default_Form_Classroom_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Classroom_Post_Test()
        {

            // Arrange
            ClassroomsController controller = new ClassroomsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Classroom classroom = TestService.CreateOrLouadFirstClassroom(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            ClassroomsControllerTests_Service.PreBindModel(controller, classroom, nameof(ClassroomsController.Edit));
            ClassroomsControllerTests_Service.ValidateViewModel(controller, classroom);

			Default_Form_Classroom_Model Default_Form_Classroom_Model = new Default_Form_Classroom_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Classroom_Model(classroom);
            var result = controller.Edit(Default_Form_Classroom_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Classroom_Post_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = TestService.CreateInValideClassroomInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (classroom == null) return;
            ClassroomBLO classroomBLO = new ClassroomBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            ClassroomsControllerTests_Service.PreBindModel(controller, classroom, nameof(ClassroomsController.Edit));
            List<ValidationResult> ls_validation_errors = ClassroomsControllerTests_Service
                .ValidateViewModel(controller, classroom);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Classroom_Model Default_Form_Classroom_Model = new Default_Form_Classroom_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Classroom_Model(classroom);
            var result = controller.Edit(Default_Form_Classroom_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomBLO.Validate(classroom);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

