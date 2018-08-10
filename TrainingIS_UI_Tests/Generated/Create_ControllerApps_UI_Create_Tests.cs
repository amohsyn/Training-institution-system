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
    public class ControllerApp_UI_Index_Tests : Base_UI_Tests
    {
       

        public ControllerApp_UI_Index_Tests()
        {
            this.Entity_Path = "/ControllerApps";
        }
       
        [TestMethod]
        public void ControllerApp_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void ControllerApp_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ControllerApp ControllerApp = new ControllerAppsControllerTests_Service().CreateValideControllerAppInstance();
            Default_ControllerAppFormView Default_ControllerAppFormView = new Default_ControllerAppFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_ControllerAppFormView(ControllerApp);



 
			var Code = b.FindElement(By.Id(nameof(Default_ControllerAppFormView.Code)));
            Code.SendKeys(Default_ControllerAppFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_ControllerAppFormView.Name)));
            Name.SendKeys(Default_ControllerAppFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_ControllerAppFormView.Description)));
            Description.SendKeys(Default_ControllerAppFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
