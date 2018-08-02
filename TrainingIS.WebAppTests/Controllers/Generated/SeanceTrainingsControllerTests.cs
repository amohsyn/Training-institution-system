﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class SeanceTrainingsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SeanceTrainingsControllerTests()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
        #region Initialize
        [TestInitialize]
        public void InitTest()
        {}

		/// <summary>
        /// Find the first SeanceTraining instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public SeanceTraining CreateOrLouadFirstSeanceTraining(UnitOfWork unitOfWork)
        {
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(unitOfWork);
           
		   SeanceTraining entity = null;
            if (seancetrainingBLO.FindAll()?.Count > 0)
                entity = seancetrainingBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp SeanceTraining for Test
                entity = this.CreateValideSeanceTrainingInstance();
                seancetrainingBLO.Save(entity);
            }
            return entity;
        }

        private SeanceTraining CreateValideSeanceTrainingInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            SeanceTraining  Valide_SeanceTraining = this._Fixture.Create<SeanceTraining>();
            Valide_SeanceTraining.Id = 0;
            // Many to One 
            //
			// SeancePlanning
			var SeancePlanning = new SeancePlanningsControllerTests().CreateOrLouadFirstSeancePlanning(unitOfWork);
            Valide_SeanceTraining.SeancePlanning = null;
            Valide_SeanceTraining.SeancePlanningId = SeancePlanning.Id;
            // One to Many
            //
            return Valide_SeanceTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceTraining can't exist</returns>
        private SeanceTraining CreateInValideSeanceTrainingInstance(UnitOfWork unitOfWork = null)
        {
            SeanceTraining seancetraining = this.CreateValideSeanceTrainingInstance(unitOfWork);
             
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining(new UnitOfWork());
            
            return seancetraining;
        }


		  private SeanceTraining CreateInValideSeanceTrainingInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            SeanceTraining seancetraining = this.CreateOrLouadFirstSeanceTraining(unitOfWork);
             
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
			var existant_SeanceTraining = this.CreateOrLouadFirstSeanceTraining(new UnitOfWork());
            
            return seancetraining;
        }


		 
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {} 
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            SeanceTrainingsController SeanceTrainingsController = new SeanceTrainingsController();

            //Act
            ViewResult viewResult = SeanceTrainingsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
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
            SeanceTraining seancetraining = this.CreateValideSeanceTrainingInstance();

            //--Acte--
            //
            SeanceTrainingsControllerTests.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Create));
            SeanceTrainingsControllerTests.ValidateViewModel(controller,seancetraining);

			Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceTrainingFormView(seancetraining);
            var result = controller.Create(Default_SeanceTrainingFormView);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            // [ToDo] Verify Binding Include with GAppDisplayAttribute.BindCreate 

            //--Assert--
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Create_InValide_SeanceTraining_Post_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = this.CreateInValideSeanceTrainingInstance();
            if (seancetraining == null) return;
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(controller._UnitOfWork);

            // Acte
            SeanceTrainingsControllerTests.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Create));
            List<ValidationResult>  ls_validation_errors = SeanceTrainingsControllerTests
                .ValidateViewModel(controller, seancetraining);

			Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceTrainingFormView(seancetraining);
            var result = controller.Create(Default_SeanceTrainingFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancetrainingBLO.Validate(seancetraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


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
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceTraining_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining =  this.CreateOrLouadFirstSeanceTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seancetraining.Id) as ViewResult;
            var SeanceTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceTrainingDetailModelView, typeof(Default_SeanceTrainingFormView));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceTraining_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceTraining));

            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceTraining seancetraining = this.CreateOrLouadFirstSeanceTraining(new UnitOfWork());
			 
       

            // Acte
            SeanceTrainingsControllerTests.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Edit));
            SeanceTrainingsControllerTests.ValidateViewModel(controller, seancetraining);

			Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceTrainingFormView(seancetraining);
            var result = controller.Edit(Default_SeanceTrainingFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceTraining_Post_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = this.CreateInValideSeanceTrainingInstance_ForEdit(new UnitOfWork());
            if (seancetraining == null) return;
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(controller._UnitOfWork);

            // Acte
            SeanceTrainingsControllerTests.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceTrainingsControllerTests
                .ValidateViewModel(controller, seancetraining);

			Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceTrainingFormView(seancetraining);
            var result = controller.Edit(Default_SeanceTrainingFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancetrainingBLO.Validate(seancetraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_SeanceTraining_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceTraining));
			 
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = this.CreateOrLouadFirstSeanceTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(seancetraining.Id) as ViewResult;
            var SeanceTrainingDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceTrainingDetailModelView, typeof(Default_SeanceTrainingDetailsView));
        }

        [TestMethod()]
        public void Delete_SeanceTraining_Post_Test()
        {
            // Arrange
            //
            // Create SeanceTraining to Delete
            SeanceTraining seancetraining_to_delete = this.CreateValideSeanceTrainingInstance();
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(new UnitOfWork());
            seancetrainingBLO.Save(seancetraining_to_delete);
            SeanceTrainingsController controller = new SeanceTrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(seancetraining_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeanceTraining_Test()
        {
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();

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
