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
    public class AppControllersControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private AppController Valide_AppController;
        private AppController Existant_AppController_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private AppController AppController_to_Delete_On_CleanUP = null;

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
            Existant_AppController_In_DB_Value =  this.CreateOrLouadFirstAppController();
        }

        private AppController CreateOrLouadFirstAppController()
        {
            AppControllerBLO appcontrollerBLO = new AppControllerBLO(this.TestUnitOfWork);
            AppController entity = appcontrollerBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp AppController for Test
                entity = this.CreateValideAppControllerInstance();
                appcontrollerBLO.Save(entity);
                AppController_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private AppController CreateValideAppControllerInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            AppController  Valide_AppController = this._Fixture.Create<AppController>();
            Valide_AppController.Id = 0;
            // Many to One 
            //

            // One to Many
            //
             Valide_AppController.AppRoles = null;



            return Valide_AppController;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide AppController can't exist</returns>
        private AppController CreateInValideAppControllerInstance()
        {
            AppController appcontroller = this.CreateValideAppControllerInstance();
             
			// Required   
 
			appcontroller.Code = null;
            //Unique
            
            return appcontroller;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(AppController_to_Delete_On_CleanUP != null)
            {
                AppControllerBLO appcontrollerBLO = new AppControllerBLO(this.TestUnitOfWork);
                appcontrollerBLO.Delete(this.AppController_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            AppControllersController AppControllersController = new AppControllersController();

            //Act
            ViewResult viewResult = AppControllersController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            AppControllersController AppControllersController = new AppControllersController();

            ViewResult viewResult = AppControllersController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_AppController_Post_Test()
        {
            //--Arrange--
            AppControllersController controller = new AppControllersController();
            AppController appcontroller = this.CreateValideAppControllerInstance();

            //--Acte--
            //
            AppControllersControllerTests.PreBindModel(controller, appcontroller, nameof(AppControllersController.Create));
            AppControllersControllerTests.ValidateViewModel(controller,appcontroller);
            var result = controller.Create(appcontroller);
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
        public void Create_InValide_AppController_Post_Test()
        {
            // Arrange
            AppControllersController controller = new AppControllersController();
            AppController appcontroller = this.CreateInValideAppControllerInstance();
            if (appcontroller == null) return;
            AppControllerBLO appcontrollerBLO = new AppControllerBLO(controller._UnitOfWork);

            // Acte
            AppControllersControllerTests.PreBindModel(controller, appcontroller, nameof(AppControllersController.Create));
            List<ValidationResult>  ls_validation_errors = AppControllersControllerTests
                .ValidateViewModel(controller, appcontroller);
            var result = controller.Create(appcontroller);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = appcontrollerBLO.Validate(appcontroller);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_AppController_Not_Exist_Test()
        {
            // Arrange
            AppControllersController controller = new AppControllersController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_AppController_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppController));
            
            // Arrange
            AppControllersController controller = new AppControllersController();
            AppController appcontroller = this.Existant_AppController_In_DB_Value;

            // Acte
            var result = controller.Edit(appcontroller.Id) as ViewResult;
            var AppControllerDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AppControllerDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AppControllerDetailModelView, typeof(AppController));
        }

        [TestMethod()]
        public void Edit_Valide_AppController_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppController));

            // Arrange
            AppControllersController controller = new AppControllersController();
           // controller.SetFakeControllerContext();
            
          
            AppController appcontroller = this.Existant_AppController_In_DB_Value;


            // Acte
            AppControllersControllerTests.PreBindModel(controller, appcontroller, nameof(AppControllersController.Edit));
            AppControllersControllerTests.ValidateViewModel(controller, appcontroller);
            var result = controller.Edit(appcontroller);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_AppController_Post_Test()
        {
            // Arrange
            AppControllersController controller = new AppControllersController();
            AppController appcontroller = this.CreateInValideAppControllerInstance();
            if (appcontroller == null) return;
            AppControllerBLO appcontrollerBLO = new AppControllerBLO(controller._UnitOfWork);

            // Acte
            AppControllersControllerTests.PreBindModel(controller, appcontroller, nameof(AppControllersController.Create));
            List<ValidationResult> ls_validation_errors = AppControllersControllerTests
                .ValidateViewModel(controller, appcontroller);
            var result = controller.Edit(appcontroller);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = appcontrollerBLO.Validate(appcontroller);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_AppController_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppController));

            // Arrange
            AppControllersController controller = new AppControllersController();
            AppController appcontroller = this.Existant_AppController_In_DB_Value;

            // Acte
            var result = controller.Delete(appcontroller.Id) as ViewResult;
            var AppControllerDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AppControllerDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AppControllerDetailModelView, typeof(AppController));
        }

        [TestMethod()]
        public void Delete_AppController_Post_Test()
        {
            // Arrange
            //
            // Create AppController to Delete
            AppController appcontroller_to_delete = this.CreateValideAppControllerInstance();
            AppControllerBLO appcontrollerBLO = new AppControllerBLO(new UnitOfWork());
            appcontrollerBLO.Save(appcontroller_to_delete);
            AppControllersController controller = new AppControllersController();

            // Acte
            var result = controller.DeleteConfirmed(appcontroller_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_AppController_Test()
        {
            // Arrange
            AppControllersController controller = new AppControllersController();

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
        //    AppControllersController controller = new AppControllersController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    AppControllersController controller = new AppControllersController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

