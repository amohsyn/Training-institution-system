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
    public class AppRolesControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private AppRole Valide_AppRole;
        private AppRole Existant_AppRole_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private AppRole AppRole_to_Delete_On_CleanUP = null;

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
            Existant_AppRole_In_DB_Value =  this.CreateOrLouadFirstAppRole();
        }

        private AppRole CreateOrLouadFirstAppRole()
        {
            AppRoleBLO approleBLO = new AppRoleBLO(this.TestUnitOfWork);
            AppRole entity = approleBLO.FindAll()?.First();
            if (entity == null)
            {
                // Create Temp AppRole for Test
                entity = this.CreateValideAppRoleInstance();
                approleBLO.Save(entity);
                AppRole_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private AppRole CreateValideAppRoleInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            AppRole  Valide_AppRole = this._Fixture.Create<AppRole>();
            Valide_AppRole.Id = 0;
            // Many to One 
            //

            // One to Many
            //
             Valide_AppRole.AppControllerActions = null;
             Valide_AppRole.AppControllers = null;



            return Valide_AppRole;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide AppRole can't exist</returns>
        private AppRole CreateInValideAppRoleInstance()
        {
            AppRole approle = this.CreateValideAppRoleInstance();
             
			// Required   
 
			approle.Code = null;
            //Unique
            
            return approle;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(AppRole_to_Delete_On_CleanUP != null)
            {
                AppRoleBLO approleBLO = new AppRoleBLO(this.TestUnitOfWork);
                approleBLO.Delete(this.AppRole_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            AppRolesController AppRolesController = new AppRolesController();

            //Act
            ViewResult viewResult = AppRolesController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            AppRolesController AppRolesController = new AppRolesController();

            ViewResult viewResult = AppRolesController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_AppRole_Post_Test()
        {
            //--Arrange--
            AppRolesController controller = new AppRolesController();
            AppRole approle = this.CreateValideAppRoleInstance();

            //--Acte--
            //
            AppRolesControllerTests.PreBindModel(controller, approle, nameof(AppRolesController.Create));
            AppRolesControllerTests.ValidateViewModel(controller,approle);
            var result = controller.Create(approle);
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
        public void Create_InValide_AppRole_Post_Test()
        {
            // Arrange
            AppRolesController controller = new AppRolesController();
            AppRole approle = this.CreateInValideAppRoleInstance();
            if (approle == null) return;
            AppRoleBLO approleBLO = new AppRoleBLO(controller._UnitOfWork);

            // Acte
            AppRolesControllerTests.PreBindModel(controller, approle, nameof(AppRolesController.Create));
            List<ValidationResult>  ls_validation_errors = AppRolesControllerTests
                .ValidateViewModel(controller, approle);
            var result = controller.Create(approle);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = approleBLO.Validate(approle);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_AppRole_Not_Exist_Test()
        {
            // Arrange
            AppRolesController controller = new AppRolesController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_AppRole_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppRole));
            
            // Arrange
            AppRolesController controller = new AppRolesController();
            AppRole approle = this.Existant_AppRole_In_DB_Value;

            // Acte
            var result = controller.Edit(approle.Id) as ViewResult;
            var AppRoleDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AppRoleDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AppRoleDetailModelView, typeof(AppRole));
        }

        [TestMethod()]
        public void Edit_Valide_AppRole_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppRole));

            // Arrange
            AppRolesController controller = new AppRolesController();
           // controller.SetFakeControllerContext();
            
          
            AppRole approle = this.Existant_AppRole_In_DB_Value;


            // Acte
            AppRolesControllerTests.PreBindModel(controller, approle, nameof(AppRolesController.Edit));
            AppRolesControllerTests.ValidateViewModel(controller, approle);
            var result = controller.Edit(approle);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_AppRole_Post_Test()
        {
            // Arrange
            AppRolesController controller = new AppRolesController();
            AppRole approle = this.CreateInValideAppRoleInstance();
            if (approle == null) return;
            AppRoleBLO approleBLO = new AppRoleBLO(controller._UnitOfWork);

            // Acte
            AppRolesControllerTests.PreBindModel(controller, approle, nameof(AppRolesController.Create));
            List<ValidationResult> ls_validation_errors = AppRolesControllerTests
                .ValidateViewModel(controller, approle);
            var result = controller.Edit(approle);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = approleBLO.Validate(approle);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_AppRole_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(AppRole));

            // Arrange
            AppRolesController controller = new AppRolesController();
            AppRole approle = this.Existant_AppRole_In_DB_Value;

            // Acte
            var result = controller.Delete(approle.Id) as ViewResult;
            var AppRoleDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(AppRoleDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(AppRoleDetailModelView, typeof(AppRole));
        }

        [TestMethod()]
        public void Delete_AppRole_Post_Test()
        {
            // Arrange
            //
            // Create AppRole to Delete
            AppRole approle_to_delete = this.CreateValideAppRoleInstance();
            AppRoleBLO approleBLO = new AppRoleBLO(new UnitOfWork());
            approleBLO.Save(approle_to_delete);
            AppRolesController controller = new AppRolesController();

            // Acte
            var result = controller.DeleteConfirmed(approle_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_AppRole_Test()
        {
            // Arrange
            AppRolesController controller = new AppRolesController();

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
        //    AppRolesController controller = new AppRolesController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    AppRolesController controller = new AppRolesController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

