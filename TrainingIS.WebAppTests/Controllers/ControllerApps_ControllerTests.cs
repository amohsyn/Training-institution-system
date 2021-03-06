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
using TrainingIS.WebApp.Tests.Services;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.BLL.VO;
using GApp.BLL.Enums;
using GApp.UnitTest.DataAnnotations;

namespace TrainingIS.WebApp.Tests.Controllers
{
    [TestClass()]
    [CleanTestDB]
    public class ControllerApps_ControllerTests : ManagerControllerTests
    {
        ControllerAppsControllerTests_Service TestService = new ControllerAppsControllerTests_Service();

        [TestMethod()]
        public void Update_ControllerApps_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            controller.SetFakeControllerContext();

            // Acte
            var result = controller.Update_ControllerApps() as RedirectToRouteResult;

            // Assert 
            //Assert.AreEqual("Index", result.RouteValues["action"]);
            //Assert.IsTrue(controller.Session["notification"] != null);
            //var notification = controller.Session["notification"] as AlertMessage;
            //Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
    }
}
