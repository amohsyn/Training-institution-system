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
    public class NationalitiesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public NationalitiesControllerTests()
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
        /// Find the first Nationality instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Nationality CreateOrLouadFirstNationality(UnitOfWork unitOfWork)
        {
            NationalityBLO nationalityBLO = new NationalityBLO(unitOfWork);
           
		   Nationality entity = null;
            if (nationalityBLO.FindAll()?.Count > 0)
                entity = nationalityBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Nationality for Test
                entity = this.CreateValideNationalityInstance();
                nationalityBLO.Save(entity);
            }
            return entity;
        }

        private Nationality CreateValideNationalityInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Nationality  Valide_Nationality = this._Fixture.Create<Nationality>();
            Valide_Nationality.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Nationality;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Nationality can't exist</returns>
        private Nationality CreateInValideNationalityInstance(UnitOfWork unitOfWork = null)
        {
            Nationality nationality = this.CreateValideNationalityInstance(unitOfWork);
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality(new UnitOfWork());
			nationality.Code = existant_Nationality.Code;
            
            return nationality;
        }


		  private Nationality CreateInValideNationalityInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Nationality nationality = this.CreateOrLouadFirstNationality(unitOfWork);
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
			var existant_Nationality = this.CreateOrLouadFirstNationality(new UnitOfWork());
			nationality.Code = existant_Nationality.Code;
            
            return nationality;
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
            NationalitiesController NationalitiesController = new NationalitiesController();

            //Act
            ViewResult viewResult = NationalitiesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            NationalitiesController NationalitiesController = new NationalitiesController();

            ViewResult viewResult = NationalitiesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Nationality_Post_Test()
        {
            //--Arrange--
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality = this.CreateValideNationalityInstance();

            //--Acte--
            //
            NationalitiesControllerTests.PreBindModel(controller, nationality, nameof(NationalitiesController.Create));
            NationalitiesControllerTests.ValidateViewModel(controller,nationality);

			Default_NationalityFormView Default_NationalityFormView = new Default_NationalityFormViewBLM(controller._UnitOfWork).ConverTo_Default_NationalityFormView(nationality);
            var result = controller.Create(Default_NationalityFormView);
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
        public void Create_InValide_Nationality_Post_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality = this.CreateInValideNationalityInstance();
            if (nationality == null) return;
            NationalityBLO nationalityBLO = new NationalityBLO(controller._UnitOfWork);

            // Acte
            NationalitiesControllerTests.PreBindModel(controller, nationality, nameof(NationalitiesController.Create));
            List<ValidationResult>  ls_validation_errors = NationalitiesControllerTests
                .ValidateViewModel(controller, nationality);

			Default_NationalityFormView Default_NationalityFormView = new Default_NationalityFormViewBLM(controller._UnitOfWork).ConverTo_Default_NationalityFormView(nationality);
            var result = controller.Create(Default_NationalityFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = nationalityBLO.Validate(nationality);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


  [TestMethod()]
        public void EditGet_Nationality_Not_Exist_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Nationality_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality =  this.CreateOrLouadFirstNationality(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(nationality.Id) as ViewResult;
            var NationalityDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(NationalityDetailModelView, typeof(Default_NationalityFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Nationality_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Nationality));

            // Arrange
            NationalitiesController controller = new NationalitiesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Nationality nationality = this.CreateOrLouadFirstNationality(new UnitOfWork());
			 
       

            // Acte
            NationalitiesControllerTests.PreBindModel(controller, nationality, nameof(NationalitiesController.Edit));
            NationalitiesControllerTests.ValidateViewModel(controller, nationality);

			Default_NationalityFormView Default_NationalityFormView = new Default_NationalityFormViewBLM(controller._UnitOfWork).ConverTo_Default_NationalityFormView(nationality);
            var result = controller.Edit(Default_NationalityFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Nationality_Post_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality = this.CreateInValideNationalityInstance_ForEdit(new UnitOfWork());
            if (nationality == null) return;
            NationalityBLO nationalityBLO = new NationalityBLO(controller._UnitOfWork);

            // Acte
            NationalitiesControllerTests.PreBindModel(controller, nationality, nameof(NationalitiesController.Edit));
            List<ValidationResult> ls_validation_errors = NationalitiesControllerTests
                .ValidateViewModel(controller, nationality);

			Default_NationalityFormView Default_NationalityFormView = new Default_NationalityFormViewBLM(controller._UnitOfWork).ConverTo_Default_NationalityFormView(nationality);
            var result = controller.Edit(Default_NationalityFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = nationalityBLO.Validate(nationality);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_Nationality_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Nationality));
			 
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality = this.CreateOrLouadFirstNationality(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(nationality.Id) as ViewResult;
            var NationalityDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(NationalityDetailModelView, typeof(Default_NationalityDetailsView));
        }

        [TestMethod()]
        public void Delete_Nationality_Post_Test()
        {
            // Arrange
            //
            // Create Nationality to Delete
            Nationality nationality_to_delete = this.CreateValideNationalityInstance();
            NationalityBLO nationalityBLO = new NationalityBLO(new UnitOfWork());
            nationalityBLO.Save(nationality_to_delete);
            NationalitiesController controller = new NationalitiesController();

            // Acte
            var result = controller.DeleteConfirmed(nationality_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Nationality_Test()
        {
            // Arrange
            NationalitiesController controller = new NationalitiesController();

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

