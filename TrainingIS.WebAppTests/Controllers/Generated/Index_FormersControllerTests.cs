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
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.BLL.ModelsViews;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TrainingIS.WebApp.Controllers.Tests
{
    [TestClass()]
    public class Index_FormersControllerTests : ManagerControllerTests
    {
        private static IWebDriver _driverChrome;

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            _driverChrome = new ChromeDriver();
        }

        [TestMethod()]
        public void Former_Index_Controller_Test()
        {
            //Arrange
            FormersController FormersController = new FormersController();

            ViewResult viewResult = FormersController.Index() as ViewResult;

            //Asert ViewResult
            Assert.IsNotNull(viewResult.ViewName);

            // Asert ViewData/ ViewBag
            Assert.IsTrue(!string.IsNullOrEmpty(viewResult.ViewBag.msg["Index_Title"]));

        }

        [TestMethod()]
        public void Former_Index_Test()
        {
            //Arrange
            IWebDriver webDriver = new ChromeDriver();
            var url = "http://localhost:60901/Formers";
            webDriver.Navigate().GoToUrl(url);

            //webDriver.Close();
            //webDriver.Dispose();

        }

        [TestMethod]
        public void TestMethod1()
        {
            _driverChrome.Navigate().GoToUrl("https://www.google.com");

            _driverChrome.FindElement(By.Id("lst-ib")).SendKeys("developpez.com");

            _driverChrome.FindElement(By.Id("lst-ib")).SendKeys(Keys.Enter);

        }



    }
}

