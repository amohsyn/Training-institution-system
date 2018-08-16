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
    public class ApplicationParams_Delete_ControllerTests : ManagerControllerTests
    {
		ApplicationParamsControllerTests_Service TestService = new ApplicationParamsControllerTests_Service();

		[TestMethod()]
        public void ApplicationParams_Delete_ControllerTests_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = TestService.CreateOrLouadFirstApplicationParam(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(applicationparam.Id) as ViewResult;
            var ApplicationParamDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ApplicationParamDetailModelView, typeof(Default_Details_ApplicationParam_Model));
        }

        [TestMethod()]
        public void Delete_ApplicationParam_Post_Test()
        {
            // Arrange
            //
            // Create ApplicationParam to Delete
			            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam_to_delete = TestService.CreateValideApplicationParamInstance(controller._UnitOfWork,controller.GAppContext);
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            applicationparamBLO.Save(applicationparam_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(applicationparam_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ApplicationParam_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();

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

