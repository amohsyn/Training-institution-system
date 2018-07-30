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
    public class StateOfAbsecesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private StateOfAbsece Valide_StateOfAbsece;
        private StateOfAbsece Existant_StateOfAbsece_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private StateOfAbsece StateOfAbsece_to_Delete_On_CleanUP = null;

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
            Existant_StateOfAbsece_In_DB_Value =  this.CreateOrLouadFirstStateOfAbsece();
        }

        private StateOfAbsece CreateOrLouadFirstStateOfAbsece()
        {
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(this.TestUnitOfWork);
            StateOfAbsece entity = stateofabseceBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp StateOfAbsece for Test
                entity = this.CreateValideStateOfAbseceInstance();
                stateofabseceBLO.Save(entity);
                StateOfAbsece_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private StateOfAbsece CreateValideStateOfAbseceInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            StateOfAbsece  Valide_StateOfAbsece = this._Fixture.Create<StateOfAbsece>();
            Valide_StateOfAbsece.Id = 0;
            // Many to One 
            //

            // Trainee
            var Trainee = new TraineeBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_StateOfAbsece.Trainee = null;
            Valide_StateOfAbsece.TraineeId = (Trainee == null) ? 0 : Trainee.Id;
            // One to Many
            //



            return Valide_StateOfAbsece;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide StateOfAbsece can't exist</returns>
        private StateOfAbsece CreateInValideStateOfAbseceInstance()
        {
            StateOfAbsece stateofabsece = this.CreateValideStateOfAbseceInstance();
             
			// Required   
 
			stateofabsece.Name = null;
 
			stateofabsece.Category = StateOfAbseceCategories.Year;
 
			stateofabsece.Value = 0;
 
			stateofabsece.TraineeId = 0;
            //Unique
            
            return stateofabsece;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(StateOfAbsece_to_Delete_On_CleanUP != null)
            {
                StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(this.TestUnitOfWork);
                stateofabseceBLO.Delete(this.StateOfAbsece_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            StateOfAbsecesController StateOfAbsecesController = new StateOfAbsecesController();

            //Act
            ViewResult viewResult = StateOfAbsecesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            StateOfAbsecesController StateOfAbsecesController = new StateOfAbsecesController();

            ViewResult viewResult = StateOfAbsecesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_StateOfAbsece_Post_Test()
        {
            //--Arrange--
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = this.CreateValideStateOfAbseceInstance();

            //--Acte--
            //
            StateOfAbsecesControllerTests.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Create));
            StateOfAbsecesControllerTests.ValidateViewModel(controller,stateofabsece);
            var result = controller.Create(stateofabsece);
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
        public void Create_InValide_StateOfAbsece_Post_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = this.CreateInValideStateOfAbseceInstance();
            if (stateofabsece == null) return;
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(controller._UnitOfWork);

            // Acte
            StateOfAbsecesControllerTests.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Create));
            List<ValidationResult>  ls_validation_errors = StateOfAbsecesControllerTests
                .ValidateViewModel(controller, stateofabsece);
            var result = controller.Create(stateofabsece);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = stateofabseceBLO.Validate(stateofabsece);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_StateOfAbsece_Not_Exist_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_StateOfAbsece_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(StateOfAbsece));
            
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = this.Existant_StateOfAbsece_In_DB_Value;

            // Acte
            var result = controller.Edit(stateofabsece.Id) as ViewResult;
            var StateOfAbseceDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(StateOfAbseceDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(StateOfAbseceDetailModelView, typeof(StateOfAbsece));
        }

        [TestMethod()]
        public void Edit_Valide_StateOfAbsece_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(StateOfAbsece));

            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
           // controller.SetFakeControllerContext();
            
          
            StateOfAbsece stateofabsece = this.Existant_StateOfAbsece_In_DB_Value;


            // Acte
            StateOfAbsecesControllerTests.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Edit));
            StateOfAbsecesControllerTests.ValidateViewModel(controller, stateofabsece);
            var result = controller.Edit(stateofabsece);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_StateOfAbsece_Post_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = this.CreateInValideStateOfAbseceInstance();
            if (stateofabsece == null) return;
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(controller._UnitOfWork);

            // Acte
            StateOfAbsecesControllerTests.PreBindModel(controller, stateofabsece, nameof(StateOfAbsecesController.Create));
            List<ValidationResult> ls_validation_errors = StateOfAbsecesControllerTests
                .ValidateViewModel(controller, stateofabsece);
            var result = controller.Edit(stateofabsece);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = stateofabseceBLO.Validate(stateofabsece);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_StateOfAbsece_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(StateOfAbsece));

            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();
            StateOfAbsece stateofabsece = this.Existant_StateOfAbsece_In_DB_Value;

            // Acte
            var result = controller.Delete(stateofabsece.Id) as ViewResult;
            var StateOfAbseceDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(StateOfAbseceDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(StateOfAbseceDetailModelView, typeof(StateOfAbsece));
        }

        [TestMethod()]
        public void Delete_StateOfAbsece_Post_Test()
        {
            // Arrange
            //
            // Create StateOfAbsece to Delete
            StateOfAbsece stateofabsece_to_delete = this.CreateValideStateOfAbseceInstance();
            StateOfAbseceBLO stateofabseceBLO = new StateOfAbseceBLO(new UnitOfWork());
            stateofabseceBLO.Save(stateofabsece_to_delete);
            StateOfAbsecesController controller = new StateOfAbsecesController();

            // Acte
            var result = controller.DeleteConfirmed(stateofabsece_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_StateOfAbsece_Test()
        {
            // Arrange
            StateOfAbsecesController controller = new StateOfAbsecesController();

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
        //    StateOfAbsecesController controller = new StateOfAbsecesController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    StateOfAbsecesController controller = new StateOfAbsecesController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

