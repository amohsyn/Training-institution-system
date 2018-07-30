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
    public class EntityPropertyShortcutsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public EntityPropertyShortcutsControllerTests()
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
        /// Find the first EntityPropertyShortcut instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public EntityPropertyShortcut CreateOrLouadFirstEntityPropertyShortcut(UnitOfWork unitOfWork)
        {
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(unitOfWork);
           
		   EntityPropertyShortcut entity = null;
            if (entitypropertyshortcutBLO.FindAll()?.Count > 0)
                entity = entitypropertyshortcutBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp EntityPropertyShortcut for Test
                entity = this.CreateValideEntityPropertyShortcutInstance();
                entitypropertyshortcutBLO.Save(entity);
            }
            return entity;
        }

        private EntityPropertyShortcut CreateValideEntityPropertyShortcutInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            EntityPropertyShortcut  Valide_EntityPropertyShortcut = this._Fixture.Create<EntityPropertyShortcut>();
            Valide_EntityPropertyShortcut.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_EntityPropertyShortcut;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide EntityPropertyShortcut can't exist</returns>
        private EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance(UnitOfWork unitOfWork = null)
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateValideEntityPropertyShortcutInstance(unitOfWork);
             
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork());
            
            return entitypropertyshortcut;
        }


		  private EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateOrLouadFirstEntityPropertyShortcut(unitOfWork);
             
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork());
            
            return entitypropertyshortcut;
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
            EntityPropertyShortcutsController EntityPropertyShortcutsController = new EntityPropertyShortcutsController();

            //Act
            ViewResult viewResult = EntityPropertyShortcutsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            EntityPropertyShortcutsController EntityPropertyShortcutsController = new EntityPropertyShortcutsController();

            ViewResult viewResult = EntityPropertyShortcutsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_EntityPropertyShortcut_Post_Test()
        {
            //--Arrange--
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = this.CreateValideEntityPropertyShortcutInstance();

            //--Acte--
            //
            EntityPropertyShortcutsControllerTests.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Create));
            EntityPropertyShortcutsControllerTests.ValidateViewModel(controller,entitypropertyshortcut);
            var result = controller.Create(entitypropertyshortcut);
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
        public void Create_InValide_EntityPropertyShortcut_Post_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = this.CreateInValideEntityPropertyShortcutInstance();
            if (entitypropertyshortcut == null) return;
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(controller._UnitOfWork);

            // Acte
            EntityPropertyShortcutsControllerTests.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Create));
            List<ValidationResult>  ls_validation_errors = EntityPropertyShortcutsControllerTests
                .ValidateViewModel(controller, entitypropertyshortcut);
            var result = controller.Create(entitypropertyshortcut);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = entitypropertyshortcutBLO.Validate(entitypropertyshortcut);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


       
        [TestMethod()]
        public void EditGet_EntityPropertyShortcut_Not_Exist_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_EntityPropertyShortcut_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(EntityPropertyShortcut));
            
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut =  this.CreateOrLouadFirstEntityPropertyShortcut(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(entitypropertyshortcut.Id) as ViewResult;
            var EntityPropertyShortcutDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, typeof(EntityPropertyShortcut));
        }

        [TestMethod()]
        public void Edit_Valide_EntityPropertyShortcut_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(EntityPropertyShortcut));

            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            EntityPropertyShortcut entitypropertyshortcut = this.CreateOrLouadFirstEntityPropertyShortcut(new UnitOfWork());
			 
       

            // Acte
            EntityPropertyShortcutsControllerTests.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Edit));
            EntityPropertyShortcutsControllerTests.ValidateViewModel(controller, entitypropertyshortcut);
            var result = controller.Edit(entitypropertyshortcut);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_EntityPropertyShortcut_Post_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = this.CreateInValideEntityPropertyShortcutInstance_ForEdit(new UnitOfWork());
            if (entitypropertyshortcut == null) return;
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(controller._UnitOfWork);

            // Acte
            EntityPropertyShortcutsControllerTests.PreBindModel(controller, entitypropertyshortcut, nameof(EntityPropertyShortcutsController.Edit));
            List<ValidationResult> ls_validation_errors = EntityPropertyShortcutsControllerTests
                .ValidateViewModel(controller, entitypropertyshortcut);
            var result = controller.Edit(entitypropertyshortcut);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = entitypropertyshortcutBLO.Validate(entitypropertyshortcut);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

        [TestMethod()]
        public void Delete_EntityPropertyShortcut_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(EntityPropertyShortcut));
			 
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();
            EntityPropertyShortcut entitypropertyshortcut = this.CreateOrLouadFirstEntityPropertyShortcut(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(entitypropertyshortcut.Id) as ViewResult;
            var EntityPropertyShortcutDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(EntityPropertyShortcutDetailModelView, typeof(EntityPropertyShortcut));
        }

        [TestMethod()]
        public void Delete_EntityPropertyShortcut_Post_Test()
        {
            // Arrange
            //
            // Create EntityPropertyShortcut to Delete
            EntityPropertyShortcut entitypropertyshortcut_to_delete = this.CreateValideEntityPropertyShortcutInstance();
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(new UnitOfWork());
            entitypropertyshortcutBLO.Save(entitypropertyshortcut_to_delete);
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte
            var result = controller.DeleteConfirmed(entitypropertyshortcut_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_EntityPropertyShortcut_Test()
        {
            // Arrange
            EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

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
        //    EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    EntityPropertyShortcutsController controller = new EntityPropertyShortcutsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

