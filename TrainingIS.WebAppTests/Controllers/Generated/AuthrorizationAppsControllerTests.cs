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
using TrainingIS.Entities.ModelsViews.Authorizations;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class AuthrorizationAppsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public AuthrorizationAppsControllerTests()
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
        /// Find the first AuthrorizationApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public AuthrorizationApp CreateOrLouadFirstAuthrorizationApp(UnitOfWork unitOfWork)
        {
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(unitOfWork);
           
		   AuthrorizationApp entity = null;
            if (authrorizationappBLO.FindAll()?.Count > 0)
                entity = authrorizationappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp AuthrorizationApp for Test
                entity = this.CreateValideAuthrorizationAppInstance();
                authrorizationappBLO.Save(entity);
            }
            return entity;
        }

        private AuthrorizationApp CreateValideAuthrorizationAppInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            AuthrorizationApp  Valide_AuthrorizationApp = this._Fixture.Create<AuthrorizationApp>();
            Valide_AuthrorizationApp.Id = 0;
            // Many to One 
            //
			// ControllerApp
			var ControllerApp = new ControllerAppsControllerTests().CreateOrLouadFirstControllerApp(unitOfWork);
            Valide_AuthrorizationApp.ControllerApp = null;
            Valide_AuthrorizationApp.ControllerAppId = ControllerApp.Id;
			// RoleApp
			var RoleApp = new RoleAppsControllerTests().CreateOrLouadFirstRoleApp(unitOfWork);
            Valide_AuthrorizationApp.RoleApp = null;
            Valide_AuthrorizationApp.RoleAppId = RoleApp.Id;
            // One to Many
            //
			Valide_AuthrorizationApp.ActionControllerApps = null;
            return Valide_AuthrorizationApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide AuthrorizationApp can't exist</returns>
        private AuthrorizationApp CreateInValideAuthrorizationAppInstance(UnitOfWork unitOfWork = null)
        {
            AuthrorizationApp authrorizationapp = this.CreateValideAuthrorizationAppInstance(unitOfWork);
             
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork());
            
            return authrorizationapp;
        }


		  private AuthrorizationApp CreateInValideAuthrorizationAppInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            AuthrorizationApp authrorizationapp = this.CreateOrLouadFirstAuthrorizationApp(unitOfWork);
             
			// Required   
 
			authrorizationapp.RoleAppId = 0;
 
			authrorizationapp.ControllerAppId = 0;
 
			authrorizationapp.isAllAction = false;
            //Unique
			var existant_AuthrorizationApp = this.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork());
            
            return authrorizationapp;
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
            AuthrorizationAppsController AuthrorizationAppsController = new AuthrorizationAppsController();

            //Act
            ViewResult viewResult = AuthrorizationAppsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            AuthrorizationAppsController AuthrorizationAppsController = new AuthrorizationAppsController();

            ViewResult viewResult = AuthrorizationAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_AuthrorizationApp_Post_Test()
        {
            //--Arrange--
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = this.CreateValideAuthrorizationAppInstance();

            //--Acte--
            //
            AuthrorizationAppsControllerTests.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Create));
            AuthrorizationAppsControllerTests.ValidateViewModel(controller,authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Create(AuthrorizationAppFormView);
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
        public void Create_InValide_AuthrorizationApp_Post_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = this.CreateInValideAuthrorizationAppInstance();
            if (authrorizationapp == null) return;
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(controller._UnitOfWork);

            // Acte
            AuthrorizationAppsControllerTests.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Create));
            List<ValidationResult>  ls_validation_errors = AuthrorizationAppsControllerTests
                .ValidateViewModel(controller, authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Create(AuthrorizationAppFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = authrorizationappBLO.Validate(authrorizationapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


  [TestMethod()]
        public void EditGet_AuthrorizationApp_Not_Exist_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_AuthrorizationApp_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp =  this.CreateOrLouadFirstAuthrorizationApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(authrorizationapp.Id) as ViewResult;
            var AuthrorizationAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AuthrorizationAppDetailModelView, typeof(AuthrorizationAppFormView));
        }

        [TestMethod()]
        public void Edit_Valide_AuthrorizationApp_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AuthrorizationApp));

            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            AuthrorizationApp authrorizationapp = this.CreateOrLouadFirstAuthrorizationApp(new UnitOfWork());
			 
       

            // Acte
            AuthrorizationAppsControllerTests.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Edit));
            AuthrorizationAppsControllerTests.ValidateViewModel(controller, authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Edit(AuthrorizationAppFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_AuthrorizationApp_Post_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = this.CreateInValideAuthrorizationAppInstance_ForEdit(new UnitOfWork());
            if (authrorizationapp == null) return;
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(controller._UnitOfWork);

            // Acte
            AuthrorizationAppsControllerTests.PreBindModel(controller, authrorizationapp, nameof(AuthrorizationAppsController.Edit));
            List<ValidationResult> ls_validation_errors = AuthrorizationAppsControllerTests
                .ValidateViewModel(controller, authrorizationapp);

			AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(controller._UnitOfWork).ConverTo_AuthrorizationAppFormView(authrorizationapp);
            var result = controller.Edit(AuthrorizationAppFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = authrorizationappBLO.Validate(authrorizationapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_AuthrorizationApp_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AuthrorizationApp));
			 
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();
            AuthrorizationApp authrorizationapp = this.CreateOrLouadFirstAuthrorizationApp(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(authrorizationapp.Id) as ViewResult;
            var AuthrorizationAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(AuthrorizationAppDetailModelView, typeof(Default_AuthrorizationAppDetailsView));
        }

        [TestMethod()]
        public void Delete_AuthrorizationApp_Post_Test()
        {
            // Arrange
            //
            // Create AuthrorizationApp to Delete
            AuthrorizationApp authrorizationapp_to_delete = this.CreateValideAuthrorizationAppInstance();
            AuthrorizationAppBLO authrorizationappBLO = new AuthrorizationAppBLO(new UnitOfWork());
            authrorizationappBLO.Save(authrorizationapp_to_delete);
            AuthrorizationAppsController controller = new AuthrorizationAppsController();

            // Acte
            var result = controller.DeleteConfirmed(authrorizationapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_AuthrorizationApp_Test()
        {
            // Arrange
            AuthrorizationAppsController controller = new AuthrorizationAppsController();

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

