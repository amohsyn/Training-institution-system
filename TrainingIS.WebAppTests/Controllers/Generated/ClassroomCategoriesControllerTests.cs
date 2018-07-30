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
    public class ClassroomCategoriesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private ClassroomCategory Valide_ClassroomCategory;
        private ClassroomCategory Existant_ClassroomCategory_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private ClassroomCategory ClassroomCategory_to_Delete_On_CleanUP = null;

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
            Existant_ClassroomCategory_In_DB_Value =  this.CreateOrLouadFirstClassroomCategory();
        }

        private ClassroomCategory CreateOrLouadFirstClassroomCategory()
        {
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(this.TestUnitOfWork);
            ClassroomCategory entity = classroomcategoryBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp ClassroomCategory for Test
                entity = this.CreateValideClassroomCategoryInstance();
                classroomcategoryBLO.Save(entity);
                ClassroomCategory_to_Delete_On_CleanUP = entity;
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
        private ClassroomCategory CreateInValideClassroomCategoryInstance()
        {
            ClassroomCategory classroomcategory = this.CreateValideClassroomCategoryInstance();
             
			// Required   
 
			classroomcategory.Code = null;
            //Unique
            
            return classroomcategory;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(ClassroomCategory_to_Delete_On_CleanUP != null)
            {
                ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(this.TestUnitOfWork);
                classroomcategoryBLO.Delete(this.ClassroomCategory_to_Delete_On_CleanUP);
            }

        }
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
            var result = controller.Create(classroomcategory);
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
            var result = controller.Create(classroomcategory);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomcategoryBLO.Validate(classroomcategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
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
            ClassroomCategory classroomcategory = this.Existant_ClassroomCategory_In_DB_Value;

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
            
          
            ClassroomCategory classroomcategory = this.Existant_ClassroomCategory_In_DB_Value;


            // Acte
            ClassroomCategoriesControllerTests.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Edit));
            ClassroomCategoriesControllerTests.ValidateViewModel(controller, classroomcategory);
            var result = controller.Edit(classroomcategory);
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
            ClassroomCategory classroomcategory = this.CreateInValideClassroomCategoryInstance();
            if (classroomcategory == null) return;
            ClassroomCategoryBLO classroomcategoryBLO = new ClassroomCategoryBLO(controller._UnitOfWork);

            // Acte
            ClassroomCategoriesControllerTests.PreBindModel(controller, classroomcategory, nameof(ClassroomCategoriesController.Create));
            List<ValidationResult> ls_validation_errors = ClassroomCategoriesControllerTests
                .ValidateViewModel(controller, classroomcategory);
            var result = controller.Edit(classroomcategory);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomcategoryBLO.Validate(classroomcategory);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_ClassroomCategory_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ClassroomCategory));

            // Arrange
            ClassroomCategoriesController controller = new ClassroomCategoriesController();
            ClassroomCategory classroomcategory = this.Existant_ClassroomCategory_In_DB_Value;

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


        //[TestMethod()]
       // public void ExportTest()
        //{
            // Arrange
        //    ClassroomCategoriesController controller = new ClassroomCategoriesController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    ClassroomCategoriesController controller = new ClassroomCategoriesController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

