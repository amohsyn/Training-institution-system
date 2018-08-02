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
    public class SeanceNumbersControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SeanceNumbersControllerTests()
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
        /// Find the first SeanceNumber instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public SeanceNumber CreateOrLouadFirstSeanceNumber(UnitOfWork unitOfWork)
        {
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(unitOfWork);
           
		   SeanceNumber entity = null;
            if (seancenumberBLO.FindAll()?.Count > 0)
                entity = seancenumberBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp SeanceNumber for Test
                entity = this.CreateValideSeanceNumberInstance();
                seancenumberBLO.Save(entity);
            }
            return entity;
        }

        private SeanceNumber CreateValideSeanceNumberInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            SeanceNumber  Valide_SeanceNumber = this._Fixture.Create<SeanceNumber>();
            Valide_SeanceNumber.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_SeanceNumber;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceNumber can't exist</returns>
        private SeanceNumber CreateInValideSeanceNumberInstance(UnitOfWork unitOfWork = null)
        {
            SeanceNumber seancenumber = this.CreateValideSeanceNumberInstance(unitOfWork);
             
			// Required   
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber(new UnitOfWork());
            
            return seancenumber;
        }


		  private SeanceNumber CreateInValideSeanceNumberInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            SeanceNumber seancenumber = this.CreateOrLouadFirstSeanceNumber(unitOfWork);
             
			// Required   
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber(new UnitOfWork());
            
            return seancenumber;
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
            SeanceNumbersController SeanceNumbersController = new SeanceNumbersController();

            //Act
            ViewResult viewResult = SeanceNumbersController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            SeanceNumbersController SeanceNumbersController = new SeanceNumbersController();

            ViewResult viewResult = SeanceNumbersController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_SeanceNumber_Post_Test()
        {
            //--Arrange--
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber = this.CreateValideSeanceNumberInstance();

            //--Acte--
            //
            SeanceNumbersControllerTests.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Create));
            SeanceNumbersControllerTests.ValidateViewModel(controller,seancenumber);

			Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceNumberFormView(seancenumber);
            var result = controller.Create(Default_SeanceNumberFormView);
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
        public void Create_InValide_SeanceNumber_Post_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber = this.CreateInValideSeanceNumberInstance();
            if (seancenumber == null) return;
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(controller._UnitOfWork);

            // Acte
            SeanceNumbersControllerTests.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Create));
            List<ValidationResult>  ls_validation_errors = SeanceNumbersControllerTests
                .ValidateViewModel(controller, seancenumber);

			Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceNumberFormView(seancenumber);
            var result = controller.Create(Default_SeanceNumberFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancenumberBLO.Validate(seancenumber);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


  [TestMethod()]
        public void EditGet_SeanceNumber_Not_Exist_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceNumber_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber =  this.CreateOrLouadFirstSeanceNumber(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seancenumber.Id) as ViewResult;
            var SeanceNumberDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceNumberDetailModelView, typeof(Default_SeanceNumberFormView));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceNumber_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceNumber));

            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceNumber seancenumber = this.CreateOrLouadFirstSeanceNumber(new UnitOfWork());
			 
       

            // Acte
            SeanceNumbersControllerTests.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Edit));
            SeanceNumbersControllerTests.ValidateViewModel(controller, seancenumber);

			Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceNumberFormView(seancenumber);
            var result = controller.Edit(Default_SeanceNumberFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceNumber_Post_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber = this.CreateInValideSeanceNumberInstance_ForEdit(new UnitOfWork());
            if (seancenumber == null) return;
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(controller._UnitOfWork);

            // Acte
            SeanceNumbersControllerTests.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceNumbersControllerTests
                .ValidateViewModel(controller, seancenumber);

			Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceNumberFormView(seancenumber);
            var result = controller.Edit(Default_SeanceNumberFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancenumberBLO.Validate(seancenumber);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_SeanceNumber_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceNumber));
			 
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber = this.CreateOrLouadFirstSeanceNumber(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(seancenumber.Id) as ViewResult;
            var SeanceNumberDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceNumberDetailModelView, typeof(Default_SeanceNumberDetailsView));
        }

        [TestMethod()]
        public void Delete_SeanceNumber_Post_Test()
        {
            // Arrange
            //
            // Create SeanceNumber to Delete
            SeanceNumber seancenumber_to_delete = this.CreateValideSeanceNumberInstance();
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(new UnitOfWork());
            seancenumberBLO.Save(seancenumber_to_delete);
            SeanceNumbersController controller = new SeanceNumbersController();

            // Acte
            var result = controller.DeleteConfirmed(seancenumber_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeanceNumber_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();

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

