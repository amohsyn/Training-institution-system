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
    public class Sectors_Edit_ControllerTests : ManagerControllerTests
    {
		SectorsControllerTests_Service TestService = new SectorsControllerTests_Service();

		[TestMethod()]
        public void EditGet_Sector_Not_Exist_Test()
        {
            // Arrange
            SectorsController controller = new SectorsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Sector_Test()
        {
            // Arrange
            SectorsController controller = new SectorsController();
            Sector sector =  TestService.CreateOrLouadFirstSector(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(sector.Id) as ViewResult;
            var SectorDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SectorDetailModelView, typeof(Default_Form_Sector_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Sector_Post_Test()
        {

            // Arrange
            SectorsController controller = new SectorsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Sector sector = TestService.CreateOrLouadFirstSector(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            SectorsControllerTests_Service.PreBindModel(controller, sector, nameof(SectorsController.Edit));
            SectorsControllerTests_Service.ValidateViewModel(controller, sector);

			Default_Form_Sector_Model Default_Form_Sector_Model = new Default_Form_Sector_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Sector_Model(sector);
            var result = controller.Edit(Default_Form_Sector_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Sector_Post_Test()
        {
            // Arrange
            SectorsController controller = new SectorsController();
            Sector sector = TestService.CreateInValideSectorInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (sector == null) return;
            SectorBLO sectorBLO = new SectorBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SectorsControllerTests_Service.PreBindModel(controller, sector, nameof(SectorsController.Edit));
            List<ValidationResult> ls_validation_errors = SectorsControllerTests_Service
                .ValidateViewModel(controller, sector);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Sector_Model Default_Form_Sector_Model = new Default_Form_Sector_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Sector_Model(sector);
            var result = controller.Edit(Default_Form_Sector_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = sectorBLO.Validate(sector);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

