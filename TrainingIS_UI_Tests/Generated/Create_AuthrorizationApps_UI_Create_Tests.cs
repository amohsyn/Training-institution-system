using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.Authorizations;
using TrainingIS.BLL.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class AuthrorizationApp_UI_Index_Tests : Base_UI_Tests
    {
       

        public AuthrorizationApp_UI_Index_Tests()
        {
            this.Entity_Path = "/AuthrorizationApps";
        }
       
        [TestMethod]
        public void AuthrorizationApp_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void AuthrorizationApp_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            AuthrorizationApp AuthrorizationApp = new AuthrorizationAppsControllerTests_Service().CreateValideAuthrorizationAppInstance();
            AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_AuthrorizationAppFormView(AuthrorizationApp);



			string xpath_RoleAppId = string.Format("//select[@id='{0}']/option[@value='{1}']", "RoleAppId", AuthrorizationAppFormView.RoleAppId.ToString());
            b.FindElement(By.XPath(xpath_RoleAppId)).Click(); 

			string xpath_ControllerAppId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ControllerAppId", AuthrorizationAppFormView.ControllerAppId.ToString());
            b.FindElement(By.XPath(xpath_ControllerAppId)).Click(); 

			var isAllAction = b.FindElement(By.Id(nameof(AuthrorizationAppFormView.isAllAction)));
			if (AuthrorizationAppFormView.isAllAction)
                isAllAction.Click();

			var Selected_ActionControllerApps = b.FindElement(By.Id(nameof(AuthrorizationAppFormView.Selected_ActionControllerApps)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_ActionControllerApps);
            foreach (var item in AuthrorizationAppFormView.Selected_ActionControllerApps)
            {
                selectElement.SelectByValue(item);
            }	 

 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
