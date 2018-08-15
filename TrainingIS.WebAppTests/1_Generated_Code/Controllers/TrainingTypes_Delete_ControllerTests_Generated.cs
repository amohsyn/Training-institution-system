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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{ 
    [TestClass()]
    public class TrainingTypes_Delete_ControllerTests : ManagerControllerTests
    {
		TrainingTypesControllerTests_Service TestService = new TrainingTypesControllerTests_Service();

		[TestMethod()]
        public void TrainingTypes_Delete_ControllerTests_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = TestService.CreateOrLouadFirstTrainingType(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(trainingtype.Id) as ViewResult;
            var TrainingTypeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingTypeDetailModelView, typeof(Default_Details_TrainingType_Model));
        }

        [TestMethod()]
        public void Delete_TrainingType_Post_Test()
        {
            // Arrange
            //
            // Create TrainingType to Delete
            TrainingType trainingtype_to_delete = TestService.CreateValideTrainingTypeInstance();
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(new UnitOfWork<TrainingISModel>());
            trainingtypeBLO.Save(trainingtype_to_delete);
            TrainingTypesController controller = new TrainingTypesController();

            // Acte
            var result = controller.DeleteConfirmed(trainingtype_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_TrainingType_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();

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

