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
    public class Edit_SeanceNumbersControllerTests : ManagerControllerTests
    {
		SeanceNumbersControllerTests_Service TestService = new SeanceNumbersControllerTests_Service();

		[TestMethod()]
        public void EditGet_SeanceNumber_Not_Exist_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_SeanceNumber_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber =  TestService.CreateOrLouadFirstSeanceNumber(controller._UnitOfWork);

            // Acte
            var result = controller.Edit(seancenumber.Id) as ViewResult;
            var SeanceNumberDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(SeanceNumberDetailModelView, typeof(Default_SeanceNumberFormView));
        }

        [TestMethod()]
        public void Edit_Valide_SeanceNumber_Post_Test()
        {
            // Init 
            ModelViewMetaData modelViewMetaData = new ModelViewMetaData(typeof(SeanceNumber));

            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            SeanceNumber seancenumber = TestService.CreateOrLouadFirstSeanceNumber(new UnitOfWork());
			 
       

            // Acte
            SeanceNumbersControllerTests_Service.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Edit));
            SeanceNumbersControllerTests_Service.ValidateViewModel(controller, seancenumber);

			Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceNumberFormView(seancenumber);
            var result = controller.Edit(Default_SeanceNumberFormView);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_SeanceNumber_Post_Test()
        {
            // Arrange
            SeanceNumbersController controller = new SeanceNumbersController();
            SeanceNumber seancenumber = TestService.CreateInValideSeanceNumberInstance_ForEdit(new UnitOfWork());
            if (seancenumber == null) return;
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(controller._UnitOfWork);

            // Acte
            SeanceNumbersControllerTests_Service.PreBindModel(controller, seancenumber, nameof(SeanceNumbersController.Edit));
            List<ValidationResult> ls_validation_errors = SeanceNumbersControllerTests_Service
                .ValidateViewModel(controller, seancenumber);

			Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormViewBLM(controller._UnitOfWork).ConverTo_Default_SeanceNumberFormView(seancenumber);
            var result = controller.Edit(Default_SeanceNumberFormView);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = seancenumberBLO.Validate(seancenumber);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == Enums.Enums.NotificationType.warning);
        }
    }
}

