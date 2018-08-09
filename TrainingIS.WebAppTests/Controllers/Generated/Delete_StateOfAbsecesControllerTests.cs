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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Delete_StateOfAbsecesControllerTests : ManagerControllerTests
    {
		StateOfAbsecesControllerTests_Service TestService = new StateOfAbsecesControllerTests_Service();

		[TestMethod()]
        public void Delete_StateOfAbsece_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(StateOfAbsece));
			 
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = TestService.CreateOrLouadFirstStateOfAbsece(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(stateofabsece.Id) as ViewResult;
            var StateOfAbseceDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(StateOfAbseceDetailModelView, typeof(Default_StateOfAbseceDetailsView));
        }

        [TestMethod()]
        public void Delete_StateOfAbsece_Post_Test()
        {
            // Arrange
            //
            // Create StateOfAbsece to Delete
            StateOfAbsece stateofabsece_to_delete = TestService.CreateValideStateOfAbseceInstance();
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(new UnitOfWork());
            stateofabseceBLO.Save(stateofabsece_to_delete);
            StateOfAbsecesController controller = new StateOfAbsecesController();

            // Acte
            var result = controller.DeleteConfirmed(stateofabsece_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_StateOfAbsece_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();

            // Acte 
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        } 
    }
}

