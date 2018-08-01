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
    public class SeanceDaysControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SeanceDaysControllerTests()
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
        /// Find the first SeanceDay instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public SeanceDay CreateOrLouadFirstSeanceDay(UnitOfWork unitOfWork)
        {
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(unitOfWork);
           
		   SeanceDay entity = null;
            if (seancedayBLO.FindAll()?.Count > 0)
                entity = seancedayBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp SeanceDay for Test
                entity = this.CreateValideSeanceDayInstance();
                seancedayBLO.Save(entity);
            }
            return entity;
        }

        private SeanceDay CreateValideSeanceDayInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            SeanceDay  Valide_SeanceDay = this._Fixture.Create<SeanceDay>();
            Valide_SeanceDay.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_SeanceDay;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceDay can't exist</returns>
        private SeanceDay CreateInValideSeanceDayInstance(UnitOfWork unitOfWork = null)
        {
            SeanceDay seanceday = this.CreateValideSeanceDayInstance(unitOfWork);
             
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay(new UnitOfWork());
            
            return seanceday;
        }


		  private SeanceDay CreateInValideSeanceDayInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            SeanceDay seanceday = this.CreateOrLouadFirstSeanceDay(unitOfWork);
             
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
			var existant_SeanceDay = this.CreateOrLouadFirstSeanceDay(new UnitOfWork());
            
            return seanceday;
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
            SeanceDaysController SeanceDaysController = new SeanceDaysController();

            //Act
            ViewResult viewResult = SeanceDaysController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            SeanceDaysController SeanceDaysController = new SeanceDaysController();

            ViewResult viewResult = SeanceDaysController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_SeanceDay_Post_Test()
        {
            //--Arrange--
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = this.CreateValideSeanceDayInstance();

            //--Acte--
            //
            SeanceDaysControllerTests.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Create));
            SeanceDaysControllerTests.ValidateViewModel(controller,seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Create(Default_SeanceDayFormView);
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
        public void Create_InValide_SeanceDay_Post_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = this.CreateInValideSeanceDayInstance();
            if (seanceday == null) return;
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(controller._UnitOfWork);

            // Acte
            SeanceDaysControllerTests.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Create));
            List<ValidationResult>  ls_validation_errors = SeanceDaysControllerTests
                .ValidateViewModel(controller, seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Create(Default_SeanceDayFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancedayBLO.Validate(seanceday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_SeanceDay_Not_Exist_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceDay_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceDay));
            
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday =  this.CreateOrLouadFirstSeanceDay(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seanceday.Id) as ViewResult;
            var SeanceDayDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeanceDayDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeanceDayDetailModelView, typeof(SeanceDay));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceDay_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceDay));

            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceDay seanceday = this.CreateOrLouadFirstSeanceDay(new UnitOfWork());
			 
       

            // Acte
            SeanceDaysControllerTests.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Edit));
            SeanceDaysControllerTests.ValidateViewModel(controller, seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Edit(Default_SeanceDayFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceDay_Post_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = this.CreateInValideSeanceDayInstance_ForEdit(new UnitOfWork());
            if (seanceday == null) return;
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(controller._UnitOfWork);

            // Acte
            SeanceDaysControllerTests.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceDaysControllerTests
                .ValidateViewModel(controller, seanceday);

			Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceDayFormView(seanceday);
            var result = controller.Edit(Default_SeanceDayFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancedayBLO.Validate(seanceday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_SeanceDay_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceDay));
			 
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = this.CreateOrLouadFirstSeanceDay(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(seanceday.Id) as ViewResult;
            var SeanceDayDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeanceDayDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeanceDayDetailModelView, typeof(SeanceDay));
        }

        [TestMethod()]
        public void Delete_SeanceDay_Post_Test()
        {
            // Arrange
            //
            // Create SeanceDay to Delete
            SeanceDay seanceday_to_delete = this.CreateValideSeanceDayInstance();
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(new UnitOfWork());
            seancedayBLO.Save(seanceday_to_delete);
            SeanceDaysController controller = new SeanceDaysController();

            // Acte
            var result = controller.DeleteConfirmed(seanceday_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeanceDay_Test()
        {
            // Arrange
            SeanceDaysController controller = new SeanceDaysController();

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

