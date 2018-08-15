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
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests
{
    [TestClass]
    public class AuthrorizationApp_Create_UI_Tests : Base_UI_Tests
    {
       

        public AuthrorizationApp_Create_UI_Tests()
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
            Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp);



			var isAllAction = b.FindElement(By.Id(nameof(Default_Form_AuthrorizationApp_Model.isAllAction)));
			if (Default_Form_AuthrorizationApp_Model.isAllAction)
                isAllAction.Click();

			var Selected_ActionControllerApps = b.FindElement(By.Id(nameof(Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_ActionControllerApps);
            foreach (var item in Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps)
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
