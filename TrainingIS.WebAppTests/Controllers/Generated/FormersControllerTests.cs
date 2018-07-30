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
    public class FormersControllerTests : ManagerControllerTests
    {
        private Fixture _Fixture = null;
        private Former Valide_Former;
        private Former Existant_Former_In_DB_Value;
        private UnitOfWork TestUnitOfWork = null;
        private Former Former_to_Delete_On_CleanUP = null;

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
            Existant_Former_In_DB_Value =  this.CreateOrLouadFirstFormer();
        }

        private Former CreateOrLouadFirstFormer()
        {
            FormerBLO formerBLO = new FormerBLO(this.TestUnitOfWork);
           
		   Former entity = null;
            if (formerBLO.FindAll()?.Count > 0)
                entity = formerBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp Former for Test
                entity = this.CreateValideFormerInstance();
                formerBLO.Save(entity);
                Former_to_Delete_On_CleanUP = entity;
            }
            return entity;
        }

        private Former CreateValideFormerInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Former  Valide_Former = this._Fixture.Create<Former>();
            Valide_Former.Id = 0;
            // Many to One 
            //

            // One to Many
            //



            return Valide_Former;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Former can't exist</returns>
        private Former CreateInValideFormerInstance()
        {
            Former former = this.CreateValideFormerInstance();
             
			// Required   
 
			former.FirstName = null;
 
			former.LastName = null;
 
			former.Sex = false;
 
			former.Email = null;
 
			former.RegistrationNumber = null;
            //Unique
            
            return former;
        }
        #endregion

        #region TestCleanup
        [TestCleanup]
        public void Clean_UP_Test()
        {
            if(Former_to_Delete_On_CleanUP != null)
            {
                FormerBLO formerBLO = new FormerBLO(this.TestUnitOfWork);
                formerBLO.Delete(this.Former_to_Delete_On_CleanUP);
            }

        }
        #endregion

        [TestMethod()]
        public void Index_ViewNotNull_ViewBag_Test()
        {
            //Arrange
            FormersController FormersController = new FormersController();

            //Act
            ViewResult viewResult = FormersController.Index() as ViewResult;

            //Asert 
            Assert.IsNotNull(viewResult.ViewName);
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }

        [TestMethod()]
        public void Create_ViewResult_ViewBag_Get_Test()
        {
            //Arrange
            FormersController FormersController = new FormersController();

            ViewResult viewResult = FormersController.Create() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Create_Title"]));
        }

        [TestMethod()]
        public void Create_Valide_Former_Post_Test()
        {
            //--Arrange--
            FormersController controller = new FormersController();
            Former former = this.CreateValideFormerInstance();

            //--Acte--
            //
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Create));
            FormersControllerTests.ValidateViewModel(controller,former);
            var result = controller.Create(former);
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
        public void Create_InValide_Former_Post_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former = this.CreateInValideFormerInstance();
            if (former == null) return;
            FormerBLO formerBLO = new FormerBLO(controller._UnitOfWork);

            // Acte
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Create));
            List<ValidationResult>  ls_validation_errors = FormersControllerTests
                .ValidateViewModel(controller, former);
            var result = controller.Create(former);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = formerBLO.Validate(former);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null)? 0: GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }


       
        [TestMethod()]
        public void EditGet_Former_Not_Exist_Test()
        {
            // Arrange
            FormersController controller = new FormersController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Former_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Former));
            
            // Arrange
            FormersController controller = new FormersController();
            Former former = this.Existant_Former_In_DB_Value;

            // Acte
            var result = controller.Edit(former.Id) as ViewResult;
            var FormerDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.EditViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(FormerDetailModelView, modelViewMetaData.EditViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(FormerDetailModelView, typeof(Former));
        }

        [TestMethod()]
        public void Edit_Valide_Former_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Former));

            // Arrange
            FormersController controller = new FormersController();
           // controller.SetFakeControllerContext();
            
          
            Former former = this.Existant_Former_In_DB_Value;


            // Acte
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Edit));
            FormersControllerTests.ValidateViewModel(controller, former);
            var result = controller.Edit(former);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Former_Post_Test()
        {
            // Arrange
            FormersController controller = new FormersController();
            Former former = this.CreateInValideFormerInstance();
            if (former == null) return;
            FormerBLO formerBLO = new FormerBLO(controller._UnitOfWork);

            // Acte
            FormersControllerTests.PreBindModel(controller, former, nameof(FormersController.Create));
            List<ValidationResult> ls_validation_errors = FormersControllerTests
                .ValidateViewModel(controller, former);
            var result = controller.Edit(former);
            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = formerBLO.Validate(former);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }

        [TestMethod()]
        public void Delete_Former_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(Former));

            // Arrange
            FormersController controller = new FormersController();
            Former former = this.Existant_Former_In_DB_Value;

            // Acte
            var result = controller.Delete(former.Id) as ViewResult;
            var FormerDetailModelView = result.Model;

            // Assert 
            if (modelViewMetaData.DetailsViewAttribute?.TypeOfView != null)
                Assert.IsInstanceOfType(FormerDetailModelView, modelViewMetaData.DetailsViewAttribute?.TypeOfView);
            else
                Assert.IsInstanceOfType(FormerDetailModelView, typeof(Former));
        }

        [TestMethod()]
        public void Delete_Former_Post_Test()
        {
            // Arrange
            //
            // Create Former to Delete
            Former former_to_delete = this.CreateValideFormerInstance();
            FormerBLO formerBLO = new FormerBLO(new UnitOfWork());
            formerBLO.Save(former_to_delete);
            FormersController controller = new FormersController();

            // Acte
            var result = controller.DeleteConfirmed(former_to_delete.Id);
            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }
        [TestMethod()]
        public void Delete_Existtant_Former_Test()
        {
            // Arrange
            FormersController controller = new FormersController();

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
        //    FormersController controller = new FormersController();

            // Acte
         //   FileResult result = controller.Export();


            // Assert
        //}

        //[TestMethod()]
        //public void ImporttTest()
        //{
        //    // Arrange
        //    FormersController controller = new FormersController();

        //    // Acte
        //    // FileResult result = controller.Import();

        //    Assert.Fail();
        //    // Assert
        //}
    }
}

