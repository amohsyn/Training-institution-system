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
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.BLL.VO;
using GApp.BLL.Enums;
using TrainingIS.WebApp.Tests.Services;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities.ModelsViews;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class Create_SectorsControllerTests : ManagerControllerTests
    {
		SectorsControllerTests_Service TestService = new SectorsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            SectorsController SectorsController = new SectorsController();

            ViewResult viewResult = SectorsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Sector_Post_Test()
        {
            //--Arrange--
            SectorsController controller = new SectorsController();
            Sector sector = TestService.CreateValideSectorInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            SectorsControllerTests_Service.PreBindModel(controller, sector, nameof(SectorsController.Create));
            SectorsControllerTests_Service.ValidateViewModel(controller,sector);

			Default_Form_Sector_Model Default_Form_Sector_Model = new Default_Form_Sector_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Sector_Model(sector);
            var result = controller.Create(Default_Form_Sector_Model);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_Sector_Post_Test()
        {
            // Arrange
            SectorsController controller = new SectorsController();
            Sector sector = TestService.CreateInValideSectorInstance(controller._UnitOfWork,controller.GAppContext);
            if (sector == null) return;
            SectorBLO sectorBLO = new SectorBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SectorsControllerTests_Service.PreBindModel(controller, sector, nameof(SectorsController.Create));
            List<ValidationResult>  ls_validation_errors = SectorsControllerTests_Service
                .ValidateViewModel(controller, sector);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Sector_Model Default_Form_Sector_Model = new Default_Form_Sector_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Sector_Model(sector);
            var result = controller.Create(Default_Form_Sector_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = sectorBLO.Validate(sector);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

