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
    public class YearStudiesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public YearStudiesControllerTests()
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
        /// Find the first YearStudy instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public YearStudy CreateOrLouadFirstYearStudy(UnitOfWork unitOfWork)
        {
            YearStudyBLO yearstudyBLO = new YearStudyBLO(unitOfWork);
           
		   YearStudy entity = null;
            if (yearstudyBLO.FindAll()?.Count > 0)
                entity = yearstudyBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp YearStudy for Test
                entity = this.CreateValideYearStudyInstance();
                yearstudyBLO.Save(entity);
            }
            return entity;
        }

        private YearStudy CreateValideYearStudyInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            YearStudy  Valide_YearStudy = this._Fixture.Create<YearStudy>();
            Valide_YearStudy.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_YearStudy;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide YearStudy can't exist</returns>
        private YearStudy CreateInValideYearStudyInstance(UnitOfWork unitOfWork = null)
        {
            YearStudy yearstudy = this.CreateValideYearStudyInstance(unitOfWork);
             
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy(new UnitOfWork());
            
            return yearstudy;
        }


		  private YearStudy CreateInValideYearStudyInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            YearStudy yearstudy = this.CreateOrLouadFirstYearStudy(unitOfWork);
             
			// Required   
 
			yearstudy.Code = null;
 
			yearstudy.Name = null;
            //Unique
			var existant_YearStudy = this.CreateOrLouadFirstYearStudy(new UnitOfWork());
            
            return yearstudy;
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
            YearStudiesController YearStudiesController = new YearStudiesController();

            //Act
            ViewResult viewResult = YearStudiesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            YearStudiesController YearStudiesController = new YearStudiesController();

            ViewResult viewResult = YearStudiesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_YearStudy_Post_Test()
        {
            //--Arrange--
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy = this.CreateValideYearStudyInstance();

            //--Acte--
            //
            YearStudiesControllerTests.PreBindModel(controller, yearstudy, nameof(YearStudiesController.Create));
            YearStudiesControllerTests.ValidateViewModel(controller,yearstudy);
            var result = controller.Create(yearstudy);
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
        public void Create_InValide_YearStudy_Post_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy = this.CreateInValideYearStudyInstance();
            if (yearstudy == null) return;
            YearStudyBLO yearstudyBLO = new YearStudyBLO(controller._UnitOfWork);

            // Acte
            YearStudiesControllerTests.PreBindModel(controller, yearstudy, nameof(YearStudiesController.Create));
            List<ValidationResult>  ls_validation_errors = YearStudiesControllerTests
                .ValidateViewModel(controller, yearstudy);
            var result = controller.Create(yearstudy);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = yearstudyBLO.Validate(yearstudy);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


       
        [TestMethod()]
        public void EditGet_YearStudy_Not_Exist_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_YearStudy_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(YearStudy));
            
            // Arrange
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy =  this.CreateOrLouadFirstYearStudy(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(yearstudy.Id) as ViewResult;
            var YearStudyDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(YearStudyDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(YearStudyDetailModelView, typeof(YearStudy));
        }

        [TestMethod()]
        public void Edit_Valide_YearStudy_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(YearStudy));

            // Arrange
            YearStudiesController controller = new YearStudiesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            YearStudy yearstudy = this.CreateOrLouadFirstYearStudy(new UnitOfWork());
			 
       

            // Acte
            YearStudiesControllerTests.PreBindModel(controller, yearstudy, nameof(YearStudiesController.Edit));
            YearStudiesControllerTests.ValidateViewModel(controller, yearstudy);
            var result = controller.Edit(yearstudy);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_YearStudy_Post_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy = this.CreateInValideYearStudyInstance_ForEdit(new UnitOfWork());
            if (yearstudy == null) return;
            YearStudyBLO yearstudyBLO = new YearStudyBLO(controller._UnitOfWork);

            // Acte
            YearStudiesControllerTests.PreBindModel(controller, yearstudy, nameof(YearStudiesController.Edit));
            List<ValidationResult> ls_validation_errors = YearStudiesControllerTests
                .ValidateViewModel(controller, yearstudy);
            var result = controller.Edit(yearstudy);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = yearstudyBLO.Validate(yearstudy);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

        [TestMethod()]
        public void Delete_YearStudy_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(YearStudy));
			 
            // Arrange
            YearStudiesController controller = new YearStudiesController();
            YearStudy yearstudy = this.CreateOrLouadFirstYearStudy(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(yearstudy.Id) as ViewResult;
            var YearStudyDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(YearStudyDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(YearStudyDetailModelView, typeof(YearStudy));
        }

        [TestMethod()]
        public void Delete_YearStudy_Post_Test()
        {
            // Arrange
            //
            // Create YearStudy to Delete
            YearStudy yearstudy_to_delete = this.CreateValideYearStudyInstance();
            YearStudyBLO yearstudyBLO = new YearStudyBLO(new UnitOfWork());
            yearstudyBLO.Save(yearstudy_to_delete);
            YearStudiesController controller = new YearStudiesController();

            // Acte
            var result = controller.DeleteConfirmed(yearstudy_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_YearStudy_Test()
        {
            // Arrange
            YearStudiesController controller = new YearStudiesController();

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
        //    YearStudiesController controller = new YearStudiesController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    YearStudiesController controller = new YearStudiesController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

