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
    public class Category_WarningTrainees_Delete_ControllerTests : ManagerControllerTests
    {
		Category_WarningTraineesControllerTests_Service TestService = new Category_WarningTraineesControllerTests_Service();

		[TestMethod()]
        public void Category_WarningTrainees_Delete_ControllerTests_Test()
        {
            // Arrange
            Category_WarningTraineesController controller = new Category_WarningTraineesController();
            Category_WarningTrainee category_warningtrainee = TestService.CreateOrLouadFirstCategory_WarningTrainee(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(category_warningtrainee.Id) as ViewResult;
            var Category_WarningTraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(Category_WarningTraineeDetailModelView, typeof(Default_Details_Category_WarningTrainee_Model));
        }

        [TestMethod()]
        public void Delete_Category_WarningTrainee_Post_Test()
        {
            // Arrange
            //
            // Create Category_WarningTrainee to Delete
			            Category_WarningTraineesController controller = new Category_WarningTraineesController();
            Category_WarningTrainee category_warningtrainee_to_delete = TestService.CreateValideCategory_WarningTraineeInstance(controller._UnitOfWork,controller.GAppContext);
            Category_WarningTraineeBLO category_warningtraineeBLO = new Category_WarningTraineeBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            category_warningtraineeBLO.Save(category_warningtrainee_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(category_warningtrainee_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Category_WarningTrainee_Test()
        {
            // Arrange
            Category_WarningTraineesController controller = new Category_WarningTraineesController();

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

