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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class ControllerAppsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ControllerAppsControllerTests()
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
        /// Find the first ControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public ControllerApp CreateOrLouadFirstControllerApp(UnitOfWork unitOfWork)
        {
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(unitOfWork);
           
		   ControllerApp entity = null;
            if (controllerappBLO.FindAll()?.Count > 0)
                entity = controllerappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ControllerApp for Test
                entity = this.CreateValideControllerAppInstance();
                controllerappBLO.Save(entity);
            }
            return entity;
        }

        private ControllerApp CreateValideControllerAppInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            ControllerApp  Valide_ControllerApp = this._Fixture.Create<ControllerApp>();
            Valide_ControllerApp.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_ControllerApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ControllerApp can't exist</returns>
        private ControllerApp CreateInValideControllerAppInstance(UnitOfWork unitOfWork = null)
        {
            ControllerApp controllerapp = this.CreateValideControllerAppInstance(unitOfWork);
             
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp(new UnitOfWork());
            
            return controllerapp;
        }


		  private ControllerApp CreateInValideControllerAppInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            ControllerApp controllerapp = this.CreateOrLouadFirstControllerApp(unitOfWork);
             
			// Required   
 
			controllerapp.Code = null;
 
			controllerapp.Name = null;
            //Unique
			var existant_ControllerApp = this.CreateOrLouadFirstControllerApp(new UnitOfWork());
            
            return controllerapp;
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
            ControllerAppsController ControllerAppsController = new ControllerAppsController();

            //Act
            ViewResult viewResult = ControllerAppsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            ControllerAppsController ControllerAppsController = new ControllerAppsController();

            ViewResult viewResult = ControllerAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ControllerApp_Post_Test()
        {
            //--Arrange--
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = this.CreateValideControllerAppInstance();

            //--Acte--
            //
            ControllerAppsControllerTests.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Create));
            ControllerAppsControllerTests.ValidateViewModel(controller,controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Create(Default_ControllerAppFormView);
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
        public void Create_InValide_ControllerApp_Post_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = this.CreateInValideControllerAppInstance();
            if (controllerapp == null) return;
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(controller._UnitOfWork);

            // Acte
            ControllerAppsControllerTests.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Create));
            List<ValidationResult>  ls_validation_errors = ControllerAppsControllerTests
                .ValidateViewModel(controller, controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Create(Default_ControllerAppFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = controllerappBLO.Validate(controllerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_ControllerApp_Not_Exist_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ControllerApp_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ControllerApp));
            
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp =  this.CreateOrLouadFirstControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(controllerapp.Id) as ViewResult;
            var ControllerAppDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ControllerAppDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ControllerAppDetailModelView, typeof(ControllerApp));
        }

        [TestMethod()]
        public void Edit_Valide_ControllerApp_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ControllerApp));

            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ControllerApp controllerapp = this.CreateOrLouadFirstControllerApp(new UnitOfWork());
			 
       

            // Acte
            ControllerAppsControllerTests.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Edit));
            ControllerAppsControllerTests.ValidateViewModel(controller, controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Edit(Default_ControllerAppFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ControllerApp_Post_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = this.CreateInValideControllerAppInstance_ForEdit(new UnitOfWork());
            if (controllerapp == null) return;
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(controller._UnitOfWork);

            // Acte
            ControllerAppsControllerTests.PreBindModel(controller, controllerapp, nameof(ControllerAppsController.Edit));
            List<ValidationResult> ls_validation_errors = ControllerAppsControllerTests
                .ValidateViewModel(controller, controllerapp);

			Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ControllerAppFormView(controllerapp);
            var result = controller.Edit(Default_ControllerAppFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = controllerappBLO.Validate(controllerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_ControllerApp_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ControllerApp));
			 
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();
            ControllerApp controllerapp = this.CreateOrLouadFirstControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(controllerapp.Id) as ViewResult;
            var ControllerAppDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ControllerAppDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ControllerAppDetailModelView, typeof(ControllerApp));
        }

        [TestMethod()]
        public void Delete_ControllerApp_Post_Test()
        {
            // Arrange
            //
            // Create ControllerApp to Delete
            ControllerApp controllerapp_to_delete = this.CreateValideControllerAppInstance();
            ControllerAppBLO controllerappBLO = new ControllerAppBLO(new UnitOfWork());
            controllerappBLO.Save(controllerapp_to_delete);
            ControllerAppsController controller = new ControllerAppsController();

            // Acte
            var result = controller.DeleteConfirmed(controllerapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ControllerApp_Test()
        {
            // Arrange
            ControllerAppsController controller = new ControllerAppsController();

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

