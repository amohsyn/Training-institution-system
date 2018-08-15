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
    public class Absences_Delete_ControllerTests : ManagerControllerTests
    {
		AbsencesControllerTests_Service TestService = new AbsencesControllerTests_Service();

		[TestMethod()]
        public void Absences_Delete_ControllerTests_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence = TestService.CreateOrLouadFirstAbsence(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(absence.Id) as ViewResult;
            var AbsenceDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AbsenceDetailModelView, typeof(Default_Details_Absence_Model));
        }

        [TestMethod()]
        public void Delete_Absence_Post_Test()
        {
            // Arrange
            //
            // Create Absence to Delete
            Absence absence_to_delete = TestService.CreateValideAbsenceInstance();
            AbsenceBLO absenceBLO = new AbsenceBLO(new UnitOfWork<TrainingISModel>());
            absenceBLO.Save(absence_to_delete);
            AbsencesController controller = new AbsencesController();

            // Acte
            var result = controller.DeleteConfirmed(absence_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Absence_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();

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

