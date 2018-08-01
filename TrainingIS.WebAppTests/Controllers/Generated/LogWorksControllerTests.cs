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
    public class LogWorksControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public LogWorksControllerTests()
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
        /// Find the first LogWork instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public LogWork CreateOrLouadFirstLogWork(UnitOfWork unitOfWork)
        {
            LogWorkBLO logworkBLO = new LogWorkBLO(unitOfWork);
           
		   LogWork entity = null;
            if (logworkBLO.FindAll()?.Count > 0)
                entity = logworkBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp LogWork for Test
                entity = this.CreateValideLogWorkInstance();
                logworkBLO.Save(entity);
            }
            return entity;
        }

        private LogWork CreateValideLogWorkInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            LogWork  Valide_LogWork = this._Fixture.Create<LogWork>();
            Valide_LogWork.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_LogWork;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide LogWork can't exist</returns>
        private LogWork CreateInValideLogWorkInstance(UnitOfWork unitOfWork = null)
        {
            LogWork logwork = this.CreateValideLogWorkInstance(unitOfWork);
             
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork(new UnitOfWork());
            
            return logwork;
        }


		  private LogWork CreateInValideLogWorkInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            LogWork logwork = this.CreateOrLouadFirstLogWork(unitOfWork);
             
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork(new UnitOfWork());
            
            return logwork;
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
            LogWorksController LogWorksController = new LogWorksController();

            //Act
            ViewResult viewResult = LogWorksController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            LogWorksController LogWorksController = new LogWorksController();

            ViewResult viewResult = LogWorksController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_LogWork_Post_Test()
        {
            //--Arrange--
            LogWorksController controller = new LogWorksController();
            LogWork logwork = this.CreateValideLogWorkInstance();

            //--Acte--
            //
            LogWorksControllerTests.PreBindModel(controller, logwork, nameof(LogWorksController.Create));
            LogWorksControllerTests.ValidateViewModel(controller,logwork);

			Default_LogWorkFormView Default_LogWorkFormView = new Default_LogWorkFormViewBLM(controller._UnitOfWork).ConverTo_Default_LogWorkFormView(logwork);
            var result = controller.Create(Default_LogWorkFormView);
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
        public void Create_InValide_LogWork_Post_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork = this.CreateInValideLogWorkInstance();
            if (logwork == null) return;
            LogWorkBLO logworkBLO = new LogWorkBLO(controller._UnitOfWork);

            // Acte
            LogWorksControllerTests.PreBindModel(controller, logwork, nameof(LogWorksController.Create));
            List<ValidationResult>  ls_validation_errors = LogWorksControllerTests
                .ValidateViewModel(controller, logwork);

			Default_LogWorkFormView Default_LogWorkFormView = new Default_LogWorkFormViewBLM(controller._UnitOfWork).ConverTo_Default_LogWorkFormView(logwork);
            var result = controller.Create(Default_LogWorkFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = logworkBLO.Validate(logwork);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_LogWork_Not_Exist_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_LogWork_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(LogWork));
            
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork =  this.CreateOrLouadFirstLogWork(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(logwork.Id) as ViewResult;
            var LogWorkDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(LogWorkDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(LogWorkDetailModelView, typeof(LogWork));
        }

        [TestMethod()]
        public void Edit_Valide_LogWork_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(LogWork));

            // Arrange
            LogWorksController controller = new LogWorksController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            LogWork logwork = this.CreateOrLouadFirstLogWork(new UnitOfWork());
			 
       

            // Acte
            LogWorksControllerTests.PreBindModel(controller, logwork, nameof(LogWorksController.Edit));
            LogWorksControllerTests.ValidateViewModel(controller, logwork);

			Default_LogWorkFormView Default_LogWorkFormView = new Default_LogWorkFormViewBLM(controller._UnitOfWork).ConverTo_Default_LogWorkFormView(logwork);
            var result = controller.Edit(Default_LogWorkFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_LogWork_Post_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork = this.CreateInValideLogWorkInstance_ForEdit(new UnitOfWork());
            if (logwork == null) return;
            LogWorkBLO logworkBLO = new LogWorkBLO(controller._UnitOfWork);

            // Acte
            LogWorksControllerTests.PreBindModel(controller, logwork, nameof(LogWorksController.Edit));
            List<ValidationResult> ls_validation_errors = LogWorksControllerTests
                .ValidateViewModel(controller, logwork);

			Default_LogWorkFormView Default_LogWorkFormView = new Default_LogWorkFormViewBLM(controller._UnitOfWork).ConverTo_Default_LogWorkFormView(logwork);
            var result = controller.Edit(Default_LogWorkFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = logworkBLO.Validate(logwork);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_LogWork_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(LogWork));
			 
            // Arrange
            LogWorksController controller = new LogWorksController();
            LogWork logwork = this.CreateOrLouadFirstLogWork(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(logwork.Id) as ViewResult;
            var LogWorkDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(LogWorkDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(LogWorkDetailModelView, typeof(LogWork));
        }

        [TestMethod()]
        public void Delete_LogWork_Post_Test()
        {
            // Arrange
            //
            // Create LogWork to Delete
            LogWork logwork_to_delete = this.CreateValideLogWorkInstance();
            LogWorkBLO logworkBLO = new LogWorkBLO(new UnitOfWork());
            logworkBLO.Save(logwork_to_delete);
            LogWorksController controller = new LogWorksController();

            // Acte
            var result = controller.DeleteConfirmed(logwork_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_LogWork_Test()
        {
            // Arrange
            LogWorksController controller = new LogWorksController();

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

