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
    public class GroupsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private Group Valide_Group;
        private Group Existant_Group_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private Group Group_to_Delete_On_CleanUP = null;

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
            Existant_Group_In_DB_Value =  this.CreateOrLouadFirstGroup();
        }

        private Group CreateOrLouadFirstGroup()
        {
            GroupBLO groupBLO = new GroupBLO(this.TestUnitOfWork);
            Group entity = groupBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp Group for Test
                entity = this.CreateValideGroupInstance();
                groupBLO.Save(entity);
                Group_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private Group CreateValideGroupInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Group  Valide_Group = this._Fixture.Create<Group>();
            Valide_Group.Id = 0;
            // Many to One 
            //

            // Specialty
            var Specialty = new SpecialtyBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_Group.Specialty = null;
            Valide_Group.SpecialtyId = (Specialty == null) ? 0 : Specialty.Id;
            // TrainingType
            var TrainingType = new TrainingTypeBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_Group.TrainingType = null;
            Valide_Group.TrainingTypeId = (TrainingType == null) ? 0 : TrainingType.Id;
            // TrainingYear
            var TrainingYear = new TrainingYearBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_Group.TrainingYear = null;
            Valide_Group.TrainingYearId = (TrainingYear == null) ? 0 : TrainingYear.Id;
            // YearStudy
            var YearStudy = new YearStudyBLO(unitOfWork).FindAll().FirstOrDefault();
            Valide_Group.YearStudy = null;
            Valide_Group.YearStudyId = (YearStudy == null) ? 0 : YearStudy.Id;
            // One to Many
            //



            return Valide_Group;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Group can't exist</returns>
        private Group CreateInValideGroupInstance()
        {
            Group group = this.CreateValideGroupInstance();
             
			// Required   
 
			group.TrainingTypeId = 0;
 
			group.TrainingYearId = 0;
 
			group.SpecialtyId = 0;
 
			group.YearStudyId = 0;
 
			group.Code = null;
            //Unique
            
            return group;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(Group_to_Delete_On_CleanUP != null)
            {
                GroupBLO groupBLO = new GroupBLO(this.TestUnitOfWork);
                groupBLO.Delete(this.Group_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            GroupsController GroupsController = new GroupsController();

            //Act
            ViewResult viewResult = GroupsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            GroupsController GroupsController = new GroupsController();

            ViewResult viewResult = GroupsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Group_Post_Test()
        {
            //--Arrange--
            GroupsController controller = new GroupsController();
            Group group = this.CreateValideGroupInstance();

            //--Acte--
            //
            GroupsControllerTests.PreBindModel(controller, group, nameof(GroupsController.Create));
            GroupsControllerTests.ValidateViewModel(controller,group);
            var result = controller.Create(group);
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
        public void Create_InValide_Group_Post_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();
            Group group = this.CreateInValideGroupInstance();
            if (group == null) return;
            GroupBLO groupBLO = new GroupBLO(controller._UnitOfWork);

            // Acte
            GroupsControllerTests.PreBindModel(controller, group, nameof(GroupsController.Create));
            List<ValidationResult>  ls_validation_errors = GroupsControllerTests
                .ValidateViewModel(controller, group);
            var result = controller.Create(group);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = groupBLO.Validate(group);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_Group_Not_Exist_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Group_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Group));
            
            // Arrange
            GroupsController controller = new GroupsController();
            Group group = this.Existant_Group_In_DB_Value;

            // Acte
            var result = controller.Edit(group.Id) as ViewResult;
            var GroupDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(GroupDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(GroupDetailModelView, typeof(Group));
        }

        [TestMethod()]
        public void Edit_Valide_Group_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Group));

            // Arrange
            GroupsController controller = new GroupsController();
           // controller.SetFakeControllerContext();
            
          
            Group group = this.Existant_Group_In_DB_Value;


            // Acte
            GroupsControllerTests.PreBindModel(controller, group, nameof(GroupsController.Edit));
            GroupsControllerTests.ValidateViewModel(controller, group);
            var result = controller.Edit(group);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Group_Post_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();
            Group group = this.CreateInValideGroupInstance();
            if (group == null) return;
            GroupBLO groupBLO = new GroupBLO(controller._UnitOfWork);

            // Acte
            GroupsControllerTests.PreBindModel(controller, group, nameof(GroupsController.Create));
            List<ValidationResult> ls_validation_errors = GroupsControllerTests
                .ValidateViewModel(controller, group);
            var result = controller.Edit(group);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = groupBLO.Validate(group);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_Group_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Group));

            // Arrange
            GroupsController controller = new GroupsController();
            Group group = this.Existant_Group_In_DB_Value;

            // Acte
            var result = controller.Delete(group.Id) as ViewResult;
            var GroupDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(GroupDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(GroupDetailModelView, typeof(Group));
        }

        [TestMethod()]
        public void Delete_Group_Post_Test()
        {
            // Arrange
            //
            // Create Group to Delete
            Group group_to_delete = this.CreateValideGroupInstance();
            GroupBLO groupBLO = new GroupBLO(new UnitOfWork());
            groupBLO.Save(group_to_delete);
            GroupsController controller = new GroupsController();

            // Acte
            var result = controller.DeleteConfirmed(group_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Group_Test()
        {
            // Arrange
            GroupsController controller = new GroupsController();

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
        //    GroupsController controller = new GroupsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    GroupsController controller = new GroupsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

