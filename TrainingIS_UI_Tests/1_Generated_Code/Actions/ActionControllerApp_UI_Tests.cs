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
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
namespace TrainingIS_UI_Tests.ActionControllerApps
{
    public class Base_ActionControllerApp_UI_Tests : Base_UI_Tests
    {
       

        public Base_ActionControllerApp_UI_Tests()
        {
            this.Entity_Path = "/ActionControllerApps";
        }
       
        [TestMethod]
        public virtual void ActionControllerApp_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void ActionControllerApp_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ActionControllerApp ActionControllerApp = new ActionControllerAppsControllerTests_Service().CreateValideActionControllerAppInstance(null,GAppContext);
            Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = new Default_Form_ActionControllerApp_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_ActionControllerApp_Model(ActionControllerApp);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_ActionControllerApp_Model.Code)));
            Code.SendKeys(Default_Form_ActionControllerApp_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_ActionControllerApp_Model.Name)));
            Name.SendKeys(Default_Form_ActionControllerApp_Model.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_ActionControllerApp_Model.Description)));
            Description.SendKeys(Default_Form_ActionControllerApp_Model.Description.ToString());

			string xpath_ControllerAppId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ControllerAppId", Default_Form_ActionControllerApp_Model.ControllerAppId.ToString());
            b.FindElement(By.XPath(xpath_ControllerAppId)).Click(); 
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class ActionControllerApp_UI_Tests : Base_ActionControllerApp_UI_Tests
    {

    }
}
