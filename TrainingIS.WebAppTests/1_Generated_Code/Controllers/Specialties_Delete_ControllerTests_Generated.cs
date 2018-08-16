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
    public class Specialties_Delete_ControllerTests : ManagerControllerTests
    {
		SpecialtiesControllerTests_Service TestService = new SpecialtiesControllerTests_Service();

		[TestMethod()]
        public void Specialties_Delete_ControllerTests_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = TestService.CreateOrLouadFirstSpecialty(controller._UnitOfWork,controller.GAppContext);

            // Acte
            var result = controller.Delete(specialty.Id) as ViewResult;
            var SpecialtyDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SpecialtyDetailModelView, typeof(Default_Details_Specialty_Model));
        }

        [TestMethod()]
        public void Delete_Specialty_Post_Test()
        {
            // Arrange
            //
            // Create Specialty to Delete
			            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty_to_delete = TestService.CreateValideSpecialtyInstance(controller._UnitOfWork,controller.GAppContext);
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            specialtyBLO.Save(specialty_to_delete);


            // Acte
            var result = controller.DeleteConfirmed(specialty_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
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
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        } 
    }
}

