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
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
	[CleanTestDB]
    public class SeanceTrainings_Edit_ControllerTests : ManagerControllerTests
    {
		SeanceTrainingsControllerTests_Service TestService = new SeanceTrainingsControllerTests_Service();

		[TestMethod()]
        public void EditGet_SeanceTraining_Not_Exist_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceTraining_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining =  TestService.CreateOrLouadFirstSeanceTraining(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(seancetraining.Id) as ViewResult;
            var SeanceTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceTrainingDetailModelView, typeof(Create_SeanceTraining_Model));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceTraining_Post_Test()
        {

            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceTraining seancetraining = TestService.CreateOrLouadFirstSeanceTraining(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            SeanceTrainingsControllerTests_Service.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Edit));
            SeanceTrainingsControllerTests_Service.ValidateViewModel(controller, seancetraining);

			Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Create_SeanceTraining_Model(seancetraining);
            var result = controller.Edit(Create_SeanceTraining_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceTraining_Post_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = TestService.CreateInValideSeanceTrainingInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (seancetraining == null) return;
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SeanceTrainingsControllerTests_Service.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceTrainingsControllerTests_Service
                .ValidateViewModel(controller, seancetraining);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Create_SeanceTraining_Model(seancetraining);
            var result = controller.Edit(Create_SeanceTraining_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancetrainingBLO.Validate(seancetraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

