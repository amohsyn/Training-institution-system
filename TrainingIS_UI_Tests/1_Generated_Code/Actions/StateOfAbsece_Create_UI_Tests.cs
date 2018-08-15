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
    public class StateOfAbsece_Create_UI_Tests : Base_UI_Tests
    {
       

        public StateOfAbsece_Create_UI_Tests()
        {
            this.Entity_Path = "/StateOfAbseces";
        }
       
        [TestMethod]
        public void StateOfAbsece_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void StateOfAbsece_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            StateOfAbsece StateOfAbsece = new StateOfAbsecesControllerTests_Service().CreateValideStateOfAbseceInstance();
            Default_Form_StateOfAbsece_Model Default_Form_StateOfAbsece_Model = new Default_Form_StateOfAbsece_ModelBLM(new UnitOfWork<TrainingISModel>())
                .ConverTo_Default_Form_StateOfAbsece_Model(StateOfAbsece);



 
			var Name = b.FindElement(By.Id(nameof(Default_Form_StateOfAbsece_Model.Name)));
            Name.SendKeys(Default_Form_StateOfAbsece_Model.Name.ToString());

  			string xpath_Category = string.Format("//select[@id='{0}']/option[@value='{1}']", "Category", Default_Form_StateOfAbsece_Model.Category.ToString());
            b.FindElement(By.XPath(xpath_Category)).Click();

 
			var Value = b.FindElement(By.Id(nameof(Default_Form_StateOfAbsece_Model.Value)));
            Value.SendKeys(Default_Form_StateOfAbsece_Model.Value.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
