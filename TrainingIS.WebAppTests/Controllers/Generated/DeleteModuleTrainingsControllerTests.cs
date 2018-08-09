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
    public class Delete_ModuleTrainingsControllerTests : ManagerControllerTests
    {
		ModuleTrainingsControllerTests_Service TestService = new ModuleTrainingsControllerTests_Service();

		[TestMethod()]
        public void Delete_ModuleTraining_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ModuleTraining));
			 
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = TestService.CreateOrLouadFirstModuleTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(moduletraining.Id) as ViewResult;
            var ModuleTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ModuleTrainingDetailModelView, typeof(Default_ModuleTrainingDetailsView));
        }

        [TestMethod()]
        public void Delete_ModuleTraining_Post_Test()
        {
            // Arrange
            //
            // Create ModuleTraining to Delete
            ModuleTraining moduletraining_to_delete = TestService.CreateValideModuleTrainingInstance();
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(new UnitOfWork());
            moduletrainingBLO.Save(moduletraining_to_delete);
            ModuleTrainingsController controller = new ModuleTrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(moduletraining_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ModuleTraining_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();

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

