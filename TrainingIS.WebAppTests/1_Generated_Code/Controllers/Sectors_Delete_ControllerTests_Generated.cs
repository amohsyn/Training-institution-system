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
    public class Sectors_Delete_ControllerTests : ManagerControllerTests
    {
		SectorsControllerTests_Service TestService = new SectorsControllerTests_Service();

		[TestMethod()]
        public void Sectors_Delete_ControllerTests_Test()
        {
            // Arrange
            SectorsController controller = new SectorsController();
            Sector sector = TestService.CreateOrLouadFirstSector(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(sector.Id) as ViewResult;
            var SectorDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SectorDetailModelView, typeof(Default_Details_Sector_Model));
        }

        [TestMethod()]
        public void Delete_Sector_Post_Test()
        {
            // Arrange
            //
            // Create Sector to Delete
			            SectorsController controller = new SectorsController();
            Sector sector_to_delete = TestService.CreateValideSectorInstance(controller._UnitOfWork,controller.GAppContext);
            SectorBLO sectorBLO = new SectorBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            sectorBLO.Save(sector_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(sector_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Sector_Test()
        {
            // Arrange
            SectorsController controller = new SectorsController();

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

