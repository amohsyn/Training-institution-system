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
    public class JustificationAbsences_Delete_ControllerTests : ManagerControllerTests
    {
		JustificationAbsencesControllerTests_Service TestService = new JustificationAbsencesControllerTests_Service();

		[TestMethod()]
        public void JustificationAbsences_Delete_ControllerTests_Test()
        {
            // Arrange
            JustificationAbsencesController controller = new JustificationAbsencesController();
            JustificationAbsence justificationabsence = TestService.CreateOrLouadFirstJustificationAbsence(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(justificationabsence.Id) as ViewResult;
            var JustificationAbsenceDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(JustificationAbsenceDetailModelView, typeof(Default_Details_JustificationAbsence_Model));
        }

        [TestMethod()]
        public void Delete_JustificationAbsence_Post_Test()
        {
            // Arrange
            //
            // Create JustificationAbsence to Delete
			            JustificationAbsencesController controller = new JustificationAbsencesController();
            JustificationAbsence justificationabsence_to_delete = TestService.CreateValideJustificationAbsenceInstance(controller._UnitOfWork,controller.GAppContext);
            JustificationAbsenceBLO justificationabsenceBLO = new JustificationAbsenceBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            justificationabsenceBLO.Save(justificationabsence_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(justificationabsence_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_JustificationAbsence_Test()
        {
            // Arrange
            JustificationAbsencesController controller = new JustificationAbsencesController();

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

