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
    public class TrainingYearsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private TrainingYear Valide_TrainingYear;
        private TrainingYear Existant_TrainingYear_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private TrainingYear TrainingYear_to_Delete_On_CleanUP = null;

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
            Existant_TrainingYear_In_DB_Value =  this.CreateOrLouadFirstTrainingYear();
        }

        private TrainingYear CreateOrLouadFirstTrainingYear()
        {
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(this.TestUnitOfWork);
            TrainingYear entity = trainingyearBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp TrainingYear for Test
                entity = this.CreateValideTrainingYearInstance();
                trainingyearBLO.Save(entity);
                TrainingYear_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private TrainingYear CreateValideTrainingYearInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            TrainingYear  Valide_TrainingYear = this._Fixture.Create<TrainingYear>();
            Valide_TrainingYear.Id = 0;
            // Many to One 
            //

            // One to Many
            //



            return Valide_TrainingYear;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingYear can't exist</returns>
        private TrainingYear CreateInValideTrainingYearInstance()
        {
            TrainingYear trainingyear = this.CreateValideTrainingYearInstance();
             
			// Required   
 
			trainingyear.Code = null;
 
			trainingyear.StartDate = null;
 
			trainingyear.EndtDate = null;
            //Unique
            
            return trainingyear;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(TrainingYear_to_Delete_On_CleanUP != null)
            {
                TrainingYearBLO trainingyearBLO = new TrainingYearBLO(this.TestUnitOfWork);
                trainingyearBLO.Delete(this.TrainingYear_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            TrainingYearsController TrainingYearsController = new TrainingYearsController();

            //Act
            ViewResult viewResult = TrainingYearsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            TrainingYearsController TrainingYearsController = new TrainingYearsController();

            ViewResult viewResult = TrainingYearsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_TrainingYear_Post_Test()
        {
            //--Arrange--
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = this.CreateValideTrainingYearInstance();

            //--Acte--
            //
            TrainingYearsControllerTests.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Create));
            TrainingYearsControllerTests.ValidateViewModel(controller,trainingyear);
            var result = controller.Create(trainingyear);
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
        public void Create_InValide_TrainingYear_Post_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = this.CreateInValideTrainingYearInstance();
            if (trainingyear == null) return;
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(controller._UnitOfWork);

            // Acte
            TrainingYearsControllerTests.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Create));
            List<ValidationResult>  ls_validation_errors = TrainingYearsControllerTests
                .ValidateViewModel(controller, trainingyear);
            var result = controller.Create(trainingyear);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingyearBLO.Validate(trainingyear);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_TrainingYear_Not_Exist_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_TrainingYear_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(TrainingYear));
            
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = this.Existant_TrainingYear_In_DB_Value;

            // Acte
            var result = controller.Edit(trainingyear.Id) as ViewResult;
            var TrainingYearDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TrainingYearDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TrainingYearDetailModelView, typeof(TrainingYear));
        }

        [TestMethod()]
        public void Edit_Valide_TrainingYear_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(TrainingYear));

            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
           // controller.SetFakeControllerContext();
            
          
            TrainingYear trainingyear = this.Existant_TrainingYear_In_DB_Value;


            // Acte
            TrainingYearsControllerTests.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Edit));
            TrainingYearsControllerTests.ValidateViewModel(controller, trainingyear);
            var result = controller.Edit(trainingyear);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_TrainingYear_Post_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = this.CreateInValideTrainingYearInstance();
            if (trainingyear == null) return;
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(controller._UnitOfWork);

            // Acte
            TrainingYearsControllerTests.PreBindModel(controller, trainingyear, nameof(TrainingYearsController.Create));
            List<ValidationResult> ls_validation_errors = TrainingYearsControllerTests
                .ValidateViewModel(controller, trainingyear);
            var result = controller.Edit(trainingyear);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingyearBLO.Validate(trainingyear);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_TrainingYear_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(TrainingYear));

            // Arrange
            TrainingYearsController controller = new TrainingYearsController();
            TrainingYear trainingyear = this.Existant_TrainingYear_In_DB_Value;

            // Acte
            var result = controller.Delete(trainingyear.Id) as ViewResult;
            var TrainingYearDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TrainingYearDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TrainingYearDetailModelView, typeof(TrainingYear));
        }

        [TestMethod()]
        public void Delete_TrainingYear_Post_Test()
        {
            // Arrange
            //
            // Create TrainingYear to Delete
            TrainingYear trainingyear_to_delete = this.CreateValideTrainingYearInstance();
            TrainingYearBLO trainingyearBLO = new TrainingYearBLO(new UnitOfWork());
            trainingyearBLO.Save(trainingyear_to_delete);
            TrainingYearsController controller = new TrainingYearsController();

            // Acte
            var result = controller.DeleteConfirmed(trainingyear_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_TrainingYear_Test()
        {
            // Arrange
            TrainingYearsController controller = new TrainingYearsController();

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
        //    TrainingYearsController controller = new TrainingYearsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    TrainingYearsController controller = new TrainingYearsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

