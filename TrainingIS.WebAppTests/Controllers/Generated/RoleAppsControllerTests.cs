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
    public class RoleAppsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public RoleAppsControllerTests()
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
        /// Find the first RoleApp instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public RoleApp CreateOrLouadFirstRoleApp(UnitOfWork unitOfWork)
        {
            RoleAppBLO roleappBLO = new RoleAppBLO(unitOfWork);
           
		   RoleApp entity = null;
            if (roleappBLO.FindAll()?.Count > 0)
                entity = roleappBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp RoleApp for Test
                entity = this.CreateValideRoleAppInstance();
                roleappBLO.Save(entity);
            }
            return entity;
        }

        private RoleApp CreateValideRoleAppInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            RoleApp  Valide_RoleApp = this._Fixture.Create<RoleApp>();
            Valide_RoleApp.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_RoleApp;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide RoleApp can't exist</returns>
        private RoleApp CreateInValideRoleAppInstance(UnitOfWork unitOfWork = null)
        {
            RoleApp roleapp = this.CreateValideRoleAppInstance(unitOfWork);
             
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp(new UnitOfWork());
            
            return roleapp;
        }


		  private RoleApp CreateInValideRoleAppInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            RoleApp roleapp = this.CreateOrLouadFirstRoleApp(unitOfWork);
             
			// Required   
 
			roleapp.Code = null;
            //Unique
			var existant_RoleApp = this.CreateOrLouadFirstRoleApp(new UnitOfWork());
            
            return roleapp;
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
            RoleAppsController RoleAppsController = new RoleAppsController();

            //Act
            ViewResult viewResult = RoleAppsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            RoleAppsController RoleAppsController = new RoleAppsController();

            ViewResult viewResult = RoleAppsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_RoleApp_Post_Test()
        {
            //--Arrange--
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = this.CreateValideRoleAppInstance();

            //--Acte--
            //
            RoleAppsControllerTests.PreBindModel(controller, roleapp, nameof(RoleAppsController.Create));
            RoleAppsControllerTests.ValidateViewModel(controller,roleapp);

			Default_RoleAppFormView Default_RoleAppFormView = new Default_RoleAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_RoleAppFormView(roleapp);
            var result = controller.Create(Default_RoleAppFormView);
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
        public void Create_InValide_RoleApp_Post_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = this.CreateInValideRoleAppInstance();
            if (roleapp == null) return;
            RoleAppBLO roleappBLO = new RoleAppBLO(controller._UnitOfWork);

            // Acte
            RoleAppsControllerTests.PreBindModel(controller, roleapp, nameof(RoleAppsController.Create));
            List<ValidationResult>  ls_validation_errors = RoleAppsControllerTests
                .ValidateViewModel(controller, roleapp);

			Default_RoleAppFormView Default_RoleAppFormView = new Default_RoleAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_RoleAppFormView(roleapp);
            var result = controller.Create(Default_RoleAppFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = roleappBLO.Validate(roleapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


  [TestMethod()]
        public void EditGet_RoleApp_Not_Exist_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_RoleApp_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp =  this.CreateOrLouadFirstRoleApp(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(roleapp.Id) as ViewResult;
            var RoleAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(RoleAppDetailModelView, typeof(Default_RoleAppFormView));
        }

        [TestMethod()]
        public void Edit_Valide_RoleApp_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(RoleApp));

            // Arrange
            RoleAppsController controller = new RoleAppsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            RoleApp roleapp = this.CreateOrLouadFirstRoleApp(new UnitOfWork());
			 
       

            // Acte
            RoleAppsControllerTests.PreBindModel(controller, roleapp, nameof(RoleAppsController.Edit));
            RoleAppsControllerTests.ValidateViewModel(controller, roleapp);

			Default_RoleAppFormView Default_RoleAppFormView = new Default_RoleAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_RoleAppFormView(roleapp);
            var result = controller.Edit(Default_RoleAppFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_RoleApp_Post_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = this.CreateInValideRoleAppInstance_ForEdit(new UnitOfWork());
            if (roleapp == null) return;
            RoleAppBLO roleappBLO = new RoleAppBLO(controller._UnitOfWork);

            // Acte
            RoleAppsControllerTests.PreBindModel(controller, roleapp, nameof(RoleAppsController.Edit));
            List<ValidationResult> ls_validation_errors = RoleAppsControllerTests
                .ValidateViewModel(controller, roleapp);

			Default_RoleAppFormView Default_RoleAppFormView = new Default_RoleAppFormViewBLM(controller._UnitOfWork).ConverTo_Default_RoleAppFormView(roleapp);
            var result = controller.Edit(Default_RoleAppFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = roleappBLO.Validate(roleapp);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_RoleApp_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(RoleApp));
			 
            // Arrange
            RoleAppsController controller = new RoleAppsController();
            RoleApp roleapp = this.CreateOrLouadFirstRoleApp(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(roleapp.Id) as ViewResult;
            var RoleAppDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(RoleAppDetailModelView, typeof(Default_RoleAppDetailsView));
        }

        [TestMethod()]
        public void Delete_RoleApp_Post_Test()
        {
            // Arrange
            //
            // Create RoleApp to Delete
            RoleApp roleapp_to_delete = this.CreateValideRoleAppInstance();
            RoleAppBLO roleappBLO = new RoleAppBLO(new UnitOfWork());
            roleappBLO.Save(roleapp_to_delete);
            RoleAppsController controller = new RoleAppsController();

            // Acte
            var result = controller.DeleteConfirmed(roleapp_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_RoleApp_Test()
        {
            // Arrange
            RoleAppsController controller = new RoleAppsController();

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

