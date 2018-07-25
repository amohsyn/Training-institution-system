using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class TraineesControllerTests
    {
        // User Interaction Logic testing

        // Testing for ViewResult
        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            TraineesController traineesController = new TraineesController();

            //Act
            ViewResult viewResult = traineesController.Index() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));
        }


        [TestMethod()]
        public void CreateGetTest()
        {

            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePostTest()
        {
            // Testing for Redirection
            Assert.Fail();
        }


        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EditGetTest()
        {
            
            TraineesController controller = new TraineesController();

            // Edit not exist entity
            var result = controller.Details(-1) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Trainees", result.RouteValues["controller"]);
        }

        [TestMethod()]
        public void DetailsTest()
        {

            TraineesController controller = new TraineesController();

            // Edit not exist entity
            var result = controller.Details(-1) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Trainees", result.RouteValues["controller"]);
        }





        [TestMethod()]
        public void ImportTest()
        {
            Assert.Fail();
        }
    }
}