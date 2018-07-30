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
    public class ClassroomsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ClassroomsControllerTests()
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
        /// Find the first Classroom instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public Classroom CreateOrLouadFirstClassroom(UnitOfWork unitOfWork)
        {
            ClassroomBLO classroomBLO = new ClassroomBLO(unitOfWork);
           
		   Classroom entity = null;
            if (classroomBLO.FindAll()?.Count > 0)
                entity = classroomBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Classroom for Test
                entity = this.CreateValideClassroomInstance();
                classroomBLO.Save(entity);
            }
            return entity;
        }

        private Classroom CreateValideClassroomInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Classroom  Valide_Classroom = this._Fixture.Create<Classroom>();
            Valide_Classroom.Id = 0;
            // Many to One 
            //
			// ClassroomCategory
			var ClassroomCategory = new ClassroomCategoriesControllerTests().CreateOrLouadFirstClassroomCategory(unitOfWork);
            Valide_Classroom.ClassroomCategory = null;
            Valide_Classroom.ClassroomCategoryId = ClassroomCategory.Id;
            // One to Many
            //
            return Valide_Classroom;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Classroom can't exist</returns>
        private Classroom CreateInValideClassroomInstance(UnitOfWork unitOfWork = null)
        {
            Classroom classroom = this.CreateValideClassroomInstance(unitOfWork);
             
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom(new UnitOfWork());
            
            return classroom;
        }


		  private Classroom CreateInValideClassroomInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Classroom classroom = this.CreateOrLouadFirstClassroom(unitOfWork);
             
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom(new UnitOfWork());
            
            return classroom;
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
            ClassroomsController ClassroomsController = new ClassroomsController();

            //Act
            ViewResult viewResult = ClassroomsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            ClassroomsController ClassroomsController = new ClassroomsController();

            ViewResult viewResult = ClassroomsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Classroom_Post_Test()
        {
            //--Arrange--
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = this.CreateValideClassroomInstance();

            //--Acte--
            //
            ClassroomsControllerTests.PreBindModel(controller, classroom, nameof(ClassroomsController.Create));
            ClassroomsControllerTests.ValidateViewModel(controller,classroom);
            var result = controller.Create(classroom);
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
        public void Create_InValide_Classroom_Post_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = this.CreateInValideClassroomInstance();
            if (classroom == null) return;
            ClassroomBLO classroomBLO = new ClassroomBLO(controller._UnitOfWork);

            // Acte
            ClassroomsControllerTests.PreBindModel(controller, classroom, nameof(ClassroomsController.Create));
            List<ValidationResult>  ls_validation_errors = ClassroomsControllerTests
                .ValidateViewModel(controller, classroom);
            var result = controller.Create(classroom);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomBLO.Validate(classroom);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }


       
        [TestMethod()]
        public void EditGet_Classroom_Not_Exist_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Classroom_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Classroom));
            
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom =  this.CreateOrLouadFirstClassroom(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(classroom.Id) as ViewResult;
            var ClassroomDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ClassroomDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ClassroomDetailModelView, typeof(Classroom));
        }

        [TestMethod()]
        public void Edit_Valide_Classroom_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Classroom));

            // Arrange
            ClassroomsController controller = new ClassroomsController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Classroom classroom = this.CreateOrLouadFirstClassroom(new UnitOfWork());
			 
       

            // Acte
            ClassroomsControllerTests.PreBindModel(controller, classroom, nameof(ClassroomsController.Edit));
            ClassroomsControllerTests.ValidateViewModel(controller, classroom);
            var result = controller.Edit(classroom);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Classroom_Post_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = this.CreateInValideClassroomInstance_ForEdit(new UnitOfWork());
            if (classroom == null) return;
            ClassroomBLO classroomBLO = new ClassroomBLO(controller._UnitOfWork);

            // Acte
            ClassroomsControllerTests.PreBindModel(controller, classroom, nameof(ClassroomsController.Edit));
            List<ValidationResult> ls_validation_errors = ClassroomsControllerTests
                .ValidateViewModel(controller, classroom);
            var result = controller.Edit(classroom);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = classroomBLO.Validate(classroom);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }

        [TestMethod()]
        public void Delete_Classroom_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Classroom));
			 
            // Arrange
            ClassroomsController controller = new ClassroomsController();
            Classroom classroom = this.CreateOrLouadFirstClassroom(controller._UnitOfWork);

            // Acte
            var result = controller.Delete(classroom.Id) as ViewResult;
            var ClassroomDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ClassroomDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ClassroomDetailModelView, typeof(Classroom));
        }

        [TestMethod()]
        public void Delete_Classroom_Post_Test()
        {
            // Arrange
            //
            // Create Classroom to Delete
            Classroom classroom_to_delete = this.CreateValideClassroomInstance();
            ClassroomBLO classroomBLO = new ClassroomBLO(new UnitOfWork());
            classroomBLO.Save(classroom_to_delete);
            ClassroomsController controller = new ClassroomsController();

            // Acte
            var result = controller.DeleteConfirmed(classroom_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Classroom_Test()
        {
            // Arrange
            ClassroomsController controller = new ClassroomsController();

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
        //    ClassroomsController controller = new ClassroomsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    ClassroomsController controller = new ClassroomsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

