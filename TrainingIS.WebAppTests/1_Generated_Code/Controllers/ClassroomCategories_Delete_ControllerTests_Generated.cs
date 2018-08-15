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
    public class ClassroomCategories_Delete_ControllerTests : ManagerControllerTests
    {
		ClassroomCategoriesControllerTests_Service TestService = new ClassroomCategoriesControllerTests_Service();

		[TestMethod()]
        public void ClassroomCategories_Delete_ControllerTests_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = TestService.CreateOrLouadFirstClassroomCategory(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(classroomcategory.Id) as ViewResult;
            var ClassroomCategoryDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ClassroomCategoryDetailModelView, typeof(Default_Details_ClassroomCategory_Model));
        }

        [TestMethod()]
        public void Delete_ClassroomCategory_Post_Test()
        {
            // Arrange
            //
            // Create ClassroomCategory to Delete
            ClassroomCategory classroomcategory_to_delete = TestService.CreateValideClassroomCategoryInstance();
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(new UnitOfWork<TrainingISModel>());
            classroomcategoryBLO.Save(classroomcategory_to_delete);
            ClassroomCategoriesController controller = new ClassroomCategoriesController();

            // Acte
            var result = controller.DeleteConfirmed(classroomcategory_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ClassroomCategory_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();

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

