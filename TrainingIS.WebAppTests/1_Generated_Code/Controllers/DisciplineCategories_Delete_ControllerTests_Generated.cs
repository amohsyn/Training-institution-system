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
    public class DisciplineCategories_Delete_ControllerTests : ManagerControllerTests
    {
		DisciplineCategoriesControllerTests_Service TestService = new DisciplineCategoriesControllerTests_Service();

		[TestMethod()]
        public void DisciplineCategories_Delete_ControllerTests_Test()
        {
            // Arrange
            DisciplineCategoriesController controller = new DisciplineCategoriesController();
            DisciplineCategory disciplinecategory = TestService.CreateOrLouadFirstDisciplineCategory(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(disciplinecategory.Id) as ViewResult;
            var DisciplineCategoryDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(DisciplineCategoryDetailModelView, typeof(Default_Details_DisciplineCategory_Model));
        }

        [TestMethod()]
        public void Delete_DisciplineCategory_Post_Test()
        {
            // Arrange
            //
            // Create DisciplineCategory to Delete
			            DisciplineCategoriesController controller = new DisciplineCategoriesController();
            DisciplineCategory disciplinecategory_to_delete = TestService.CreateValideDisciplineCategoryInstance(controller._UnitOfWork,controller.GAppContext);
            DisciplineCategoryBLO disciplinecategoryBLO = new DisciplineCategoryBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            disciplinecategoryBLO.Save(disciplinecategory_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(disciplinecategory_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_DisciplineCategory_Test()
        {
            // Arrange
            DisciplineCategoriesController controller = new DisciplineCategoriesController();

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

