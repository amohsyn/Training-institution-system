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

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class TrainingsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public TrainingsControllerTests()
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
        /// Find the first Training instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Training CreateOrLouadFirstTraining(UnitOfWork unitOfWork)
        {
            TrainingBLO trainingBLO = new TrainingBLO(unitOfWork);
           
		   Training entity = null;
            if (trainingBLO.FindAll()?.Count > 0)
                entity = trainingBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Training for Test
                entity = this.CreateValideTrainingInstance();
                trainingBLO.Save(entity);
            }
            return entity;
        }

        private Training CreateValideTrainingInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Training  Valide_Training = this._Fixture.Create<Training>();
            Valide_Training.Id = 0;
            // Many to One 
            //
			// Former
			var Former = new FormersControllerTests().CreateOrLouadFirstFormer(unitOfWork);
            Valide_Training.Former = null;
            Valide_Training.FormerId = Former.Id;
			// Group
			var Group = new GroupsControllerTests().CreateOrLouadFirstGroup(unitOfWork);
            Valide_Training.Group = null;
            Valide_Training.GroupId = Group.Id;
			// ModuleTraining
			var ModuleTraining = new ModuleTrainingsControllerTests().CreateOrLouadFirstModuleTraining(unitOfWork);
            Valide_Training.ModuleTraining = null;
            Valide_Training.ModuleTrainingId = ModuleTraining.Id;
			// TrainingYear
			var TrainingYear = new TrainingYearsControllerTests().CreateOrLouadFirstTrainingYear(unitOfWork);
            Valide_Training.TrainingYear = null;
            Valide_Training.TrainingYearId = TrainingYear.Id;
            // One to Many
            //
            return Valide_Training;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Training can't exist</returns>
        private Training CreateInValideTrainingInstance(UnitOfWork unitOfWork = null)
        {
            Training training = this.CreateValideTrainingInstance(unitOfWork);
             
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining(new UnitOfWork());
            
            return training;
        }


		  private Training CreateInValideTrainingInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Training training = this.CreateOrLouadFirstTraining(unitOfWork);
             
			// Required   
 
			training.TrainingYearId = 0;
 
			training.ModuleTrainingId = 0;
 
			training.FormerId = 0;
 
			training.GroupId = 0;
            //Unique
			var existant_Training = this.CreateOrLouadFirstTraining(new UnitOfWork());
            
            return training;
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
            TrainingsController TrainingsController = new TrainingsController();

            //Act
            ViewResult viewResult = TrainingsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            TrainingsController TrainingsController = new TrainingsController();

            ViewResult viewResult = TrainingsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Training_Post_Test()
        {
            //--Arrange--
            TrainingsController controller = new TrainingsController();
            Training training = this.CreateValideTrainingInstance();

            //--Acte--
            //
            TrainingsControllerTests.PreBindModel(controller, training, nameof(TrainingsController.Create));
            TrainingsControllerTests.ValidateViewModel(controller,training);
            var result = controller.Create(training);
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
        public void Create_InValide_Training_Post_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = this.CreateInValideTrainingInstance();
            if (training == null) return;
            TrainingBLO trainingBLO = new TrainingBLO(controller._UnitOfWork);

            // Acte
            TrainingsControllerTests.PreBindModel(controller, training, nameof(TrainingsController.Create));
            List<ValidationResult>  ls_validation_errors = TrainingsControllerTests
                .ValidateViewModel(controller, training);
            var result = controller.Create(training);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingBLO.Validate(training);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


       
        [TestMethod()]
        public void EditGet_Training_Not_Exist_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Training_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Training));
            
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training =  this.CreateOrLouadFirstTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(training.Id) as ViewResult;
            var TrainingDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TrainingDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TrainingDetailModelView, typeof(Training));
        }

        [TestMethod()]
        public void Edit_Valide_Training_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Training));

            // Arrange
            TrainingsController controller = new TrainingsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Training training = this.CreateOrLouadFirstTraining(new UnitOfWork());
			 
       

            // Acte
            TrainingsControllerTests.PreBindModel(controller, training, nameof(TrainingsController.Edit));
            TrainingsControllerTests.ValidateViewModel(controller, training);
            var result = controller.Edit(training);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Training_Post_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = this.CreateInValideTrainingInstance_ForEdit(new UnitOfWork());
            if (training == null) return;
            TrainingBLO trainingBLO = new TrainingBLO(controller._UnitOfWork);

            // Acte
            TrainingsControllerTests.PreBindModel(controller, training, nameof(TrainingsController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingsControllerTests
                .ValidateViewModel(controller, training);
            var result = controller.Edit(training);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingBLO.Validate(training);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

        [TestMethod()]
        public void Delete_Training_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Training));
			 
            // Arrange
            TrainingsController controller = new TrainingsController();
            Training training = this.CreateOrLouadFirstTraining(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(training.Id) as ViewResult;
            var TrainingDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TrainingDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TrainingDetailModelView, typeof(Training));
        }

        [TestMethod()]
        public void Delete_Training_Post_Test()
        {
            // Arrange
            //
            // Create Training to Delete
            Training training_to_delete = this.CreateValideTrainingInstance();
            TrainingBLO trainingBLO = new TrainingBLO(new UnitOfWork());
            trainingBLO.Save(training_to_delete);
            TrainingsController controller = new TrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(training_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Training_Test()
        {
            // Arrange
            TrainingsController controller = new TrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


        //[TestMethod()]
       // public void ExportTest()
        //{
            // Arrange
        //    TrainingsController controller = new TrainingsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    TrainingsController controller = new TrainingsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

