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
    public class ApplicationParam_UI_Index_Tests : Base_UI_Tests
    {
       

        public ApplicationParam_UI_Index_Tests()
        {
            this.Entity_Path = "/ApplicationParams";
        }
       
        [TestMethod]
        public void ApplicationParam_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void ApplicationParam_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ApplicationParam ApplicationParam = new ApplicationParamsControllerTests_Service().CreateValideApplicationParamInstance();
            Default_ApplicationParamFormView Default_ApplicationParamFormView = new Default_ApplicationParamFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_ApplicationParamFormView(ApplicationParam);



 
			var Code = b.FindElement(By.Id(nameof(Default_ApplicationParamFormView.Code)));
            Code.SendKeys(Default_ApplicationParamFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_ApplicationParamFormView.Name)));
            Name.SendKeys(Default_ApplicationParamFormView.Name.ToString());

 
			var Value = b.FindElement(By.Id(nameof(Default_ApplicationParamFormView.Value)));
            Value.SendKeys(Default_ApplicationParamFormView.Value.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_ApplicationParamFormView.Description)));
            Description.SendKeys(Default_ApplicationParamFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
