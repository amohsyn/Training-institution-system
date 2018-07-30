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
    public class SeanceNumbersControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private SeanceNumber Valide_SeanceNumber;
        private SeanceNumber Existant_SeanceNumber_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private SeanceNumber SeanceNumber_to_Delete_On_CleanUP = null;

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
            Existant_SeanceNumber_In_DB_Value =  this.CreateOrLouadFirstSeanceNumber();
        }

        private SeanceNumber CreateOrLouadFirstSeanceNumber()
        {
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(this.TestUnitOfWork);
            SeanceNumber entity = seancenumberBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp SeanceNumber for Test
                entity = this.CreateValideSeanceNumberInstance();
                seancenumberBLO.Save(entity);
                SeanceNumber_to_Delete_On_CleanUP = entity;
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
        private SeanceNumber CreateInValideSeanceNumberInstance()
        {
            SeanceNumber seancenumber = this.CreateValideSeanceNumberInstance();
             
			// Required   
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
            
            return seancenumber;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(SeanceNumber_to_Delete_On_CleanUP != null)
            {
                SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(this.TestUnitOfWork);
                seancenumberBLO.Delete(this.SeanceNumber_to_Delete_On_CleanUP);
            }

        }
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
            var result = controller.Create(seancenumber);
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
            var result = controller.Create(seancenumber);
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
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceNumber));
            
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber = this.Existant_SeanceNumber_In_DB_Value;

            // Acte
            var result = controller.Edit(seancenumber.Id) as ViewResult;
            var SeanceNumberDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeanceNumberDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeanceNumberDetailModelView, typeof(SeanceNumber));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceNumber_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceNumber));

            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
           // controller.SetFakeControllerContext();
            
          
            SeanceNumber seancenumber = this.Existant_SeanceNumber_In_DB_Value;


            // Acte
            SeanceNumbersControllerTests.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Edit));
            SeanceNumbersControllerTests.ValidateViewModel(controller, seancenumber);
            var result = controller.Edit(seancenumber);
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
            SeanceNumber seancenumber = this.CreateInValideSeanceNumberInstance();
            if (seancenumber == null) return;
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(controller._UnitOfWork);

            // Acte
            SeanceNumbersControllerTests.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Create));
            List<ValidationResult> ls_validation_errors = SeanceNumbersControllerTests
                .ValidateViewModel(controller, seancenumber);
            var result = controller.Edit(seancenumber);
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
            SeanceNumber seancenumber = this.Existant_SeanceNumber_In_DB_Value;

            // Acte
            var result = controller.Delete(seancenumber.Id) as ViewResult;
            var SeanceNumberDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SeanceNumberDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SeanceNumberDetailModelView, typeof(SeanceNumber));
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


        //[TestMethod()]
       // public void ExportTest()
        //{
            // Arrange
        //    SeanceNumbersController controller = new SeanceNumbersController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    SeanceNumbersController controller = new SeanceNumbersController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

