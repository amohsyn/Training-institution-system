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
    public class Classrooms_Delete_ControllerTests : ManagerControllerTests
    {
		ClassroomsControllerTests_Service TestService = new ClassroomsControllerTests_Service();

		[TestMethod()]
        public void Classrooms_Delete_ControllerTests_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = TestService.CreateOrLouadFirstClassroom(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(classroom.Id) as ViewResult;
            var ClassroomDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ClassroomDetailModelView, typeof(Default_Details_Classroom_Model));
        }

        [TestMethod()]
        public void Delete_Classroom_Post_Test()
        {
            // Arrange
            //
            // Create Classroom to Delete
            Classroom classroom_to_delete = TestService.CreateValideClassroomInstance();
            ClassroomBLO classroomBLO = new ClassroomBLO(new UnitOfWork<TrainingISModel>());
            classroomBLO.Save(classroom_to_delete);
            ClassroomsController controller = new ClassroomsController();

            // Acte
            var result = controller.DeleteConfirmed(classroom_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Classroom_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

