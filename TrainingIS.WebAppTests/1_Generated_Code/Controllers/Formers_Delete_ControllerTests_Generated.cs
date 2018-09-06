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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
	[CleanTestDB]
    public class Formers_Delete_ControllerTests : ManagerControllerTests
    {
		FormersControllerTests_Service TestService = new FormersControllerTests_Service();

		[TestMethod()]
        public void Formers_Delete_ControllerTests_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former = TestService.CreateOrLouadFirstFormer(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(former.Id) as ViewResult;
            var FormerDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FormerDetailModelView, typeof(FormerDetailsView));
        }

        [TestMethod()]
        public void Delete_Former_Post_Test()
        {
            // Arrange
            //
            // Create Former to Delete
			            FormersController controller = new FormersController();
            Former former_to_delete = TestService.CreateValideFormerInstance(controller._UnitOfWork,controller.GAppContext);
            FormerBLO formerBLO = new FormerBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            formerBLO.Save(former_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(former_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Former_Test()
        {
            // Arrange
            FormersController controller = new FormersController();

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

