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
    public class Functions_Delete_ControllerTests : ManagerControllerTests
    {
		FunctionsControllerTests_Service TestService = new FunctionsControllerTests_Service();

		[TestMethod()]
        public void Functions_Delete_ControllerTests_Test()
        {
            // Arrange
            FunctionsController controller = new FunctionsController();
            Function function = TestService.CreateOrLouadFirstFunction(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(function.Id) as ViewResult;
            var FunctionDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FunctionDetailModelView, typeof(Default_Details_Function_Model));
        }

        [TestMethod()]
        public void Delete_Function_Post_Test()
        {
            // Arrange
            //
            // Create Function to Delete
			            FunctionsController controller = new FunctionsController();
            Function function_to_delete = TestService.CreateValideFunctionInstance(controller._UnitOfWork,controller.GAppContext);
            FunctionBLO functionBLO = new FunctionBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            functionBLO.Save(function_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(function_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Function_Test()
        {
            // Arrange
            FunctionsController controller = new FunctionsController();

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

