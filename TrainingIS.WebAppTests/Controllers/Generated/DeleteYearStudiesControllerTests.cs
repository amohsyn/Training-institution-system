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
    public class Delete_YearStudiesControllerTests : ManagerControllerTests
    {
		YearStudiesControllerTests_Service TestService = new YearStudiesControllerTests_Service();

		[TestMethod()]
        public void Delete_YearStudy_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(YearStudy));
			 
            // Arrange
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy = TestService.CreateOrLouadFirstYearStudy(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(yearstudy.Id) as ViewResult;
            var YearStudyDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(YearStudyDetailModelView, typeof(Default_YearStudyDetailsView));
        }

        [TestMethod()]
        public void Delete_YearStudy_Post_Test()
        {
            // Arrange
            //
            // Create YearStudy to Delete
            YearStudy yearstudy_to_delete = TestService.CreateValideYearStudyInstance();
            YearStudyBLO yearstudyBLO = new YearStudyBLO(new UnitOfWork());
            yearstudyBLO.Save(yearstudy_to_delete);
            YearStudiesController controller = new YearStudiesController();

            // Acte
            var result = controller.DeleteConfirmed(yearstudy_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_YearStudy_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        } 
    }
}

