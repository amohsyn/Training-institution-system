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
    public class TrainingLevels_Edit_ControllerTests : ManagerControllerTests
    {
		TrainingLevelsControllerTests_Service TestService = new TrainingLevelsControllerTests_Service();

		[TestMethod()]
        public void EditGet_TrainingLevel_Not_Exist_Test()
        {
            // Arrange
            TrainingLevelsController controller = new TrainingLevelsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_TrainingLevel_Test()
        {
            // Arrange
            TrainingLevelsController controller = new TrainingLevelsController();
            TrainingLevel traininglevel =  TestService.CreateOrLouadFirstTrainingLevel(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(traininglevel.Id) as ViewResult;
            var TrainingLevelDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TrainingLevelDetailModelView, typeof(Default_Form_TrainingLevel_Model));
        }

        [TestMethod()]
        public void Edit_Valide_TrainingLevel_Post_Test()
        {

            // Arrange
            TrainingLevelsController controller = new TrainingLevelsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            TrainingLevel traininglevel = TestService.CreateOrLouadFirstTrainingLevel(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            TrainingLevelsControllerTests_Service.PreBindModel(controller, traininglevel, nameof(TrainingLevelsController.Edit));
            TrainingLevelsControllerTests_Service.ValidateViewModel(controller, traininglevel);

			Default_Form_TrainingLevel_Model Default_Form_TrainingLevel_Model = new Default_Form_TrainingLevel_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TrainingLevel_Model(traininglevel);
            var result = controller.Edit(Default_Form_TrainingLevel_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_TrainingLevel_Post_Test()
        {
            // Arrange
            TrainingLevelsController controller = new TrainingLevelsController();
            TrainingLevel traininglevel = TestService.CreateInValideTrainingLevelInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (traininglevel == null) return;
            TrainingLevelBLO traininglevelBLO = new TrainingLevelBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            TrainingLevelsControllerTests_Service.PreBindModel(controller, traininglevel, nameof(TrainingLevelsController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingLevelsControllerTests_Service
                .ValidateViewModel(controller, traininglevel);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_TrainingLevel_Model Default_Form_TrainingLevel_Model = new Default_Form_TrainingLevel_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_TrainingLevel_Model(traininglevel);
            var result = controller.Edit(Default_Form_TrainingLevel_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = traininglevelBLO.Validate(traininglevel);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

