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
    public class Delete_NationalitiesControllerTests : ManagerControllerTests
    {
		NationalitiesControllerTests_Service TestService = new NationalitiesControllerTests_Service();

		[TestMethod()]
        public void Delete_Nationality_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality = TestService.CreateOrLouadFirstNationality(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(nationality.Id) as ViewResult;
            var NationalityDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(NationalityDetailModelView, typeof(Default_NationalityDetailsView));
        }

        [TestMethod()]
        public void Delete_Nationality_Post_Test()
        {
            // Arrange
            //
            // Create Nationality to Delete
            Nationality nationality_to_delete = TestService.CreateValideNationalityInstance();
            NationalityBLO nationalityBLO = new NationalityBLO(new UnitOfWork());
            nationalityBLO.Save(nationality_to_delete);
            NationalitiesController controller = new NationalitiesController();

            // Acte
            var result = controller.DeleteConfirmed(nationality_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Nationality_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();

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

