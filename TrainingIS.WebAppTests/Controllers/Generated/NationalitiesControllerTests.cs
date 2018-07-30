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
    public class NationalitiesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private Nationality Valide_Nationality;
        private Nationality Existant_Nationality_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private Nationality Nationality_to_Delete_On_CleanUP = null;

        #region Initialize
        [TestInitialize]
        public void InitTest()
        {
            // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            TestUnitOfWork = new UnitOfWork();
            Existant_Nationality_In_DB_Value =  this.CreateOrLouadFirstNationality();
        }

        private Nationality CreateOrLouadFirstNationality()
        {
            NationalityBLO nationalityBLO = new NationalityBLO(this.TestUnitOfWork);
            Nationality entity = nationalityBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp Nationality for Test
                entity = this.CreateValideNationalityInstance();
                nationalityBLO.Save(entity);
                Nationality_to_Delete_On_CleanUP = entity;
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
        private Nationality CreateInValideNationalityInstance()
        {
            Nationality nationality = this.CreateValideNationalityInstance();
             
			// Required   
 
			nationality.Code = null;
 
			nationality.Name = null;
            //Unique
            
            return nationality;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(Nationality_to_Delete_On_CleanUP != null)
            {
                NationalityBLO nationalityBLO = new NationalityBLO(this.TestUnitOfWork);
                nationalityBLO.Delete(this.Nationality_to_Delete_On_CleanUP);
            }

        }
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
            var result = controller.Create(nationality);
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
            var result = controller.Create(nationality);
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
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Nationality));
            
            // Arrange
            NationalitiesController controller = new NationalitiesController();
            Nationality nationality = this.Existant_Nationality_In_DB_Value;

            // Acte
            var result = controller.Edit(nationality.Id) as ViewResult;
            var NationalityDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(NationalityDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(NationalityDetailModelView, typeof(Nationality));
        }

        [TestMethod()]
        public void Edit_Valide_Nationality_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Nationality));

            // Arrange
            NationalitiesController controller = new NationalitiesController();
           // controller.SetFakeControllerContext();
            
          
            Nationality nationality = this.Existant_Nationality_In_DB_Value;


            // Acte
            NationalitiesControllerTests.PreBindModel(controller, nationality, nameof(NationalitiesController.Edit));
            NationalitiesControllerTests.ValidateViewModel(controller, nationality);
            var result = controller.Edit(nationality);
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
            Nationality nationality = this.CreateInValideNationalityInstance();
            if (nationality == null) return;
            NationalityBLO nationalityBLO = new NationalityBLO(controller._UnitOfWork);

            // Acte
            NationalitiesControllerTests.PreBindModel(controller, nationality, nameof(NationalitiesController.Create));
            List<ValidationResult> ls_validation_errors = NationalitiesControllerTests
                .ValidateViewModel(controller, nationality);
            var result = controller.Edit(nationality);
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
            Nationality nationality = this.Existant_Nationality_In_DB_Value;

            // Acte
            var result = controller.Delete(nationality.Id) as ViewResult;
            var NationalityDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(NationalityDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(NationalityDetailModelView, typeof(Nationality));
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


        //[TestMethod()]
       // public void ExportTest()
        //{
            // Arrange
        //    NationalitiesController controller = new NationalitiesController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    NationalitiesController controller = new NationalitiesController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

