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
    public class ModuleTrainingsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private ModuleTraining Valide_ModuleTraining;
        private ModuleTraining Existant_ModuleTraining_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private ModuleTraining ModuleTraining_to_Delete_On_CleanUP = null;

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
            Existant_ModuleTraining_In_DB_Value =  this.CreateOrLouadFirstModuleTraining();
        }

        private ModuleTraining CreateOrLouadFirstModuleTraining()
        {
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(this.TestUnitOfWork);
           
		   ModuleTraining entity = null;
            if (moduletrainingBLO.FindAll()?.Count > 0)
                entity = moduletrainingBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ModuleTraining for Test
                entity = this.CreateValideModuleTrainingInstance();
                moduletrainingBLO.Save(entity);
                ModuleTraining_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private ModuleTraining CreateValideModuleTrainingInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            ModuleTraining  Valide_ModuleTraining = this._Fixture.Create<ModuleTraining>();
            Valide_ModuleTraining.Id = 0;
            // Many to One 
            //

            // Specialty
            var Specialty = new SpecialtyBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_ModuleTraining.Specialty = null;
            Valide_ModuleTraining.SpecialtyId = (Specialty == null) ? 0 : Specialty.Id;
            // One to Many
            //



            return Valide_ModuleTraining;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ModuleTraining can't exist</returns>
        private ModuleTraining CreateInValideModuleTrainingInstance()
        {
            ModuleTraining moduletraining = this.CreateValideModuleTrainingInstance();
             
			// Required   
 
			moduletraining.SpecialtyId = 0;
 
			moduletraining.Name = null;
            //Unique
            
            return moduletraining;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(ModuleTraining_to_Delete_On_CleanUP != null)
            {
                ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(this.TestUnitOfWork);
                moduletrainingBLO.Delete(this.ModuleTraining_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            ModuleTrainingsController ModuleTrainingsController = new ModuleTrainingsController();

            //Act
            ViewResult viewResult = ModuleTrainingsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            ModuleTrainingsController ModuleTrainingsController = new ModuleTrainingsController();

            ViewResult viewResult = ModuleTrainingsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ModuleTraining_Post_Test()
        {
            //--Arrange--
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = this.CreateValideModuleTrainingInstance();

            //--Acte--
            //
            ModuleTrainingsControllerTests.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Create));
            ModuleTrainingsControllerTests.ValidateViewModel(controller,moduletraining);
            var result = controller.Create(moduletraining);
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
        public void Create_InValide_ModuleTraining_Post_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = this.CreateInValideModuleTrainingInstance();
            if (moduletraining == null) return;
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(controller._UnitOfWork);

            // Acte
            ModuleTrainingsControllerTests.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Create));
            List<ValidationResult>  ls_validation_errors = ModuleTrainingsControllerTests
                .ValidateViewModel(controller, moduletraining);
            var result = controller.Create(moduletraining);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = moduletrainingBLO.Validate(moduletraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_ModuleTraining_Not_Exist_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ModuleTraining_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ModuleTraining));
            
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = this.Existant_ModuleTraining_In_DB_Value;

            // Acte
            var result = controller.Edit(moduletraining.Id) as ViewResult;
            var ModuleTrainingDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ModuleTrainingDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ModuleTrainingDetailModelView, typeof(ModuleTraining));
        }

        [TestMethod()]
        public void Edit_Valide_ModuleTraining_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ModuleTraining));

            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
           // controller.SetFakeControllerContext();
            
          
            ModuleTraining moduletraining = this.Existant_ModuleTraining_In_DB_Value;


            // Acte
            ModuleTrainingsControllerTests.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Edit));
            ModuleTrainingsControllerTests.ValidateViewModel(controller, moduletraining);
            var result = controller.Edit(moduletraining);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ModuleTraining_Post_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = this.CreateInValideModuleTrainingInstance();
            if (moduletraining == null) return;
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(controller._UnitOfWork);

            // Acte
            ModuleTrainingsControllerTests.PreBindModel(controller, moduletraining, nameof(ModuleTrainingsController.Create));
            List<ValidationResult> ls_validation_errors = ModuleTrainingsControllerTests
                .ValidateViewModel(controller, moduletraining);
            var result = controller.Edit(moduletraining);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = moduletrainingBLO.Validate(moduletraining);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_ModuleTraining_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ModuleTraining));

            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();
            ModuleTraining moduletraining = this.Existant_ModuleTraining_In_DB_Value;

            // Acte
            var result = controller.Delete(moduletraining.Id) as ViewResult;
            var ModuleTrainingDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ModuleTrainingDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ModuleTrainingDetailModelView, typeof(ModuleTraining));
        }

        [TestMethod()]
        public void Delete_ModuleTraining_Post_Test()
        {
            // Arrange
            //
            // Create ModuleTraining to Delete
            ModuleTraining moduletraining_to_delete = this.CreateValideModuleTrainingInstance();
            ModuleTrainingBLO moduletrainingBLO = new ModuleTrainingBLO(new UnitOfWork());
            moduletrainingBLO.Save(moduletraining_to_delete);
            ModuleTrainingsController controller = new ModuleTrainingsController();

            // Acte
            var result = controller.DeleteConfirmed(moduletraining_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ModuleTraining_Test()
        {
            // Arrange
            ModuleTrainingsController controller = new ModuleTrainingsController();

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
        //    ModuleTrainingsController controller = new ModuleTrainingsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    ModuleTrainingsController controller = new ModuleTrainingsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

