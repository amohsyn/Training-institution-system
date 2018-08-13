﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class Delete_TrainingYearsControllerTests : ManagerControllerTests
    {
		TrainingYearsControllerTests_Service TestService = new TrainingYearsControllerTests_Service();

		[TestMethod()]
        public void Delete_TrainingYear_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = TestService.CreateOrLouadFirstTrainingYear(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(trainingyear.Id) as ViewResult;
            var TrainingYearDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingYearDetailModelView, typeof(Default_TrainingYearDetailsView));
        }

        [TestMethod()]
        public void Delete_TrainingYear_Post_Test()
        {
            // Arrange
            //
            // Create TrainingYear to Delete
            TrainingYear trainingyear_to_delete = TestService.CreateValideTrainingYearInstance();
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(new UnitOfWork());
            trainingyearBLO.Save(trainingyear_to_delete);
            TrainingYearsController controller = new TrainingYearsController();

            // Acte
            var result = controller.DeleteConfirmed(trainingyear_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_TrainingYear_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();

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
