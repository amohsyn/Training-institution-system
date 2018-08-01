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
    public class AppControllerActionsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public AppControllerActionsControllerTests()
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
        /// Find the first AppControllerAction instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public AppControllerAction CreateOrLouadFirstAppControllerAction(UnitOfWork unitOfWork)
        {
            AppControllerActionBLO appcontrolleractionBLO = new AppControllerActionBLO(unitOfWork);
           
		   AppControllerAction entity = null;
            if (appcontrolleractionBLO.FindAll()?.Count > 0)
                entity = appcontrolleractionBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp AppControllerAction for Test
                entity = this.CreateValideAppControllerActionInstance();
                appcontrolleractionBLO.Save(entity);
            }
            return entity;
        }

        private AppControllerAction CreateValideAppControllerActionInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            AppControllerAction  Valide_AppControllerAction = this._Fixture.Create<AppControllerAction>();
            Valide_AppControllerAction.Id = 0;
            // Many to One 
            //
			// AppController
			var AppController = new AppControllersControllerTests().CreateOrLouadFirstAppController(unitOfWork);
            Valide_AppControllerAction.AppController = null;
            Valide_AppControllerAction.AppControllerId = AppController.Id;
            // One to Many
            //
			Valide_AppControllerAction.AppRoles = null;
            return Valide_AppControllerAction;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide AppControllerAction can't exist</returns>
        private AppControllerAction CreateInValideAppControllerActionInstance(UnitOfWork unitOfWork = null)
        {
            AppControllerAction appcontrolleraction = this.CreateValideAppControllerActionInstance(unitOfWork);
             
			// Required   
 
			appcontrolleraction.Code = null;
 
			appcontrolleraction.AppControllerId = 0;
            //Unique
			var existant_AppControllerAction = this.CreateOrLouadFirstAppControllerAction(new UnitOfWork());
            
            return appcontrolleraction;
        }


		  private AppControllerAction CreateInValideAppControllerActionInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            AppControllerAction appcontrolleraction = this.CreateOrLouadFirstAppControllerAction(unitOfWork);
             
			// Required   
 
			appcontrolleraction.Code = null;
 
			appcontrolleraction.AppControllerId = 0;
            //Unique
			var existant_AppControllerAction = this.CreateOrLouadFirstAppControllerAction(new UnitOfWork());
            
            return appcontrolleraction;
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
            AppControllerActionsController AppControllerActionsController = new AppControllerActionsController();

            //Act
            ViewResult viewResult = AppControllerActionsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            AppControllerActionsController AppControllerActionsController = new AppControllerActionsController();

            ViewResult viewResult = AppControllerActionsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_AppControllerAction_Post_Test()
        {
            //--Arrange--
            AppControllerActionsController controller = new AppControllerActionsController();
            AppControllerAction appcontrolleraction = this.CreateValideAppControllerActionInstance();

            //--Acte--
            //
            AppControllerActionsControllerTests.PreBindModel(controller, appcontrolleraction, nameof(AppControllerActionsController.Create));
            AppControllerActionsControllerTests.ValidateViewModel(controller,appcontrolleraction);

			Default_AppControllerActionFormView Default_AppControllerActionFormView = new Default_AppControllerActionFormViewBLM(controller._UnitOfWork).ConverTo_Default_AppControllerActionFormView(appcontrolleraction);
            var result = controller.Create(Default_AppControllerActionFormView);
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
        public void Create_InValide_AppControllerAction_Post_Test()
        {
            // Arrange
            AppControllerActionsController controller = new AppControllerActionsController();
            AppControllerAction appcontrolleraction = this.CreateInValideAppControllerActionInstance();
            if (appcontrolleraction == null) return;
            AppControllerActionBLO appcontrolleractionBLO = new AppControllerActionBLO(controller._UnitOfWork);

            // Acte
            AppControllerActionsControllerTests.PreBindModel(controller, appcontrolleraction, nameof(AppControllerActionsController.Create));
            List<ValidationResult>  ls_validation_errors = AppControllerActionsControllerTests
                .ValidateViewModel(controller, appcontrolleraction);

			Default_AppControllerActionFormView Default_AppControllerActionFormView = new Default_AppControllerActionFormViewBLM(controller._UnitOfWork).ConverTo_Default_AppControllerActionFormView(appcontrolleraction);
            var result = controller.Create(Default_AppControllerActionFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = appcontrolleractionBLO.Validate(appcontrolleraction);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_AppControllerAction_Not_Exist_Test()
        {
            // Arrange
            AppControllerActionsController controller = new AppControllerActionsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_AppControllerAction_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppControllerAction));
            
            // Arrange
            AppControllerActionsController controller = new AppControllerActionsController();
            AppControllerAction appcontrolleraction =  this.CreateOrLouadFirstAppControllerAction(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(appcontrolleraction.Id) as ViewResult;
            var AppControllerActionDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AppControllerActionDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AppControllerActionDetailModelView, typeof(AppControllerAction));
        }

        [TestMethod()]
        public void Edit_Valide_AppControllerAction_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppControllerAction));

            // Arrange
            AppControllerActionsController controller = new AppControllerActionsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            AppControllerAction appcontrolleraction = this.CreateOrLouadFirstAppControllerAction(new UnitOfWork());
			 
       

            // Acte
            AppControllerActionsControllerTests.PreBindModel(controller, appcontrolleraction, nameof(AppControllerActionsController.Edit));
            AppControllerActionsControllerTests.ValidateViewModel(controller, appcontrolleraction);

			Default_AppControllerActionFormView Default_AppControllerActionFormView = new Default_AppControllerActionFormViewBLM(controller._UnitOfWork).ConverTo_Default_AppControllerActionFormView(appcontrolleraction);
            var result = controller.Edit(Default_AppControllerActionFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_AppControllerAction_Post_Test()
        {
            // Arrange
            AppControllerActionsController controller = new AppControllerActionsController();
            AppControllerAction appcontrolleraction = this.CreateInValideAppControllerActionInstance_ForEdit(new UnitOfWork());
            if (appcontrolleraction == null) return;
            AppControllerActionBLO appcontrolleractionBLO = new AppControllerActionBLO(controller._UnitOfWork);

            // Acte
            AppControllerActionsControllerTests.PreBindModel(controller, appcontrolleraction, nameof(AppControllerActionsController.Edit));
            List<ValidationResult> ls_validation_errors = AppControllerActionsControllerTests
                .ValidateViewModel(controller, appcontrolleraction);

			Default_AppControllerActionFormView Default_AppControllerActionFormView = new Default_AppControllerActionFormViewBLM(controller._UnitOfWork).ConverTo_Default_AppControllerActionFormView(appcontrolleraction);
            var result = controller.Edit(Default_AppControllerActionFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = appcontrolleractionBLO.Validate(appcontrolleraction);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_AppControllerAction_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppControllerAction));
			 
            // Arrange
            AppControllerActionsController controller = new AppControllerActionsController();
            AppControllerAction appcontrolleraction = this.CreateOrLouadFirstAppControllerAction(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(appcontrolleraction.Id) as ViewResult;
            var AppControllerActionDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AppControllerActionDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AppControllerActionDetailModelView, typeof(AppControllerAction));
        }

        [TestMethod()]
        public void Delete_AppControllerAction_Post_Test()
        {
            // Arrange
            //
            // Create AppControllerAction to Delete
            AppControllerAction appcontrolleraction_to_delete = this.CreateValideAppControllerActionInstance();
            AppControllerActionBLO appcontrolleractionBLO = new AppControllerActionBLO(new UnitOfWork());
            appcontrolleractionBLO.Save(appcontrolleraction_to_delete);
            AppControllerActionsController controller = new AppControllerActionsController();

            // Acte
            var result = controller.DeleteConfirmed(appcontrolleraction_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_AppControllerAction_Test()
        {
            // Arrange
            AppControllerActionsController controller = new AppControllerActionsController();

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

