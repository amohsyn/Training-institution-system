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
    public class SeanceDaysControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private SeanceDay Valide_SeanceDay;
        private SeanceDay Existant_SeanceDay_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private SeanceDay SeanceDay_to_Delete_On_CleanUP = null;

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
            Existant_SeanceDay_In_DB_Value =  this.CreateOrLouadFirstSeanceDay();
        }

        private SeanceDay CreateOrLouadFirstSeanceDay()
        {
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(this.TestUnitOfWork);
           
		   SeanceDay entity = null;
            if (seancedayBLO.FindAll()?.Count > 0)
                entity = seancedayBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp SeanceDay for Test
                entity = this.CreateValideSeanceDayInstance();
                seancedayBLO.Save(entity);
                SeanceDay_to_Delete_On_CleanUP = entity;
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
        private SeanceDay CreateInValideSeanceDayInstance()
        {
            SeanceDay seanceday = this.CreateValideSeanceDayInstance();
             
			// Required   
 
			seanceday.Name = null;
 
			seanceday.Code = null;
            //Unique
            
            return seanceday;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(SeanceDay_to_Delete_On_CleanUP != null)
            {
                SeanceDayBLO seancedayBLO = new SeanceDayBLO(this.TestUnitOfWork);
                seancedayBLO.Delete(this.SeanceDay_to_Delete_On_CleanUP);
            }

        }
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
            var result = controller.Create(seanceday);
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
            var result = controller.Create(seanceday);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancedayBLO.Validate(seanceday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
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
            SeanceDay seanceday = this.Existant_SeanceDay_In_DB_Value;

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
            
          
            SeanceDay seanceday = this.Existant_SeanceDay_In_DB_Value;


            // Acte
            SeanceDaysControllerTests.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Edit));
            SeanceDaysControllerTests.ValidateViewModel(controller, seanceday);
            var result = controller.Edit(seanceday);
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
            SeanceDay seanceday = this.CreateInValideSeanceDayInstance();
            if (seanceday == null) return;
            SeanceDayBLO seancedayBLO = new SeanceDayBLO(controller._UnitOfWork);

            // Acte
            SeanceDaysControllerTests.PreBindModel(controller, seanceday, nameof(SeanceDaysController.Create));
            List<ValidationResult> ls_validation_errors = SeanceDaysControllerTests
                .ValidateViewModel(controller, seanceday);
            var result = controller.Edit(seanceday);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancedayBLO.Validate(seanceday);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_SeanceDay_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceDay));

            // Arrange
            SeanceDaysController controller = new SeanceDaysController();
            SeanceDay seanceday = this.Existant_SeanceDay_In_DB_Value;

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


        //[TestMethod()]
       // public void ExportTest()
        //{
            // Arrange
        //    SeanceDaysController controller = new SeanceDaysController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    SeanceDaysController controller = new SeanceDaysController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

