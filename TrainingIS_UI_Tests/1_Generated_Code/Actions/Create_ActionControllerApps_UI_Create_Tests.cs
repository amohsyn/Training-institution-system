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
    public class ActionControllerApp_UI_Index_Tests : Base_UI_Tests
    {
       

        public ActionControllerApp_UI_Index_Tests()
        {
            this.Entity_Path = "/ActionControllerApps";
        }
       
        [TestMethod]
        public void ActionControllerApp_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void ActionControllerApp_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ActionControllerApp ActionControllerApp = new ActionControllerAppsControllerTests_Service().CreateValideActionControllerAppInstance();
            Default_ActionControllerAppFormView Default_ActionControllerAppFormView = new Default_ActionControllerAppFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_ActionControllerAppFormView(ActionControllerApp);



 
			var Code = b.FindElement(By.Id(nameof(Default_ActionControllerAppFormView.Code)));
            Code.SendKeys(Default_ActionControllerAppFormView.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_ActionControllerAppFormView.Name)));
            Name.SendKeys(Default_ActionControllerAppFormView.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_ActionControllerAppFormView.Description)));
            Description.SendKeys(Default_ActionControllerAppFormView.Description.ToString());

			string xpath_ControllerAppId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ControllerAppId", Default_ActionControllerAppFormView.ControllerAppId.ToString());
            b.FindElement(By.XPath(xpath_ControllerAppId)).Click(); 
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
