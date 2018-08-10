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
    public class SeanceDay_UI_Index_Tests : Base_UI_Tests
    {
       

        public SeanceDay_UI_Index_Tests()
        {
            this.Entity_Path = "/SeanceDays";
        }
       
        [TestMethod]
        public void SeanceDay_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void SeanceDay_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeanceDay SeanceDay = new SeanceDaysControllerTests_Service().CreateValideSeanceDayInstance();
            Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_SeanceDayFormView(SeanceDay);



 
			var Name = b.FindElement(By.Id(nameof(Default_SeanceDayFormView.Name)));
            Name.SendKeys(Default_SeanceDayFormView.Name.ToString());

 
			var Code = b.FindElement(By.Id(nameof(Default_SeanceDayFormView.Code)));
            Code.SendKeys(Default_SeanceDayFormView.Code.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_SeanceDayFormView.Description)));
            Description.SendKeys(Default_SeanceDayFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
