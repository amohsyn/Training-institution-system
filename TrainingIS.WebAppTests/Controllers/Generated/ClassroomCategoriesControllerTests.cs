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
    public class ClassroomCategoriesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ClassroomCategoriesControllerTests()
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
        /// Find the first ClassroomCategory instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public ClassroomCategory CreateOrLouadFirstClassroomCategory(UnitOfWork unitOfWork)
        {
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(unitOfWork);
           
		   ClassroomCategory entity = null;
            if (classroomcategoryBLO.FindAll()?.Count > 0)
                entity = classroomcategoryBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ClassroomCategory for Test
                entity = this.CreateValideClassroomCategoryInstance();
                classroomcategoryBLO.Save(entity);
            }
            return entity;
        }

        private ClassroomCategory CreateValideClassroomCategoryInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            ClassroomCategory  Valide_ClassroomCategory = this._Fixture.Create<ClassroomCategory>();
            Valide_ClassroomCategory.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_ClassroomCategory;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ClassroomCategory can't exist</returns>
        private ClassroomCategory CreateInValideClassroomCategoryInstance(UnitOfWork unitOfWork = null)
        {
            ClassroomCategory classroomcategory = this.CreateValideClassroomCategoryInstance(unitOfWork);
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory(new UnitOfWork());
            
            return classroomcategory;
        }


		  private ClassroomCategory CreateInValideClassroomCategoryInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            ClassroomCategory classroomcategory = this.CreateOrLouadFirstClassroomCategory(unitOfWork);
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
			var existant_ClassroomCategory = this.CreateOrLouadFirstClassroomCategory(new UnitOfWork());
            
            return classroomcategory;
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
            ClassroomCategoriesController ClassroomCategoriesController = new ClassroomCategoriesController();

            //Act
            ViewResult viewResult = ClassroomCategoriesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

		 [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            ClassroomCategoriesController ClassroomCategoriesController = new ClassroomCategoriesController();

            ViewResult viewResult = ClassroomCategoriesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ClassroomCategory_Post_Test()
        {
            //--Arrange--
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = this.CreateValideClassroomCategoryInstance();

            //--Acte--
            //
            ClassroomCategoriesControllerTests.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Create));
            ClassroomCategoriesControllerTests.ValidateViewModel(controller,classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Create(Default_ClassroomCategoryFormView);
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
        public void Create_InValide_ClassroomCategory_Post_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = this.CreateInValideClassroomCategoryInstance();
            if (classroomcategory == null) return;
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(controller._UnitOfWork);

            // Acte
            ClassroomCategoriesControllerTests.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Create));
            List<ValidationResult>  ls_validation_errors = ClassroomCategoriesControllerTests
                .ValidateViewModel(controller, classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Create(Default_ClassroomCategoryFormView);

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomcategoryBLO.Validate(classroomcategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


  [TestMethod()]
        public void EditGet_ClassroomCategory_Not_Exist_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ClassroomCategory_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ClassroomCategory));
            
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory =  this.CreateOrLouadFirstClassroomCategory(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(classroomcategory.Id) as ViewResult;
            var ClassroomCategoryDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ClassroomCategoryDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ClassroomCategoryDetailModelView, typeof(ClassroomCategory));
        }

        [TestMethod()]
        public void Edit_Valide_ClassroomCategory_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ClassroomCategory));

            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            ClassroomCategory classroomcategory = this.CreateOrLouadFirstClassroomCategory(new UnitOfWork());
			 
       

            // Acte
            ClassroomCategoriesControllerTests.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Edit));
            ClassroomCategoriesControllerTests.ValidateViewModel(controller, classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Edit(Default_ClassroomCategoryFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ClassroomCategory_Post_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = this.CreateInValideClassroomCategoryInstance_ForEdit(new UnitOfWork());
            if (classroomcategory == null) return;
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(controller._UnitOfWork);

            // Acte
            ClassroomCategoriesControllerTests.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Edit));
            List<ValidationResult> ls_validation_errors = ClassroomCategoriesControllerTests
                .ValidateViewModel(controller, classroomcategory);

			Default_ClassroomCategoryFormView Default_ClassroomCategoryFormView = new Default_ClassroomCategoryFormViewBLM(controller._UnitOfWork).ConverTo_Default_ClassroomCategoryFormView(classroomcategory);
            var result = controller.Edit(Default_ClassroomCategoryFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomcategoryBLO.Validate(classroomcategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

		 [TestMethod()]
        public void Delete_ClassroomCategory_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ClassroomCategory));
			 
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = this.CreateOrLouadFirstClassroomCategory(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(classroomcategory.Id) as ViewResult;
            var ClassroomCategoryDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ClassroomCategoryDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ClassroomCategoryDetailModelView, typeof(ClassroomCategory));
        }

        [TestMethod()]
        public void Delete_ClassroomCategory_Post_Test()
        {
            // Arrange
            //
            // Create ClassroomCategory to Delete
            ClassroomCategory classroomcategory_to_delete = this.CreateValideClassroomCategoryInstance();
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(new UnitOfWork());
            classroomcategoryBLO.Save(classroomcategory_to_delete);
            ClassroomCategoriesController controller = new ClassroomCategoriesController();

            // Acte
            var result = controller.DeleteConfirmed(classroomcategory_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ClassroomCategory_Test()
        {
            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();

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

