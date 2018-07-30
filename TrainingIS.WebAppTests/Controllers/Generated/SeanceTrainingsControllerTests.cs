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
    public class SeanceTrainingsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private SeanceTraining Valide_SeanceTraining;
        private SeanceTraining Existant_SeanceTraining_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private SeanceTraining SeanceTraining_to_Delete_On_CleanUP = null;

        #region Initialize
        [TestInitialize]
        public void InitTest()
        {
            // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            TestUnitOfWork = new UnitOfWork();
            Existant_SeanceTraining_In_DB_Value =  this.CreateOrLouadFirstSeanceTraining();
        }

        private SeanceTraining CreateOrLouadFirstSeanceTraining()
        {
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(this.TestUnitOfWork);
            SeanceTraining entity = seancetrainingBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp SeanceTraining for Test
                entity = this.CreateValideSeanceTrainingInstance();
                seancetrainingBLO.Save(entity);
                SeanceTraining_to_Delete_On_CleanUP = entity;
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
            var SeancePlanning = new SeancePlanningBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_SeanceTraining.SeancePlanning = null;
            Valide_SeanceTraining.SeancePlanningId = (SeancePlanning == null) ? 0 : SeancePlanning.Id;
            // One to Many
            //



            return Valide_SeanceTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceTraining can't exist</returns>
        private SeanceTraining CreateInValideSeanceTrainingInstance()
        {
            SeanceTraining seancetraining = this.CreateValideSeanceTrainingInstance();
             
			// Required   
 
			seancetraining.SeanceDate = null;
 
			seancetraining.SeancePlanningId = 0;
            //Unique
            
            return seancetraining;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(SeanceTraining_to_Delete_On_CleanUP != null)
            {
                SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(this.TestUnitOfWork);
                seancetrainingBLO.Delete(this.SeanceTraining_to_Delete_On_CleanUP);
            }

        }
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
            var result = controller.Create(seancetraining);
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
            var result = controller.Create(seancetraining);
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
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceTraining));
            
            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
            SeanceTraining seancetraining = this.Existant_SeanceTraining_In_DB_Value;

            // Acte
            var result = controller.Edit(seancetraining.Id) as ViewResult;
            var SeanceTrainingDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeanceTrainingDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeanceTrainingDetailModelView, typeof(SeanceTraining));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceTraining_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceTraining));

            // Arrange
            SeanceTrainingsController controller = new SeanceTrainingsController();
           // controller.SetFakeControllerContext();
            
          
            SeanceTraining seancetraining = this.Existant_SeanceTraining_In_DB_Value;


            // Acte
            SeanceTrainingsControllerTests.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Edit));
            SeanceTrainingsControllerTests.ValidateViewModel(controller, seancetraining);
            var result = controller.Edit(seancetraining);
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
            SeanceTraining seancetraining = this.CreateInValideSeanceTrainingInstance();
            if (seancetraining == null) return;
            SeanceTrainingBLO seancetrainingBLO = new SeanceTrainingBLO(controller._UnitOfWork);

            // Acte
            SeanceTrainingsControllerTests.PreBindModel(controller, seancetraining, nameof(SeanceTrainingsController.Create));
            List<ValidationResult> ls_validation_errors = SeanceTrainingsControllerTests
                .ValidateViewModel(controller, seancetraining);
            var result = controller.Edit(seancetraining);
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
            SeanceTraining seancetraining = this.Existant_SeanceTraining_In_DB_Value;

            // Acte
            var result = controller.Delete(seancetraining.Id) as ViewResult;
            var SeanceTrainingDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeanceTrainingDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeanceTrainingDetailModelView, typeof(SeanceTraining));
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


        //[TestMethod()]
       // public void ExportTest()
        //{
            // Arrange
        //    SeanceTrainingsController controller = new SeanceTrainingsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    SeanceTrainingsController controller = new SeanceTrainingsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

