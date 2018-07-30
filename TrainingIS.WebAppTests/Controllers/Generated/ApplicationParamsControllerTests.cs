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
    public class ApplicationParamsControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private ApplicationParam Valide_ApplicationParam;
        private ApplicationParam Existant_ApplicationParam_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private ApplicationParam ApplicationParam_to_Delete_On_CleanUP = null;

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
            Existant_ApplicationParam_In_DB_Value =  this.CreateOrLouadFirstApplicationParam();
        }

        private ApplicationParam CreateOrLouadFirstApplicationParam()
        {
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(this.TestUnitOfWork);
           
		   ApplicationParam entity = null;
            if (applicationparamBLO.FindAll()?.Count > 0)
                entity = applicationparamBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp ApplicationParam for Test
                entity = this.CreateValideApplicationParamInstance();
                applicationparamBLO.Save(entity);
                ApplicationParam_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private ApplicationParam CreateValideApplicationParamInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            ApplicationParam  Valide_ApplicationParam = this._Fixture.Create<ApplicationParam>();
            Valide_ApplicationParam.Id = 0;
            // Many to One 
            //

            // One to Many
            //



            return Valide_ApplicationParam;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ApplicationParam can't exist</returns>
        private ApplicationParam CreateInValideApplicationParamInstance()
        {
            ApplicationParam applicationparam = this.CreateValideApplicationParamInstance();
             
			// Required   
 
			applicationparam.Code = null;
            //Unique
            
            return applicationparam;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(ApplicationParam_to_Delete_On_CleanUP != null)
            {
                ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(this.TestUnitOfWork);
                applicationparamBLO.Delete(this.ApplicationParam_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            ApplicationParamsController ApplicationParamsController = new ApplicationParamsController();

            //Act
            ViewResult viewResult = ApplicationParamsController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            ApplicationParamsController ApplicationParamsController = new ApplicationParamsController();

            ViewResult viewResult = ApplicationParamsController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_ApplicationParam_Post_Test()
        {
            //--Arrange--
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = this.CreateValideApplicationParamInstance();

            //--Acte--
            //
            ApplicationParamsControllerTests.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Create));
            ApplicationParamsControllerTests.ValidateViewModel(controller,applicationparam);
            var result = controller.Create(applicationparam);
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
        public void Create_InValide_ApplicationParam_Post_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = this.CreateInValideApplicationParamInstance();
            if (applicationparam == null) return;
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(controller._UnitOfWork);

            // Acte
            ApplicationParamsControllerTests.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Create));
            List<ValidationResult>  ls_validation_errors = ApplicationParamsControllerTests
                .ValidateViewModel(controller, applicationparam);
            var result = controller.Create(applicationparam);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = applicationparamBLO.Validate(applicationparam);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_ApplicationParam_Not_Exist_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_ApplicationParam_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ApplicationParam));
            
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = this.Existant_ApplicationParam_In_DB_Value;

            // Acte
            var result = controller.Edit(applicationparam.Id) as ViewResult;
            var ApplicationParamDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ApplicationParamDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ApplicationParamDetailModelView, typeof(ApplicationParam));
        }

        [TestMethod()]
        public void Edit_Valide_ApplicationParam_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ApplicationParam));

            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
           // controller.SetFakeControllerContext();
            
          
            ApplicationParam applicationparam = this.Existant_ApplicationParam_In_DB_Value;


            // Acte
            ApplicationParamsControllerTests.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Edit));
            ApplicationParamsControllerTests.ValidateViewModel(controller, applicationparam);
            var result = controller.Edit(applicationparam);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_ApplicationParam_Post_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = this.CreateInValideApplicationParamInstance();
            if (applicationparam == null) return;
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(controller._UnitOfWork);

            // Acte
            ApplicationParamsControllerTests.PreBindModel(controller, applicationparam, nameof(ApplicationParamsController.Create));
            List<ValidationResult> ls_validation_errors = ApplicationParamsControllerTests
                .ValidateViewModel(controller, applicationparam);
            var result = controller.Edit(applicationparam);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = applicationparamBLO.Validate(applicationparam);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_ApplicationParam_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(ApplicationParam));

            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();
            ApplicationParam applicationparam = this.Existant_ApplicationParam_In_DB_Value;

            // Acte
            var result = controller.Delete(applicationparam.Id) as ViewResult;
            var ApplicationParamDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(ApplicationParamDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(ApplicationParamDetailModelView, typeof(ApplicationParam));
        }

        [TestMethod()]
        public void Delete_ApplicationParam_Post_Test()
        {
            // Arrange
            //
            // Create ApplicationParam to Delete
            ApplicationParam applicationparam_to_delete = this.CreateValideApplicationParamInstance();
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(new UnitOfWork());
            applicationparamBLO.Save(applicationparam_to_delete);
            ApplicationParamsController controller = new ApplicationParamsController();

            // Acte
            var result = controller.DeleteConfirmed(applicationparam_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_ApplicationParam_Test()
        {
            // Arrange
            ApplicationParamsController controller = new ApplicationParamsController();

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
        //    ApplicationParamsController controller = new ApplicationParamsController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    ApplicationParamsController controller = new ApplicationParamsController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

