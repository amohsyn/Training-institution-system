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
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using GApp.Entities;
using GApp.BLL.Enums;
using GApp.BLL.VO;
using GApp.DAL;
using TrainingIS.WebApp.Tests.Services;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Metiers_Edit_ControllerTests : ManagerControllerTests
    {
		MetiersControllerTests_Service TestService = new MetiersControllerTests_Service();

		[TestMethod()]
        public void EditGet_Metier_Not_Exist_Test()
        {
            // Arrange
            MetiersController controller = new MetiersController();

            // Acte
            var result = controller.Edit(-1) as RedirectToRouteResult;

            // Assert 
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.error);
        }
        [TestMethod()]
        public void EditGet_Metier_Test()
        {
            // Arrange
            MetiersController controller = new MetiersController();
            Metier metier =  TestService.CreateOrLouadFirstMetier(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            var result = controller.Edit(metier.Id) as ViewResult;
            var MetierDetailModelView = result.Model;

            // Assert 
			Assert.IsInstanceOfType(MetierDetailModelView, typeof(Default_Form_Metier_Model));
        }

        [TestMethod()]
        public void Edit_Valide_Metier_Post_Test()
        {

            // Arrange
            MetiersController controller = new MetiersController();
			// controller.SetFakeControllerContext();
            
			// Load existant entity in new Work, to be detached from the the controller work
            Metier metier = TestService.CreateOrLouadFirstMetier(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
			 
       

            // Acte
            MetiersControllerTests_Service.PreBindModel(controller, metier, nameof(MetiersController.Edit));
            MetiersControllerTests_Service.ValidateViewModel(controller, metier);

			Default_Form_Metier_Model Default_Form_Metier_Model = new Default_Form_Metier_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Metier_Model(metier);
            var result = controller.Edit(Default_Form_Metier_Model);



            RedirectToRouteResult redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(controller.TempData.ContainsKey("notification"));
            var notification = controller.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.success);
        }

        [TestMethod()]
        public void Edit_InValide_Metier_Post_Test()
        {
            // Arrange
            MetiersController controller = new MetiersController();
            Metier metier = TestService.CreateInValideMetierInstance_ForEdit(new UnitOfWork<TrainingISModel>(),controller.GAppContext);
            if (metier == null) return;
            MetierBLO metierBLO = new MetierBLO(controller._UnitOfWork, controller.GAppContext) ;

            // Acte
            MetiersControllerTests_Service.PreBindModel(controller, metier, nameof(MetiersController.Edit));
            List<ValidationResult> ls_validation_errors = MetiersControllerTests_Service
                .ValidateViewModel(controller, metier);
			
			// stop test if the InValide entity is valide
            if (ls_validation_errors.Count == 0) return;

			Default_Form_Metier_Model Default_Form_Metier_Model = new Default_Form_Metier_ModelBLM(controller._UnitOfWork, controller.GAppContext) .ConverTo_Default_Form_Metier_Model(metier);
            var result = controller.Edit(Default_Form_Metier_Model);
 

            ViewResult resultViewResult = result as ViewResult;
            var GAppErrors = metierBLO.Validate(metier);
            int Exprected_Errors_Number = ls_validation_errors.Count + ((GAppErrors == null) ? 0 : GAppErrors.Count);

            // Assert 
            Assert.AreEqual(Exprected_Errors_Number, controller.ModelState.Count);
            Assert.IsTrue(resultViewResult.TempData.ContainsKey("notification"));
            var notification = resultViewResult.TempData["notification"] as AlertMessage;
            Assert.IsTrue(notification.notificationType == NotificationType.warning);
        }
    }
}

