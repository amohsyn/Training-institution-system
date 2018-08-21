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
using TrainingIS.Models.Absences;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_AbsencesControllerTests : ManagerControllerTests
    {
		AbsencesControllerTests_Service TestService = new AbsencesControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            AbsencesController AbsencesController = new AbsencesController();

            ViewResult viewResult = AbsencesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Absence_Post_Test()
        {
            //--Arrange--
            AbsencesController controller = new AbsencesController();
            Absence absence = TestService.CreateValideAbsenceInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            AbsencesControllerTests_Service.PreBindModel(controller, absence, nameof(AbsencesController.Create));
            AbsencesControllerTests_Service.ValidateViewModel(controller,absence);

			Create_Absence_Model Create_Absence_Model = new Create_Absence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Create_Absence_Model(absence);
            var result = controller.Create(Create_Absence_Model);
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
        public void Create_InValide_Absence_Post_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence = TestService.CreateInValideAbsenceInstance(controller._UnitOfWork,controller.GAppContext);
            if (absence == null) return;
            AbsenceBLO absenceBLO = new AbsenceBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            AbsencesControllerTests_Service.PreBindModel(controller, absence, nameof(AbsencesController.Create));
            List<ValidationResult>  ls_validation_errors = AbsencesControllerTests_Service
                .ValidateViewModel(controller, absence);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Create_Absence_Model Create_Absence_Model = new Create_Absence_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Create_Absence_Model(absence);
            var result = controller.Create(Create_Absence_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = absenceBLO.Validate(absence);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

