using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;

namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Nationalty_UI_Index_Tests : Base_UI_Tests
    {
       

        public Nationalty_UI_Index_Tests()
        {
            this.Entity_Path = "/Nationalities";
        }
       
        [TestMethod]
        public void Nationalty_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Nationalty_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Nationality Nationality = new NationalitiesControllerTests_Service().CreateValideNationalityInstance();

            var Name = b.FindElement(By.Id(nameof(Nationality.Name)));
            Name.SendKeys(Nationality.Name);

            var Code = b.FindElement(By.Id(nameof(Nationality.Code)));
            Code.SendKeys(Nationality.Code);

            var Description = b.FindElement(By.Id(nameof(Nationality.Description)));
            Description.SendKeys(Nationality.Description);

            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();


            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }

      



        //[TestMethod]
        //public void Former_Create_Test()
        //{
        //    var Former_URL = _URL + "/Formers";
        //    b.Navigate().GoToUrl(Former_URL);
        //}






    }
}
