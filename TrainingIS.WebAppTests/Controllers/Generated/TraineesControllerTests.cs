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
    public class TraineesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private Trainee Valide_Trainee;
        private Trainee Existant_Trainee_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private Trainee Trainee_to_Delete_On_CleanUP = null;

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
            Existant_Trainee_In_DB_Value =  this.CreateOrLouadFirstTrainee();
        }

        private Trainee CreateOrLouadFirstTrainee()
        {
            TraineeBLO traineeBLO = new TraineeBLO(this.TestUnitOfWork);
            Trainee entity = traineeBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp Trainee for Test
                entity = this.CreateValideTraineeInstance();
                traineeBLO.Save(entity);
                Trainee_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private Trainee CreateValideTraineeInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Trainee  Valide_Trainee = this._Fixture.Create<Trainee>();
            Valide_Trainee.Id = 0;
            // Many to One 
            //

            // Group
            var Group = new GroupBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_Trainee.Group = null;
            Valide_Trainee.GroupId = (Group == null) ? 0 : Group.Id;
            // Nationality
            var Nationality = new NationalityBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_Trainee.Nationality = null;
            Valide_Trainee.NationalityId = (Nationality == null) ? 0 : Nationality.Id;
            // Schoollevel
            var Schoollevel = new SchoollevelBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_Trainee.Schoollevel = null;
            Valide_Trainee.SchoollevelId = (Schoollevel == null) ? 0 : Schoollevel.Id;
            // One to Many
            //
             Valide_Trainee.StateOfAbseces = null;



            return Valide_Trainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Trainee can't exist</returns>
        private Trainee CreateInValideTraineeInstance()
        {
            Trainee trainee = this.CreateValideTraineeInstance();
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.NationalityId = 0;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.FirstNameArabe = null;
 
			trainee.LastNameArabe = null;
 
			trainee.Birthdate = DateTime.Now;
 
			trainee.BirthPlace = null;
 
			trainee.Sex = SexEnum.man;
 
			trainee.CIN = null;
            //Unique
			trainee.CNE = this.Existant_Trainee_In_DB_Value.CNE;
			trainee.CIN = this.Existant_Trainee_In_DB_Value.CIN;
            
            return trainee;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(Trainee_to_Delete_On_CleanUP != null)
            {
                TraineeBLO traineeBLO = new TraineeBLO(this.TestUnitOfWork);
                traineeBLO.Delete(this.Trainee_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            TraineesController TraineesController = new TraineesController();

            //Act
            ViewResult viewResult = TraineesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            TraineesController TraineesController = new TraineesController();

            ViewResult viewResult = TraineesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Trainee_Post_Test()
        {
            //--Arrange--
            TraineesController controller = new TraineesController();
            Trainee trainee = this.CreateValideTraineeInstance();

            //--Acte--
            //
            TraineesControllerTests.PreBindModel(controller, trainee, nameof(TraineesController.Create));
            TraineesControllerTests.ValidateViewModel(controller,trainee);
            var result = controller.Create(trainee);
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
        public void Create_InValide_Trainee_Post_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = this.CreateInValideTraineeInstance();
            if (trainee == null) return;
            TraineeBLO traineeBLO = new TraineeBLO(controller._UnitOfWork);

            // Acte
            TraineesControllerTests.PreBindModel(controller, trainee, nameof(TraineesController.Create));
            List<ValidationResult>  ls_validation_errors = TraineesControllerTests
                .ValidateViewModel(controller, trainee);
            var result = controller.Create(trainee);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = traineeBLO.Validate(trainee);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_Trainee_Not_Exist_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Trainee_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Trainee));
            
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = this.Existant_Trainee_In_DB_Value;

            // Acte
            var result = controller.Edit(trainee.Id) as ViewResult;
            var TraineeDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TraineeDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Trainee));
        }

        [TestMethod()]
        public void Edit_Valide_Trainee_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Trainee));

            // Arrange
            TraineesController controller = new TraineesController();
           // controller.SetFakeControllerContext();
            
          
            Trainee trainee = this.Existant_Trainee_In_DB_Value;


            // Acte
            TraineesControllerTests.PreBindModel(controller, trainee, nameof(TraineesController.Edit));
            TraineesControllerTests.ValidateViewModel(controller, trainee);
            var result = controller.Edit(trainee);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Trainee_Post_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = this.CreateInValideTraineeInstance();
            if (trainee == null) return;
            TraineeBLO traineeBLO = new TraineeBLO(controller._UnitOfWork);

            // Acte
            TraineesControllerTests.PreBindModel(controller, trainee, nameof(TraineesController.Create));
            List<ValidationResult> ls_validation_errors = TraineesControllerTests
                .ValidateViewModel(controller, trainee);
            var result = controller.Edit(trainee);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = traineeBLO.Validate(trainee);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_Trainee_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Trainee));

            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee = this.Existant_Trainee_In_DB_Value;

            // Acte
            var result = controller.Delete(trainee.Id) as ViewResult;
            var TraineeDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TraineeDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Trainee));
        }

        [TestMethod()]
        public void Delete_Trainee_Post_Test()
        {
            // Arrange
            //
            // Create Trainee to Delete
            Trainee trainee_to_delete = this.CreateValideTraineeInstance();
            TraineeBLO traineeBLO = new TraineeBLO(new UnitOfWork());
            traineeBLO.Save(trainee_to_delete);
            TraineesController controller = new TraineesController();

            // Acte
            var result = controller.DeleteConfirmed(trainee_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Trainee_Test()
        {
            // Arrange
            TraineesController controller = new TraineesController();

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
        //    TraineesController controller = new TraineesController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    TraineesController controller = new TraineesController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

