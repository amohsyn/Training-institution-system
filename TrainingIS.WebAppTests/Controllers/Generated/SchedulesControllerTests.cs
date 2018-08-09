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
    public class SchedulesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SchedulesControllerTests()
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
        /// Find the first Schedule instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Schedule CreateOrLouadFirstSchedule(UnitOfWork unitOfWork)
        {
            ScheduleBLO scheduleBLO = new ScheduleBLO(unitOfWork);
           
		   Schedule entity = null;
            if (scheduleBLO.FindAll()?.Count > 0)
                entity = scheduleBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Schedule for Test
                entity = this.CreateValideScheduleInstance();
                scheduleBLO.Save(entity);
            }
            return entity;
        }

        private Schedule CreateValideScheduleInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Schedule  Valide_Schedule = this._Fixture.Create<Schedule>();
            Valide_Schedule.Id = 0;
            // Many to One 
            //
			// TrainingYear
			var TrainingYear = new TrainingYearsControllerTests().CreateOrLouadFirstTrainingYear(unitOfWork);
            Valide_Schedule.TrainingYear = null;
            Valide_Schedule.TrainingYearId = TrainingYear.Id;
            // One to Many
            //
            return Valide_Schedule;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Schedule can't exist</returns>
        private Schedule CreateInValideScheduleInstance(UnitOfWork unitOfWork = null)
        {
            Schedule schedule = this.CreateValideScheduleInstance(unitOfWork);
             
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule(new UnitOfWork());
            
            return schedule;
        }


		  private Schedule CreateInValideScheduleInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Schedule schedule = this.CreateOrLouadFirstSchedule(unitOfWork);
             
			// Required   
 
			schedule.TrainingYearId = 0;
 
			schedule.StartDate = DateTime.Now;
 
			schedule.EndtDate = DateTime.Now;
            //Unique
			var existant_Schedule = this.CreateOrLouadFirstSchedule(new UnitOfWork());
            
            return schedule;
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
            SchedulesController SchedulesController = new SchedulesController();

            //Act
            ViewResult viewResult = SchedulesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            SchedulesController SchedulesController = new SchedulesController();

            ViewResult viewResult = SchedulesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Schedule_Post_Test()
        {
            //--Arrange--
            SchedulesController controller = new SchedulesController();
            Schedule schedule = this.CreateValideScheduleInstance();

            //--Acte--
            //
            SchedulesControllerTests.PreBindModel(controller, schedule, nameof(SchedulesController.Create));
            SchedulesControllerTests.ValidateViewModel(controller,schedule);

			Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormViewBLM(controller._UnitOfWork).ConverTo_Default_ScheduleFormView(schedule);
            var result = controller.Create(Default_ScheduleFormView);
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
        public void Create_InValide_Schedule_Post_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();
            Schedule schedule = this.CreateInValideScheduleInstance();
            if (schedule == null) return;
            ScheduleBLO scheduleBLO = new ScheduleBLO(controller._UnitOfWork);

            // Acte
            SchedulesControllerTests.PreBindModel(controller, schedule, nameof(SchedulesController.Create));
            List<ValidationResult>  ls_validation_errors = SchedulesControllerTests
                .ValidateViewModel(controller, schedule);

			Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormViewBLM(controller._UnitOfWork).ConverTo_Default_ScheduleFormView(schedule);
            var result = controller.Create(Default_ScheduleFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = scheduleBLO.Validate(schedule);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


  [TestMethod()]
        public void EditGet_Schedule_Not_Exist_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Schedule_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();
            Schedule schedule =  this.CreateOrLouadFirstSchedule(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(schedule.Id) as ViewResult;
            var ScheduleDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ScheduleDetailModelView, typeof(Default_ScheduleFormView));
        }

        [TestMethod()]
        public void Edit_Valide_Schedule_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Schedule));

            // Arrange
            SchedulesController controller = new SchedulesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Schedule schedule = this.CreateOrLouadFirstSchedule(new UnitOfWork());
			 
       

            // Acte
            SchedulesControllerTests.PreBindModel(controller, schedule, nameof(SchedulesController.Edit));
            SchedulesControllerTests.ValidateViewModel(controller, schedule);

			Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormViewBLM(controller._UnitOfWork).ConverTo_Default_ScheduleFormView(schedule);
            var result = controller.Edit(Default_ScheduleFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Schedule_Post_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();
            Schedule schedule = this.CreateInValideScheduleInstance_ForEdit(new UnitOfWork());
            if (schedule == null) return;
            ScheduleBLO scheduleBLO = new ScheduleBLO(controller._UnitOfWork);

            // Acte
            SchedulesControllerTests.PreBindModel(controller, schedule, nameof(SchedulesController.Edit));
            List<ValidationResult> ls_validation_errors = SchedulesControllerTests
                .ValidateViewModel(controller, schedule);

			Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormViewBLM(controller._UnitOfWork).ConverTo_Default_ScheduleFormView(schedule);
            var result = controller.Edit(Default_ScheduleFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = scheduleBLO.Validate(schedule);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

		 [TestMethod()]
        public void Delete_Schedule_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Schedule));
			 
            // Arrange
            SchedulesController controller = new SchedulesController();
            Schedule schedule = this.CreateOrLouadFirstSchedule(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(schedule.Id) as ViewResult;
            var ScheduleDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(ScheduleDetailModelView, typeof(Default_ScheduleDetailsView));
        }

        [TestMethod()]
        public void Delete_Schedule_Post_Test()
        {
            // Arrange
            //
            // Create Schedule to Delete
            Schedule schedule_to_delete = this.CreateValideScheduleInstance();
            ScheduleBLO scheduleBLO = new ScheduleBLO(new UnitOfWork());
            scheduleBLO.Save(schedule_to_delete);
            SchedulesController controller = new SchedulesController();

            // Acte
            var result = controller.DeleteConfirmed(schedule_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Schedule_Test()
        {
            // Arrange
            SchedulesController controller = new SchedulesController();

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

