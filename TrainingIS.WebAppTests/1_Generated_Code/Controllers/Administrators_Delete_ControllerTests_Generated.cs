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
    public class Administrators_Delete_ControllerTests : ManagerControllerTests
    {
		AdministratorsControllerTests_Service TestService = new AdministratorsControllerTests_Service();

		[TestMethod()]
        public void Administrators_Delete_ControllerTests_Test()
        {
            // Arrange
            AdministratorsController controller = new AdministratorsController();
            Administrator administrator = TestService.CreateOrLouadFirstAdministrator(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(administrator.Id) as ViewResult;
            var AdministratorDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AdministratorDetailModelView, typeof(Default_Details_Administrator_Model));
        }

        [TestMethod()]
        public void Delete_Administrator_Post_Test()
        {
            // Arrange
            //
            // Create Administrator to Delete
			            AdministratorsController controller = new AdministratorsController();
            Administrator administrator_to_delete = TestService.CreateValideAdministratorInstance(controller._UnitOfWork,controller.GAppContext);
            AdministratorBLO administratorBLO = new AdministratorBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            administratorBLO.Save(administrator_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(administrator_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Administrator_Test()
        {
            // Arrange
            AdministratorsController controller = new AdministratorsController();

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

