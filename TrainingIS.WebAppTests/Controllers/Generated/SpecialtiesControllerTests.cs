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
    public class SpecialtiesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private Specialty Valide_Specialty;
        private Specialty Existant_Specialty_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private Specialty Specialty_to_Delete_On_CleanUP = null;

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
            Existant_Specialty_In_DB_Value =  this.CreateOrLouadFirstSpecialty();
        }

        private Specialty CreateOrLouadFirstSpecialty()
        {
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(this.TestUnitOfWork);
            Specialty entity = specialtyBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp Specialty for Test
                entity = this.CreateValideSpecialtyInstance();
                specialtyBLO.Save(entity);
                Specialty_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private Specialty CreateValideSpecialtyInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Specialty  Valide_Specialty = this._Fixture.Create<Specialty>();
            Valide_Specialty.Id = 0;
            // Many to One 
            //

            // One to Many
            //



            return Valide_Specialty;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Specialty can't exist</returns>
        private Specialty CreateInValideSpecialtyInstance()
        {
            Specialty specialty = this.CreateValideSpecialtyInstance();
             
			// Required   
 
			specialty.Code = null;
 
			specialty.Name = null;
            //Unique
            
            return specialty;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(Specialty_to_Delete_On_CleanUP != null)
            {
                SpecialtyBLO specialtyBLO = new SpecialtyBLO(this.TestUnitOfWork);
                specialtyBLO.Delete(this.Specialty_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            SpecialtiesController SpecialtiesController = new SpecialtiesController();

            //Act
            ViewResult viewResult = SpecialtiesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            SpecialtiesController SpecialtiesController = new SpecialtiesController();

            ViewResult viewResult = SpecialtiesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Specialty_Post_Test()
        {
            //--Arrange--
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = this.CreateValideSpecialtyInstance();

            //--Acte--
            //
            SpecialtiesControllerTests.PreBindModel(controller, specialty, nameof(SpecialtiesController.Create));
            SpecialtiesControllerTests.ValidateViewModel(controller,specialty);
            var result = controller.Create(specialty);
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
        public void Create_InValide_Specialty_Post_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = this.CreateInValideSpecialtyInstance();
            if (specialty == null) return;
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(controller._UnitOfWork);

            // Acte
            SpecialtiesControllerTests.PreBindModel(controller, specialty, nameof(SpecialtiesController.Create));
            List<ValidationResult>  ls_validation_errors = SpecialtiesControllerTests
                .ValidateViewModel(controller, specialty);
            var result = controller.Create(specialty);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = specialtyBLO.Validate(specialty);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_Specialty_Not_Exist_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Specialty_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Specialty));
            
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = this.Existant_Specialty_In_DB_Value;

            // Acte
            var result = controller.Edit(specialty.Id) as ViewResult;
            var SpecialtyDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SpecialtyDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SpecialtyDetailModelView, typeof(Specialty));
        }

        [TestMethod()]
        public void Edit_Valide_Specialty_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Specialty));

            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
           // controller.SetFakeControllerContext();
            
          
            Specialty specialty = this.Existant_Specialty_In_DB_Value;


            // Acte
            SpecialtiesControllerTests.PreBindModel(controller, specialty, nameof(SpecialtiesController.Edit));
            SpecialtiesControllerTests.ValidateViewModel(controller, specialty);
            var result = controller.Edit(specialty);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Specialty_Post_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = this.CreateInValideSpecialtyInstance();
            if (specialty == null) return;
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(controller._UnitOfWork);

            // Acte
            SpecialtiesControllerTests.PreBindModel(controller, specialty, nameof(SpecialtiesController.Create));
            List<ValidationResult> ls_validation_errors = SpecialtiesControllerTests
                .ValidateViewModel(controller, specialty);
            var result = controller.Edit(specialty);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = specialtyBLO.Validate(specialty);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_Specialty_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Specialty));

            // Arrange
            SpecialtiesController controller = new SpecialtiesController();
            Specialty specialty = this.Existant_Specialty_In_DB_Value;

            // Acte
            var result = controller.Delete(specialty.Id) as ViewResult;
            var SpecialtyDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SpecialtyDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SpecialtyDetailModelView, typeof(Specialty));
        }

        [TestMethod()]
        public void Delete_Specialty_Post_Test()
        {
            // Arrange
            //
            // Create Specialty to Delete
            Specialty specialty_to_delete = this.CreateValideSpecialtyInstance();
            SpecialtyBLO specialtyBLO = new SpecialtyBLO(new UnitOfWork());
            specialtyBLO.Save(specialty_to_delete);
            SpecialtiesController controller = new SpecialtiesController();

            // Acte
            var result = controller.DeleteConfirmed(specialty_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Specialty_Test()
        {
            // Arrange
            SpecialtiesController controller = new SpecialtiesController();

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
        //    SpecialtiesController controller = new SpecialtiesController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    SpecialtiesController controller = new SpecialtiesController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

