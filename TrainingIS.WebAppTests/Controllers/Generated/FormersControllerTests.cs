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
    public class FormersControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public FormersControllerTests()
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
        /// Find the first Former instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Former CreateOrLouadFirstFormer(UnitOfWork unitOfWork)
        {
            FormerBLO formerBLO = new FormerBLO(unitOfWork);
           
		   Former entity = null;
            if (formerBLO.FindAll()?.Count > 0)
                entity = formerBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Former for Test
                entity = this.CreateValideFormerInstance();
                formerBLO.Save(entity);
            }
            return entity;
        }

        private Former CreateValideFormerInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Former  Valide_Former = this._Fixture.Create<Former>();
            Valide_Former.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Former;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Former can't exist</returns>
        private Former CreateInValideFormerInstance(UnitOfWork unitOfWork = null)
        {
            Former former = this.CreateValideFormerInstance(unitOfWork);
             
			// Required   
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.Sex = false;
 
			former.Email = null;
 
			former.RegistrationNumber = null;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer(new UnitOfWork());
            
            return former;
        }


		  private Former CreateInValideFormerInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Former former = this.CreateOrLouadFirstFormer(unitOfWork);
             
			// Required   
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.Sex = false;
 
			former.Email = null;
 
			former.RegistrationNumber = null;
            //Unique
			var existant_Former = this.CreateOrLouadFirstFormer(new UnitOfWork());
            
            return former;
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
            FormersController FormersController = new FormersController();

            //Act
            ViewResult viewResult = FormersController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            FormersController FormersController = new FormersController();

            ViewResult viewResult = FormersController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Former_Post_Test()
        {
            //--Arrange--
            FormersController controller = new FormersController();
            Former former = this.CreateValideFormerInstance();

            //--Acte--
            //
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Create));
            FormersControllerTests.ValidateViewModel(controller,former);

			Default_FormerFormView Default_FormerFormView = new Default_FormerFormViewBLM(controller._UnitOfWork).ConverTo_Default_FormerFormView(former);
            var result = controller.Create(Default_FormerFormView);
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
        public void Create_InValide_Former_Post_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former = this.CreateInValideFormerInstance();
            if (former == null) return;
            FormerBLO formerBLO = new FormerBLO(controller._UnitOfWork);

            // Acte
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Create));
            List<ValidationResult>  ls_validation_errors = FormersControllerTests
                .ValidateViewModel(controller, former);

			Default_FormerFormView Default_FormerFormView = new Default_FormerFormViewBLM(controller._UnitOfWork).ConverTo_Default_FormerFormView(former);
            var result = controller.Create(Default_FormerFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = formerBLO.Validate(former);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


  [TestMethod()]
        public void EditGet_Former_Not_Exist_Test()
        {
            // Arrange
            FormersController controller = new FormersController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Former_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former =  this.CreateOrLouadFirstFormer(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(former.Id) as ViewResult;
            var FormerDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FormerDetailModelView, typeof(Default_FormerFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Former_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Former));

            // Arrange
            FormersController controller = new FormersController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Former former = this.CreateOrLouadFirstFormer(new UnitOfWork());
			 
       

            // Acte
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Edit));
            FormersControllerTests.ValidateViewModel(controller, former);

			Default_FormerFormView Default_FormerFormView = new Default_FormerFormViewBLM(controller._UnitOfWork).ConverTo_Default_FormerFormView(former);
            var result = controller.Edit(Default_FormerFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Former_Post_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former = this.CreateInValideFormerInstance_ForEdit(new UnitOfWork());
            if (former == null) return;
            FormerBLO formerBLO = new FormerBLO(controller._UnitOfWork);

            // Acte
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Edit));
            List<ValidationResult> ls_validation_errors = FormersControllerTests
                .ValidateViewModel(controller, former);

			Default_FormerFormView Default_FormerFormView = new Default_FormerFormViewBLM(controller._UnitOfWork).ConverTo_Default_FormerFormView(former);
            var result = controller.Edit(Default_FormerFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = formerBLO.Validate(former);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_Former_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Former));
			 
            // Arrange
            FormersController controller = new FormersController();
            Former former = this.CreateOrLouadFirstFormer(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(former.Id) as ViewResult;
            var FormerDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(FormerDetailModelView, typeof(Default_FormerDetailsView));
        }

        [TestMethod()]
        public void Delete_Former_Post_Test()
        {
            // Arrange
            //
            // Create Former to Delete
            Former former_to_delete = this.CreateValideFormerInstance();
            FormerBLO formerBLO = new FormerBLO(new UnitOfWork());
            formerBLO.Save(former_to_delete);
            FormersController controller = new FormersController();

            // Acte
            var result = controller.DeleteConfirmed(former_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Former_Test()
        {
            // Arrange
            FormersController controller = new FormersController();

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

