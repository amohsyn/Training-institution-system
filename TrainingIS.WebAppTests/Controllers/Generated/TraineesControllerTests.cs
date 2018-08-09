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
    public class TraineesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public TraineesControllerTests()
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
        /// Find the first Trainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Trainee CreateOrLouadFirstTrainee(UnitOfWork unitOfWork)
        {
            TraineeBLO traineeBLO = new TraineeBLO(unitOfWork);
           
		   Trainee entity = null;
            if (traineeBLO.FindAll()?.Count > 0)
                entity = traineeBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Trainee for Test
                entity = this.CreateValideTraineeInstance();
                traineeBLO.Save(entity);
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
			var Group = new GroupsControllerTests().CreateOrLouadFirstGroup(unitOfWork);
            Valide_Trainee.Group = null;
            Valide_Trainee.GroupId = Group.Id;
			// Nationality
			var Nationality = new NationalitiesControllerTests().CreateOrLouadFirstNationality(unitOfWork);
            Valide_Trainee.Nationality = null;
            Valide_Trainee.NationalityId = Nationality.Id;
			// Schoollevel
			var Schoollevel = new SchoollevelsControllerTests().CreateOrLouadFirstSchoollevel(unitOfWork);
            Valide_Trainee.Schoollevel = null;
            Valide_Trainee.SchoollevelId = Schoollevel.Id;
            // One to Many
            //
			Valide_Trainee.StateOfAbseces = null;
            return Valide_Trainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Trainee can't exist</returns>
        private Trainee CreateInValideTraineeInstance(UnitOfWork unitOfWork = null)
        {
            Trainee trainee = this.CreateValideTraineeInstance(unitOfWork);
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.FirstNameArabe = null;
 
			trainee.LastNameArabe = null;
 
			trainee.Sex = SexEnum.man;
 
			trainee.Birthdate = DateTime.Now;
 
			trainee.NationalityId = 0;
 
			trainee.BirthPlace = null;
 
			trainee.CIN = null;
 
			trainee.Email = null;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee(new UnitOfWork());
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
            
            return trainee;
        }


		  private Trainee CreateInValideTraineeInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Trainee trainee = this.CreateOrLouadFirstTrainee(unitOfWork);
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.FirstNameArabe = null;
 
			trainee.LastNameArabe = null;
 
			trainee.Sex = SexEnum.man;
 
			trainee.Birthdate = DateTime.Now;
 
			trainee.NationalityId = 0;
 
			trainee.BirthPlace = null;
 
			trainee.CIN = null;
 
			trainee.Email = null;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee(new UnitOfWork());
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
            
            return trainee;
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

			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TraineeFormView(trainee);
            var result = controller.Create(Default_TraineeFormView);
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

			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TraineeFormView(trainee);
            var result = controller.Create(Default_TraineeFormView);

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
            // Arrange
            TraineesController controller = new TraineesController();
            Trainee trainee =  this.CreateOrLouadFirstTrainee(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(trainee.Id) as ViewResult;
            var TraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Default_TraineeFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Trainee_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Trainee));

            // Arrange
            TraineesController controller = new TraineesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Trainee trainee = this.CreateOrLouadFirstTrainee(new UnitOfWork());
			 
       

            // Acte
            TraineesControllerTests.PreBindModel(controller, trainee, nameof(TraineesController.Edit));
            TraineesControllerTests.ValidateViewModel(controller, trainee);

			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TraineeFormView(trainee);
            var result = controller.Edit(Default_TraineeFormView);



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
            Trainee trainee = this.CreateInValideTraineeInstance_ForEdit(new UnitOfWork());
            if (trainee == null) return;
            TraineeBLO traineeBLO = new TraineeBLO(controller._UnitOfWork);

            // Acte
            TraineesControllerTests.PreBindModel(controller, trainee, nameof(TraineesController.Edit));
            List<ValidationResult> ls_validation_errors = TraineesControllerTests
                .ValidateViewModel(controller, trainee);

			Default_TraineeFormView Default_TraineeFormView = new Default_TraineeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TraineeFormView(trainee);
            var result = controller.Edit(Default_TraineeFormView);
 

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
            Trainee trainee = this.CreateOrLouadFirstTrainee(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(trainee.Id) as ViewResult;
            var TraineeDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(TraineeDetailModelView, typeof(Default_TraineeDetailsView));
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
    }
}

