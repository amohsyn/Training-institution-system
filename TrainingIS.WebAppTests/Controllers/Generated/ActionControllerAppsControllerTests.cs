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
    public class ActionControllerAppsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ActionControllerAppsControllerTests()
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
        /// Find the first ActionControllerApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public ActionControllerApp CreateOrLouadFirstActionControllerApp(UnitOfWork unitOfWork)
        {
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(unitOfWork);
           
		   ActionControllerApp entity = null;
            if (actioncontrollerappBLO.FindAll()?.Count > 0)
                entity = actioncontrollerappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ActionControllerApp for Test
                entity = this.CreateValideActionControllerAppInstance();
                actioncontrollerappBLO.Save(entity);
            }
            return entity;
        }

        private ActionControllerApp CreateValideActionControllerAppInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            ActionControllerApp  Valide_ActionControllerApp = this._Fixture.Create<ActionControllerApp>();
            Valide_ActionControllerApp.Id = 0;
            // Many to One 
            //
			// ControllerApp
			var ControllerApp = new ControllerAppsControllerTests().CreateOrLouadFirstControllerApp(unitOfWork);
            Valide_ActionControllerApp.ControllerApp = null;
            Valide_ActionControllerApp.ControllerAppId = ControllerApp.Id;
            // One to Many
            //
			Valide_ActionControllerApp.AuthrorizationApps = null;
            return Valide_ActionControllerApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ActionControllerApp can't exist</returns>
        private ActionControllerApp CreateInValideActionControllerAppInstance(UnitOfWork unitOfWork = null)
        {
            ActionControllerApp actioncontrollerapp = this.CreateValideActionControllerAppInstance(unitOfWork);
             
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp(new UnitOfWork());
            
            return actioncontrollerapp;
        }


		  private ActionControllerApp CreateInValideActionControllerAppInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            ActionControllerApp actioncontrollerapp = this.CreateOrLouadFirstActionControllerApp(unitOfWork);
             
			// Required   
 
			actioncontrollerapp.Code = null;
 
			actioncontrollerapp.Name = null;
 
			actioncontrollerapp.ControllerAppId = 0;
            //Unique
			var existant_ActionControllerApp = this.CreateOrLouadFirstActionControllerApp(new UnitOfWork());
            
            return actioncontrollerapp;
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
            ActionControllerAppsController ActionControllerAppsController = new ActionControllerAppsController();

            //Act
            ViewResult viewResult = ActionControllerAppsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            ActionControllerAppsController ActionControllerAppsController = new ActionControllerAppsController();

            ViewResult viewResult = ActionControllerAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ActionControllerApp_Post_Test()
        {
            //--Arrange--
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = this.CreateValideActionControllerAppInstance();

            //--Acte--
            //
            ActionControllerAppsControllerTests.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Create));
            ActionControllerAppsControllerTests.ValidateViewModel(controller,actioncontrollerapp);

			Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ActionControllerAppFormView(actioncontrollerapp);
            var result = controller.Create(Default_ActionControllerAppFormView);
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
        public void Create_InValide_ActionControllerApp_Post_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = this.CreateInValideActionControllerAppInstance();
            if (actioncontrollerapp == null) return;
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(controller._UnitOfWork);

            // Acte
            ActionControllerAppsControllerTests.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Create));
            List<ValidationResult>  ls_validation_errors = ActionControllerAppsControllerTests
                .ValidateViewModel(controller, actioncontrollerapp);

			Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ActionControllerAppFormView(actioncontrollerapp);
            var result = controller.Create(Default_ActionControllerAppFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = actioncontrollerappBLO.Validate(actioncontrollerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


  [TestMethod()]
        public void EditGet_ActionControllerApp_Not_Exist_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ActionControllerApp_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp =  this.CreateOrLouadFirstActionControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(actioncontrollerapp.Id) as ViewResult;
            var ActionControllerAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ActionControllerAppDetailModelView, typeof(Default_ActionControllerAppFormView));
        }

        [TestMethod()]
        public void Edit_Valide_ActionControllerApp_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ActionControllerApp));

            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ActionControllerApp actioncontrollerapp = this.CreateOrLouadFirstActionControllerApp(new UnitOfWork());
			 
       

            // Acte
            ActionControllerAppsControllerTests.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Edit));
            ActionControllerAppsControllerTests.ValidateViewModel(controller, actioncontrollerapp);

			Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ActionControllerAppFormView(actioncontrollerapp);
            var result = controller.Edit(Default_ActionControllerAppFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ActionControllerApp_Post_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = this.CreateInValideActionControllerAppInstance_ForEdit(new UnitOfWork());
            if (actioncontrollerapp == null) return;
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(controller._UnitOfWork);

            // Acte
            ActionControllerAppsControllerTests.PreBindModel(controller, actioncontrollerapp, nameof(ActionControllerAppsController.Edit));
            List<ValidationResult> ls_validation_errors = ActionControllerAppsControllerTests
                .ValidateViewModel(controller, actioncontrollerapp);

			Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_ActionControllerAppFormView(actioncontrollerapp);
            var result = controller.Edit(Default_ActionControllerAppFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = actioncontrollerappBLO.Validate(actioncontrollerapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_ActionControllerApp_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ActionControllerApp));
			 
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();
            ActionControllerApp actioncontrollerapp = this.CreateOrLouadFirstActionControllerApp(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(actioncontrollerapp.Id) as ViewResult;
            var ActionControllerAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ActionControllerAppDetailModelView, typeof(Default_ActionControllerAppDetailsView));
        }

        [TestMethod()]
        public void Delete_ActionControllerApp_Post_Test()
        {
            // Arrange
            //
            // Create ActionControllerApp to Delete
            ActionControllerApp actioncontrollerapp_to_delete = this.CreateValideActionControllerAppInstance();
            ActionControllerAppBLO actioncontrollerappBLO = new ActionControllerAppBLO(new UnitOfWork());
            actioncontrollerappBLO.Save(actioncontrollerapp_to_delete);
            ActionControllerAppsController controller = new ActionControllerAppsController();

            // Acte
            var result = controller.DeleteConfirmed(actioncontrollerapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ActionControllerApp_Test()
        {
            // Arrange
            ActionControllerAppsController controller = new ActionControllerAppsController();

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

