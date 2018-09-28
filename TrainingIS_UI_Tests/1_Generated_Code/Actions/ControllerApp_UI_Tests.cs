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
namespace TrainingIS_UI_Tests.ControllerApps
{
    public class Base_ControllerApp_UI_Tests : Base_UI_Tests
    {
       

        public Base_ControllerApp_UI_Tests()
        {
            this.Entity_Path = "/ControllerApps";
        }
       
        [TestMethod]
        public virtual void ControllerApp_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void ControllerApp_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            ControllerApp ControllerApp = new ControllerAppsControllerTests_Service().CreateValideControllerAppInstance(null,GAppContext);
            Default_Form_ControllerApp_Model Default_Form_ControllerApp_Model = new Default_Form_ControllerApp_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_ControllerApp_Model(ControllerApp);



 
			var Code = b.FindElement(By.Id(nameof(Default_Form_ControllerApp_Model.Code)));
            Code.SendKeys(Default_Form_ControllerApp_Model.Code.ToString());

 
			var Name = b.FindElement(By.Id(nameof(Default_Form_ControllerApp_Model.Name)));
            Name.SendKeys(Default_Form_ControllerApp_Model.Name.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_Form_ControllerApp_Model.Description)));
            Description.SendKeys(Default_Form_ControllerApp_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class ControllerApp_UI_Tests : Base_ControllerApp_UI_Tests
    {

    }
}
