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
    public class YearStudy_UI_Index_Tests : Base_UI_Tests
    {
       

        public YearStudy_UI_Index_Tests()
        {
            this.Entity_Path = "/YearStudies";
        }
       
        [TestMethod]
        public void YearStudy_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void YearStudy_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            YearStudy YearStudy = new YearStudiesControllerTests_Service().CreateValideYearStudyInstance();
            Default_YearStudyFormView Default_YearStudyFormView = new Default_YearStudyFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_YearStudyFormView(YearStudy);



 
			var Code = b.FindElement(By.Id(nameof(Default_YearStudyFormView.Code)));
            Code.SendKeys(Default_YearStudyFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_YearStudyFormView.Name)));
            Name.SendKeys(Default_YearStudyFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_YearStudyFormView.Description)));
            Description.SendKeys(Default_YearStudyFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
