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
    public class Delete_SpecialtiesControllerTests : ManagerControllerTests
    {
		SpecialtiesControllerTests_Service TestService = new SpecialtiesControllerTests_Service();

		[TestMethod()]
        public void Delete_Specialty_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Specialty));
			 
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = TestService.CreateOrLouadFirstSpecialty(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(specialty.Id) as ViewResult;
            var SpecialtyDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SpecialtyDetailModelView, typeof(Default_SpecialtyDetailsView));
        }

        [TestMethod()]
        public void Delete_Specialty_Post_Test()
        {
            // Arrange
            //
            // Create Specialty to Delete
            Specialty specialty_to_delete = TestService.CreateValideSpecialtyInstance();
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(new UnitOfWork());
            specialtyBLO.Save(specialty_to_delete);
            SpecialtiesController controller = new SpecialtiesController();

            // Acte
            var result = controller.DeleteConfirmed(specialty_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Specialty_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();

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

