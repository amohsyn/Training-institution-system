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
    public class ActionControllerApp_Create_UI_Tests : Base_UI_Tests
    {
       

        public ActionControllerApp_Create_UI_Tests()
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
            Default_Form_ActionControllerApp_Model Default_Form_ActionControllerApp_Model = new Default_Form_ActionControllerApp_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_ActionControllerApp_Model(ActionControllerApp);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_ActionControllerApp_Model.Code)));
            Code.SendKeys(Default_Form_ActionControllerApp_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_ActionControllerApp_Model.Name)));
            Name.SendKeys(Default_Form_ActionControllerApp_Model.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_ActionControllerApp_Model.Description)));
            Description.SendKeys(Default_Form_ActionControllerApp_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
