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
    public class ModuleTrainings_Delete_ControllerTests : ManagerControllerTests
    {
		ModuleTrainingsControllerTests_Service TestService = new ModuleTrainingsControllerTests_Service();

		[TestMethod()]
        public void ModuleTrainings_Delete_ControllerTests_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = TestService.CreateOrLouadFirstModuleTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(moduletraining.Id) as ViewResult;
            var ModuleTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ModuleTrainingDetailModelView, typeof(Default_Details_ModuleTraining_Model));
        }

        [TestMethod()]
        public void Delete_ModuleTraining_Post_Test()
        {
            // Arrange
            //
            // Create ModuleTraining to Delete
            ModuleTraining moduletraining_to_delete = TestService.CreateValideModuleTrainingInstance();
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(new UnitOfWork<TrainingISModel>());
            moduletrainingBLO.Save(moduletraining_to_delete);
            ModuleTrainingsController controller = new ModuleTrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(moduletraining_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

