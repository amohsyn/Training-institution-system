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
using TrainingIS.Models.SeanceTrainings;


namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Create_SeanceTrainingsControllerTests : ManagerControllerTests
    {
		SeanceTrainingsControllerTests_Service TestService = new SeanceTrainingsControllerTests_Service();

		[TestMethod()]
        public void Create_ViewResult_Test()
        {
            //Arrange
            SeanceTrainingsController SeanceTrainingsController = new SeanceTrainingsController();

            ViewResult viewResult = SeanceTrainingsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_SeanceTraining_Post_Test()
        {
            //--Arrange--
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = TestService.CreateValideSeanceTrainingInstance(controller._UnitOfWork,controller.GAppContext);

            //--Acte--
            //
            SeanceTrainingsControllerTests_Service.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Create));
            SeanceTrainingsControllerTests_Service.ValidateViewModel(controller,seancetraining);

			Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Create_SeanceTraining_Model(seancetraining);
            var result = controller.Create(Create_SeanceTraining_Model);
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
        public void Create_InValide_SeanceTraining_Post_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = TestService.CreateInValideSeanceTrainingInstance(controller._UnitOfWork,controller.GAppContext);
            if (seancetraining == null) return;
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            SeanceTrainingsControllerTests_Service.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Create));
            List<ValidationResult>  ls_validation_errors = SeanceTrainingsControllerTests_Service
                .ValidateViewModel(controller, seancetraining);

			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Create_SeanceTraining_Model(seancetraining);
            var result = controller.Create(Create_SeanceTraining_Model);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancetrainingBLO.Validate(seancetraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

