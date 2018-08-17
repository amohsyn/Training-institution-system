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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
    public class Metiers_Delete_ControllerTests : ManagerControllerTests
    {
		MetiersControllerTests_Service TestService = new MetiersControllerTests_Service();

		[TestMethod()]
        public void Metiers_Delete_ControllerTests_Test()
        {
            // Arrange
            MetiersController controller = new MetiersController();
            Metier metier = TestService.CreateOrLouadFirstMetier(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(metier.Id) as ViewResult;
            var MetierDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(MetierDetailModelView, typeof(Default_Details_Metier_Model));
        }

        [TestMethod()]
        public void Delete_Metier_Post_Test()
        {
            // Arrange
            //
            // Create Metier to Delete
			            MetiersController controller = new MetiersController();
            Metier metier_to_delete = TestService.CreateValideMetierInstance(controller._UnitOfWork,controller.GAppContext);
            MetierBLO metierBLO = new MetierBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            metierBLO.Save(metier_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(metier_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Metier_Test()
        {
            // Arrange
            MetiersController controller = new MetiersController();

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

