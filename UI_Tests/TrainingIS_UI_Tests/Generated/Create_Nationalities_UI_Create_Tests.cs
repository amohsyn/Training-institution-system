using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class Nationality_UI_Index_Tests : Base_UI_Tests
    {
       

        public Nationality_UI_Index_Tests()
        {
            this.Entity_Path = "/Nationalities";
        }
       
        [TestMethod]
        public void Nationality_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Nationality_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Nationality Nationality = new NationalitiesControllerTests_Service().CreateValideNationalityInstance();
            Default_NationalityFormView Default_NationalityFormView = new Default_NationalityFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_NationalityFormView(Nationality);



 
			var Code = b.FindElement(By.Id(nameof(Default_NationalityFormView.Code)));
            Code.SendKeys(Default_NationalityFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_NationalityFormView.Name)));
            Name.SendKeys(Default_NationalityFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_NationalityFormView.Description)));
            Description.SendKeys(Default_NationalityFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
