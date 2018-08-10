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
    public class TrainingYear_UI_Index_Tests : Base_UI_Tests
    {
       

        public TrainingYear_UI_Index_Tests()
        {
            this.Entity_Path = "/TrainingYears";
        }
       
        [TestMethod]
        public void TrainingYear_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void TrainingYear_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            TrainingYear TrainingYear = new TrainingYearsControllerTests_Service().CreateValideTrainingYearInstance();
            Default_TrainingYearFormView Default_TrainingYearFormView = new Default_TrainingYearFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_TrainingYearFormView(TrainingYear);



 
			var Code = b.FindElement(By.Id(nameof(Default_TrainingYearFormView.Code)));
            Code.SendKeys(Default_TrainingYearFormView.Code.ToString());

 
			var StartDate = b.FindElement(By.Id(nameof(Default_TrainingYearFormView.StartDate)));
            StartDate.SendKeys(Default_TrainingYearFormView.StartDate.ToString());

 
			var EndtDate = b.FindElement(By.Id(nameof(Default_TrainingYearFormView.EndtDate)));
            EndtDate.SendKeys(Default_TrainingYearFormView.EndtDate.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
