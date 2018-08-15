using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.WebApp.Tests.Services;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class ApplicationParam_Create_UI_Tests : Base_UI_Tests
    {
       

        public ApplicationParam_Create_UI_Tests()
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
            Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_ApplicationParam_Model(ApplicationParam);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Code)));
            Code.SendKeys(Default_Form_ApplicationParam_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Name)));
            Name.SendKeys(Default_Form_ApplicationParam_Model.Name.ToString());

 
			var Value = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Value)));
            Value.SendKeys(Default_Form_ApplicationParam_Model.Value.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Description)));
            Description.SendKeys(Default_Form_ApplicationParam_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
