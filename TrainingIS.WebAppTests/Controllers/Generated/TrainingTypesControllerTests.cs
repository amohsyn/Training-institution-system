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
    public class TrainingTypesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public TrainingTypesControllerTests()
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
        /// Find the first TrainingType instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public TrainingType CreateOrLouadFirstTrainingType(UnitOfWork unitOfWork)
        {
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(unitOfWork);
           
		   TrainingType entity = null;
            if (trainingtypeBLO.FindAll()?.Count > 0)
                entity = trainingtypeBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp TrainingType for Test
                entity = this.CreateValideTrainingTypeInstance();
                trainingtypeBLO.Save(entity);
            }
            return entity;
        }

        private TrainingType CreateValideTrainingTypeInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            TrainingType  Valide_TrainingType = this._Fixture.Create<TrainingType>();
            Valide_TrainingType.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_TrainingType;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingType can't exist</returns>
        private TrainingType CreateInValideTrainingTypeInstance(UnitOfWork unitOfWork = null)
        {
            TrainingType trainingtype = this.CreateValideTrainingTypeInstance(unitOfWork);
             
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType(new UnitOfWork());
            
            return trainingtype;
        }


		  private TrainingType CreateInValideTrainingTypeInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            TrainingType trainingtype = this.CreateOrLouadFirstTrainingType(unitOfWork);
             
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType(new UnitOfWork());
            
            return trainingtype;
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
            TrainingTypesController TrainingTypesController = new TrainingTypesController();

            //Act
            ViewResult viewResult = TrainingTypesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            TrainingTypesController TrainingTypesController = new TrainingTypesController();

            ViewResult viewResult = TrainingTypesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_TrainingType_Post_Test()
        {
            //--Arrange--
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = this.CreateValideTrainingTypeInstance();

            //--Acte--
            //
            TrainingTypesControllerTests.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Create));
            TrainingTypesControllerTests.ValidateViewModel(controller,trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Create(Default_TrainingTypeFormView);
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
        public void Create_InValide_TrainingType_Post_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = this.CreateInValideTrainingTypeInstance();
            if (trainingtype == null) return;
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(controller._UnitOfWork);

            // Acte
            TrainingTypesControllerTests.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Create));
            List<ValidationResult>  ls_validation_errors = TrainingTypesControllerTests
                .ValidateViewModel(controller, trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Create(Default_TrainingTypeFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingtypeBLO.Validate(trainingtype);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_TrainingType_Not_Exist_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_TrainingType_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(TrainingType));
            
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype =  this.CreateOrLouadFirstTrainingType(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(trainingtype.Id) as ViewResult;
            var TrainingTypeDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TrainingTypeDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TrainingTypeDetailModelView, typeof(TrainingType));
        }

        [TestMethod()]
        public void Edit_Valide_TrainingType_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(TrainingType));

            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            TrainingType trainingtype = this.CreateOrLouadFirstTrainingType(new UnitOfWork());
			 
       

            // Acte
            TrainingTypesControllerTests.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Edit));
            TrainingTypesControllerTests.ValidateViewModel(controller, trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Edit(Default_TrainingTypeFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_TrainingType_Post_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = this.CreateInValideTrainingTypeInstance_ForEdit(new UnitOfWork());
            if (trainingtype == null) return;
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(controller._UnitOfWork);

            // Acte
            TrainingTypesControllerTests.PreBindModel(controller, trainingtype, nameof(TrainingTypesController.Edit));
            List<ValidationResult> ls_validation_errors = TrainingTypesControllerTests
                .ValidateViewModel(controller, trainingtype);

			Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormViewBLM(controller._UnitOfWork).ConverTo_Default_TrainingTypeFormView(trainingtype);
            var result = controller.Edit(Default_TrainingTypeFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = trainingtypeBLO.Validate(trainingtype);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_TrainingType_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(TrainingType));
			 
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();
            TrainingType trainingtype = this.CreateOrLouadFirstTrainingType(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(trainingtype.Id) as ViewResult;
            var TrainingTypeDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(TrainingTypeDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(TrainingTypeDetailModelView, typeof(TrainingType));
        }

        [TestMethod()]
        public void Delete_TrainingType_Post_Test()
        {
            // Arrange
            //
            // Create TrainingType to Delete
            TrainingType trainingtype_to_delete = this.CreateValideTrainingTypeInstance();
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(new UnitOfWork());
            trainingtypeBLO.Save(trainingtype_to_delete);
            TrainingTypesController controller = new TrainingTypesController();

            // Acte
            var result = controller.DeleteConfirmed(trainingtype_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_TrainingType_Test()
        {
            // Arrange
            TrainingTypesController controller = new TrainingTypesController();

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

