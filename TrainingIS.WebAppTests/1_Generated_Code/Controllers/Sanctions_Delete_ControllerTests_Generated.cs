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
    public class Sanctions_Delete_ControllerTests : ManagerControllerTests
    {
		SanctionsControllerTests_Service TestService = new SanctionsControllerTests_Service();

		[TestMethod()]
        public void Sanctions_Delete_ControllerTests_Test()
        {
            // Arrange
            SanctionsController controller = new SanctionsController();
            Sanction sanction = TestService.CreateOrLouadFirstSanction(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(sanction.Id) as ViewResult;
            var SanctionDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SanctionDetailModelView, typeof(Default_Details_Sanction_Model));
        }

        [TestMethod()]
        public void Delete_Sanction_Post_Test()
        {
            // Arrange
            //
            // Create Sanction to Delete
			            SanctionsController controller = new SanctionsController();
            Sanction sanction_to_delete = TestService.CreateValideSanctionInstance(controller._UnitOfWork,controller.GAppContext);
            SanctionBLO sanctionBLO = new SanctionBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            sanctionBLO.Save(sanction_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(sanction_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Sanction_Test()
        {
            // Arrange
            SanctionsController controller = new SanctionsController();

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

