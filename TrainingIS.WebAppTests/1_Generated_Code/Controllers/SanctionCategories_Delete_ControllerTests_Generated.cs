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
    public class SanctionCategories_Delete_ControllerTests : ManagerControllerTests
    {
		SanctionCategoriesControllerTests_Service TestService = new SanctionCategoriesControllerTests_Service();

		[TestMethod()]
        public void SanctionCategories_Delete_ControllerTests_Test()
        {
            // Arrange
            SanctionCategoriesController controller = new SanctionCategoriesController();
            SanctionCategory sanctioncategory = TestService.CreateOrLouadFirstSanctionCategory(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(sanctioncategory.Id) as ViewResult;
            var SanctionCategoryDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SanctionCategoryDetailModelView, typeof(Default_Details_SanctionCategory_Model));
        }

        [TestMethod()]
        public void Delete_SanctionCategory_Post_Test()
        {
            // Arrange
            //
            // Create SanctionCategory to Delete
			            SanctionCategoriesController controller = new SanctionCategoriesController();
            SanctionCategory sanctioncategory_to_delete = TestService.CreateValideSanctionCategoryInstance(controller._UnitOfWork,controller.GAppContext);
            SanctionCategoryBLO sanctioncategoryBLO = new SanctionCategoryBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            sanctioncategoryBLO.Save(sanctioncategory_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(sanctioncategory_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SanctionCategory_Test()
        {
            // Arrange
            SanctionCategoriesController controller = new SanctionCategoriesController();

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

