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
    public class FormerSpecialties_Delete_ControllerTests : ManagerControllerTests
    {
		FormerSpecialtiesControllerTests_Service TestService = new FormerSpecialtiesControllerTests_Service();

		[TestMethod()]
        public void FormerSpecialties_Delete_ControllerTests_Test()
        {
            // Arrange
            FormerSpecialtiesController controller = new FormerSpecialtiesController();
            FormerSpecialty formerspecialty = TestService.CreateOrLouadFirstFormerSpecialty(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(formerspecialty.Id) as ViewResult;
            var FormerSpecialtyDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FormerSpecialtyDetailModelView, typeof(Default_Details_FormerSpecialty_Model));
        }

        [TestMethod()]
        public void Delete_FormerSpecialty_Post_Test()
        {
            // Arrange
            //
            // Create FormerSpecialty to Delete
			            FormerSpecialtiesController controller = new FormerSpecialtiesController();
            FormerSpecialty formerspecialty_to_delete = TestService.CreateValideFormerSpecialtyInstance(controller._UnitOfWork,controller.GAppContext);
            FormerSpecialtyBLO formerspecialtyBLO = new FormerSpecialtyBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            formerspecialtyBLO.Save(formerspecialty_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(formerspecialty_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_FormerSpecialty_Test()
        {
            // Arrange
            FormerSpecialtiesController controller = new FormerSpecialtiesController();

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

