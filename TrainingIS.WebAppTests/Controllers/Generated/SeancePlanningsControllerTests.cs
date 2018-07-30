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
    public class SeancePlanningsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private SeancePlanning Valide_SeancePlanning;
        private SeancePlanning Existant_SeancePlanning_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private SeancePlanning SeancePlanning_to_Delete_On_CleanUP = null;

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
            Existant_SeancePlanning_In_DB_Value =  this.CreateOrLouadFirstSeancePlanning();
        }

        private SeancePlanning CreateOrLouadFirstSeancePlanning()
        {
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(this.TestUnitOfWork);
            SeancePlanning entity = seanceplanningBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp SeancePlanning for Test
                entity = this.CreateValideSeancePlanningInstance();
                seanceplanningBLO.Save(entity);
                SeancePlanning_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private SeancePlanning CreateValideSeancePlanningInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            SeancePlanning  Valide_SeancePlanning = this._Fixture.Create<SeancePlanning>();
            Valide_SeancePlanning.Id = 0;
            // Many to One 
            //

            // SeanceDay
            var SeanceDay = new SeanceDayBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_SeancePlanning.SeanceDay = null;
            Valide_SeancePlanning.SeanceDayId = (SeanceDay == null) ? 0 : SeanceDay.Id;
            // SeanceNumber
            var SeanceNumber = new SeanceNumberBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_SeancePlanning.SeanceNumber = null;
            Valide_SeancePlanning.SeanceNumberId = (SeanceNumber == null) ? 0 : SeanceNumber.Id;
            // Training
            var Training = new TrainingBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_SeancePlanning.Training = null;
            Valide_SeancePlanning.TrainingId = (Training == null) ? 0 : Training.Id;
            // One to Many
            //



            return Valide_SeancePlanning;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeancePlanning can't exist</returns>
        private SeancePlanning CreateInValideSeancePlanningInstance()
        {
            SeancePlanning seanceplanning = this.CreateValideSeancePlanningInstance();
             
			// Required   
 
			seanceplanning.TrainingId = 0;
 
			seanceplanning.SeanceDayId = 0;
 
			seanceplanning.SeanceNumberId = 0;
            //Unique
            
            return seanceplanning;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(SeancePlanning_to_Delete_On_CleanUP != null)
            {
                SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(this.TestUnitOfWork);
                seanceplanningBLO.Delete(this.SeancePlanning_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            SeancePlanningsController SeancePlanningsController = new SeancePlanningsController();

            //Act
            ViewResult viewResult = SeancePlanningsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            SeancePlanningsController SeancePlanningsController = new SeancePlanningsController();

            ViewResult viewResult = SeancePlanningsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_SeancePlanning_Post_Test()
        {
            //--Arrange--
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = this.CreateValideSeancePlanningInstance();

            //--Acte--
            //
            SeancePlanningsControllerTests.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Create));
            SeancePlanningsControllerTests.ValidateViewModel(controller,seanceplanning);
            var result = controller.Create(seanceplanning);
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
        public void Create_InValide_SeancePlanning_Post_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = this.CreateInValideSeancePlanningInstance();
            if (seanceplanning == null) return;
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(controller._UnitOfWork);

            // Acte
            SeancePlanningsControllerTests.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Create));
            List<ValidationResult>  ls_validation_errors = SeancePlanningsControllerTests
                .ValidateViewModel(controller, seanceplanning);
            var result = controller.Create(seanceplanning);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seanceplanningBLO.Validate(seanceplanning);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_SeancePlanning_Not_Exist_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeancePlanning_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeancePlanning));
            
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = this.Existant_SeancePlanning_In_DB_Value;

            // Acte
            var result = controller.Edit(seanceplanning.Id) as ViewResult;
            var SeancePlanningDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeancePlanningDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeancePlanningDetailModelView, typeof(SeancePlanning));
        }

        [TestMethod()]
        public void Edit_Valide_SeancePlanning_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeancePlanning));

            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
           // controller.SetFakeControllerContext();
            
          
            SeancePlanning seanceplanning = this.Existant_SeancePlanning_In_DB_Value;


            // Acte
            SeancePlanningsControllerTests.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Edit));
            SeancePlanningsControllerTests.ValidateViewModel(controller, seanceplanning);
            var result = controller.Edit(seanceplanning);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeancePlanning_Post_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = this.CreateInValideSeancePlanningInstance();
            if (seanceplanning == null) return;
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(controller._UnitOfWork);

            // Acte
            SeancePlanningsControllerTests.PreBindModel(controller, seanceplanning, nameof(SeancePlanningsController.Create));
            List<ValidationResult> ls_validation_errors = SeancePlanningsControllerTests
                .ValidateViewModel(controller, seanceplanning);
            var result = controller.Edit(seanceplanning);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seanceplanningBLO.Validate(seanceplanning);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_SeancePlanning_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeancePlanning));

            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();
            SeancePlanning seanceplanning = this.Existant_SeancePlanning_In_DB_Value;

            // Acte
            var result = controller.Delete(seanceplanning.Id) as ViewResult;
            var SeancePlanningDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeancePlanningDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeancePlanningDetailModelView, typeof(SeancePlanning));
        }

        [TestMethod()]
        public void Delete_SeancePlanning_Post_Test()
        {
            // Arrange
            //
            // Create SeancePlanning to Delete
            SeancePlanning seanceplanning_to_delete = this.CreateValideSeancePlanningInstance();
            SeancePlanningBLO seanceplanningBLO = new SeancePlanningBLO(new UnitOfWork());
            seanceplanningBLO.Save(seanceplanning_to_delete);
            SeancePlanningsController controller = new SeancePlanningsController();

            // Acte
            var result = controller.DeleteConfirmed(seanceplanning_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_SeancePlanning_Test()
        {
            // Arrange
            SeancePlanningsController controller = new SeancePlanningsController();

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
        //    SeancePlanningsController controller = new SeancePlanningsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    SeancePlanningsController controller = new SeancePlanningsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

