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
    public class AbsencesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public AbsencesControllerTests()
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
        /// Find the first Absence instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Absence CreateOrLouadFirstAbsence(UnitOfWork unitOfWork)
        {
            AbsenceBLO absenceBLO = new AbsenceBLO(unitOfWork);
           
		   Absence entity = null;
            if (absenceBLO.FindAll()?.Count > 0)
                entity = absenceBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Absence for Test
                entity = this.CreateValideAbsenceInstance();
                absenceBLO.Save(entity);
            }
            return entity;
        }

        private Absence CreateValideAbsenceInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Absence  Valide_Absence = this._Fixture.Create<Absence>();
            Valide_Absence.Id = 0;
            // Many to One 
            //
			// SeanceTraining
			var SeanceTraining = new SeanceTrainingsControllerTests().CreateOrLouadFirstSeanceTraining(unitOfWork);
            Valide_Absence.SeanceTraining = null;
            Valide_Absence.SeanceTrainingId = SeanceTraining.Id;
			// Trainee
			var Trainee = new TraineesControllerTests().CreateOrLouadFirstTrainee(unitOfWork);
            Valide_Absence.Trainee = null;
            Valide_Absence.TraineeId = Trainee.Id;
            // One to Many
            //
            return Valide_Absence;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Absence can't exist</returns>
        private Absence CreateInValideAbsenceInstance(UnitOfWork unitOfWork = null)
        {
            Absence absence = this.CreateValideAbsenceInstance(unitOfWork);
             
			// Required   
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
 
			absence.SeanceTrainingId = 0;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence(new UnitOfWork());
            
            return absence;
        }


		  private Absence CreateInValideAbsenceInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Absence absence = this.CreateOrLouadFirstAbsence(unitOfWork);
             
			// Required   
 
			absence.TraineeId = 0;
 
			absence.isHaveAuthorization = false;
 
			absence.SeanceTrainingId = 0;
            //Unique
			var existant_Absence = this.CreateOrLouadFirstAbsence(new UnitOfWork());
            
            return absence;
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
            AbsencesController AbsencesController = new AbsencesController();

            //Act
            ViewResult viewResult = AbsencesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            AbsencesController AbsencesController = new AbsencesController();

            ViewResult viewResult = AbsencesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Absence_Post_Test()
        {
            //--Arrange--
            AbsencesController controller = new AbsencesController();
            Absence absence = this.CreateValideAbsenceInstance();

            //--Acte--
            //
            AbsencesControllerTests.PreBindModel(controller, absence, nameof(AbsencesController.Create));
            AbsencesControllerTests.ValidateViewModel(controller,absence);

			Default_AbsenceFormView Default_AbsenceFormView = new Default_AbsenceFormViewBLM(controller._UnitOfWork).ConverTo_Default_AbsenceFormView(absence);
            var result = controller.Create(Default_AbsenceFormView);
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
        public void Create_InValide_Absence_Post_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence = this.CreateInValideAbsenceInstance();
            if (absence == null) return;
            AbsenceBLO absenceBLO = new AbsenceBLO(controller._UnitOfWork);

            // Acte
            AbsencesControllerTests.PreBindModel(controller, absence, nameof(AbsencesController.Create));
            List<ValidationResult>  ls_validation_errors = AbsencesControllerTests
                .ValidateViewModel(controller, absence);

			Default_AbsenceFormView Default_AbsenceFormView = new Default_AbsenceFormViewBLM(controller._UnitOfWork).ConverTo_Default_AbsenceFormView(absence);
            var result = controller.Create(Default_AbsenceFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = absenceBLO.Validate(absence);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_Absence_Not_Exist_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Absence_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Absence));
            
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence =  this.CreateOrLouadFirstAbsence(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(absence.Id) as ViewResult;
            var AbsenceDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AbsenceDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AbsenceDetailModelView, typeof(Absence));
        }

        [TestMethod()]
        public void Edit_Valide_Absence_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Absence));

            // Arrange
            AbsencesController controller = new AbsencesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Absence absence = this.CreateOrLouadFirstAbsence(new UnitOfWork());
			 
       

            // Acte
            AbsencesControllerTests.PreBindModel(controller, absence, nameof(AbsencesController.Edit));
            AbsencesControllerTests.ValidateViewModel(controller, absence);

			Default_AbsenceFormView Default_AbsenceFormView = new Default_AbsenceFormViewBLM(controller._UnitOfWork).ConverTo_Default_AbsenceFormView(absence);
            var result = controller.Edit(Default_AbsenceFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Absence_Post_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence = this.CreateInValideAbsenceInstance_ForEdit(new UnitOfWork());
            if (absence == null) return;
            AbsenceBLO absenceBLO = new AbsenceBLO(controller._UnitOfWork);

            // Acte
            AbsencesControllerTests.PreBindModel(controller, absence, nameof(AbsencesController.Edit));
            List<ValidationResult> ls_validation_errors = AbsencesControllerTests
                .ValidateViewModel(controller, absence);

			Default_AbsenceFormView Default_AbsenceFormView = new Default_AbsenceFormViewBLM(controller._UnitOfWork).ConverTo_Default_AbsenceFormView(absence);
            var result = controller.Edit(Default_AbsenceFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = absenceBLO.Validate(absence);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_Absence_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Absence));
			 
            // Arrange
            AbsencesController controller = new AbsencesController();
            Absence absence = this.CreateOrLouadFirstAbsence(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(absence.Id) as ViewResult;
            var AbsenceDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AbsenceDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AbsenceDetailModelView, typeof(Absence));
        }

        [TestMethod()]
        public void Delete_Absence_Post_Test()
        {
            // Arrange
            //
            // Create Absence to Delete
            Absence absence_to_delete = this.CreateValideAbsenceInstance();
            AbsenceBLO absenceBLO = new AbsenceBLO(new UnitOfWork());
            absenceBLO.Save(absence_to_delete);
            AbsencesController controller = new AbsencesController();

            // Acte
            var result = controller.DeleteConfirmed(absence_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Absence_Test()
        {
            // Arrange
            AbsencesController controller = new AbsencesController();

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

