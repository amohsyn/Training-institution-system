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
    public class RoleApp_UI_Index_Tests : Base_UI_Tests
    {
       

        public RoleApp_UI_Index_Tests()
        {
            this.Entity_Path = "/RoleApps";
        }
       
        [TestMethod]
        public void RoleApp_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void RoleApp_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            RoleApp RoleApp = new RoleAppsControllerTests_Service().CreateValideRoleAppInstance();
            Default_RoleAppFormView Default_RoleAppFormView = new Default_RoleAppFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_RoleAppFormView(RoleApp);



 
			var Code = b.FindElement(By.Id(nameof(Default_RoleAppFormView.Code)));
            Code.SendKeys(Default_RoleAppFormView.Code.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_RoleAppFormView.Description)));
            Description.SendKeys(Default_RoleAppFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
