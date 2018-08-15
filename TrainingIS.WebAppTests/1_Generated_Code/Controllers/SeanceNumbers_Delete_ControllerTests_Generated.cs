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
    public class SeanceNumbers_Delete_ControllerTests : ManagerControllerTests
    {
		SeanceNumbersControllerTests_Service TestService = new SeanceNumbersControllerTests_Service();

		[TestMethod()]
        public void SeanceNumbers_Delete_ControllerTests_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber = TestService.CreateOrLouadFirstSeanceNumber(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(seancenumber.Id) as ViewResult;
            var SeanceNumberDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceNumberDetailModelView, typeof(Default_Details_SeanceNumber_Model));
        }

        [TestMethod()]
        public void Delete_SeanceNumber_Post_Test()
        {
            // Arrange
            //
            // Create SeanceNumber to Delete
            SeanceNumber seancenumber_to_delete = TestService.CreateValideSeanceNumberInstance();
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(new UnitOfWork<TrainingISModel>());
            seancenumberBLO.Save(seancenumber_to_delete);
            SeanceNumbersController controller = new SeanceNumbersController();

            // Acte
            var result = controller.DeleteConfirmed(seancenumber_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeanceNumber_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();

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

