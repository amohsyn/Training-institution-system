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
    public class SchoollevelsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SchoollevelsControllerTests()
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
        /// Find the first Schoollevel instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Schoollevel CreateOrLouadFirstSchoollevel(UnitOfWork unitOfWork)
        {
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(unitOfWork);
           
		   Schoollevel entity = null;
            if (schoollevelBLO.FindAll()?.Count > 0)
                entity = schoollevelBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Schoollevel for Test
                entity = this.CreateValideSchoollevelInstance();
                schoollevelBLO.Save(entity);
            }
            return entity;
        }

        private Schoollevel CreateValideSchoollevelInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Schoollevel  Valide_Schoollevel = this._Fixture.Create<Schoollevel>();
            Valide_Schoollevel.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Schoollevel;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Schoollevel can't exist</returns>
        private Schoollevel CreateInValideSchoollevelInstance(UnitOfWork unitOfWork = null)
        {
            Schoollevel schoollevel = this.CreateValideSchoollevelInstance(unitOfWork);
             
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel(new UnitOfWork());
            
            return schoollevel;
        }


		  private Schoollevel CreateInValideSchoollevelInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Schoollevel schoollevel = this.CreateOrLouadFirstSchoollevel(unitOfWork);
             
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel(new UnitOfWork());
            
            return schoollevel;
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
            SchoollevelsController SchoollevelsController = new SchoollevelsController();

            //Act
            ViewResult viewResult = SchoollevelsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            SchoollevelsController SchoollevelsController = new SchoollevelsController();

            ViewResult viewResult = SchoollevelsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Schoollevel_Post_Test()
        {
            //--Arrange--
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = this.CreateValideSchoollevelInstance();

            //--Acte--
            //
            SchoollevelsControllerTests.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Create));
            SchoollevelsControllerTests.ValidateViewModel(controller,schoollevel);

			Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormViewBLM(controller._UnitOfWork).ConverTo_Default_SchoollevelFormView(schoollevel);
            var result = controller.Create(Default_SchoollevelFormView);
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
        public void Create_InValide_Schoollevel_Post_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = this.CreateInValideSchoollevelInstance();
            if (schoollevel == null) return;
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(controller._UnitOfWork);

            // Acte
            SchoollevelsControllerTests.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Create));
            List<ValidationResult>  ls_validation_errors = SchoollevelsControllerTests
                .ValidateViewModel(controller, schoollevel);

			Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormViewBLM(controller._UnitOfWork).ConverTo_Default_SchoollevelFormView(schoollevel);
            var result = controller.Create(Default_SchoollevelFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = schoollevelBLO.Validate(schoollevel);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_Schoollevel_Not_Exist_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Schoollevel_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Schoollevel));
            
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel =  this.CreateOrLouadFirstSchoollevel(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(schoollevel.Id) as ViewResult;
            var SchoollevelDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SchoollevelDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SchoollevelDetailModelView, typeof(Schoollevel));
        }

        [TestMethod()]
        public void Edit_Valide_Schoollevel_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Schoollevel));

            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Schoollevel schoollevel = this.CreateOrLouadFirstSchoollevel(new UnitOfWork());
			 
       

            // Acte
            SchoollevelsControllerTests.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Edit));
            SchoollevelsControllerTests.ValidateViewModel(controller, schoollevel);

			Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormViewBLM(controller._UnitOfWork).ConverTo_Default_SchoollevelFormView(schoollevel);
            var result = controller.Edit(Default_SchoollevelFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Schoollevel_Post_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = this.CreateInValideSchoollevelInstance_ForEdit(new UnitOfWork());
            if (schoollevel == null) return;
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(controller._UnitOfWork);

            // Acte
            SchoollevelsControllerTests.PreBindModel(controller, schoollevel, nameof(SchoollevelsController.Edit));
            List<ValidationResult> ls_validation_errors = SchoollevelsControllerTests
                .ValidateViewModel(controller, schoollevel);

			Default_SchoollevelFormView Default_SchoollevelFormView = new Default_SchoollevelFormViewBLM(controller._UnitOfWork).ConverTo_Default_SchoollevelFormView(schoollevel);
            var result = controller.Edit(Default_SchoollevelFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = schoollevelBLO.Validate(schoollevel);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_Schoollevel_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Schoollevel));
			 
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();
            Schoollevel schoollevel = this.CreateOrLouadFirstSchoollevel(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(schoollevel.Id) as ViewResult;
            var SchoollevelDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(SchoollevelDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(SchoollevelDetailModelView, typeof(Schoollevel));
        }

        [TestMethod()]
        public void Delete_Schoollevel_Post_Test()
        {
            // Arrange
            //
            // Create Schoollevel to Delete
            Schoollevel schoollevel_to_delete = this.CreateValideSchoollevelInstance();
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(new UnitOfWork());
            schoollevelBLO.Save(schoollevel_to_delete);
            SchoollevelsController controller = new SchoollevelsController();

            // Acte
            var result = controller.DeleteConfirmed(schoollevel_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Schoollevel_Test()
        {
            // Arrange
            SchoollevelsController controller = new SchoollevelsController();

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

