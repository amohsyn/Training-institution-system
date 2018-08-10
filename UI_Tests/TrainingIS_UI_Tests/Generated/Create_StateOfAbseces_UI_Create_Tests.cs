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
    public class StateOfAbsece_UI_Index_Tests : Base_UI_Tests
    {
       

        public StateOfAbsece_UI_Index_Tests()
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
            Default_StateOfAbseceFormView Default_StateOfAbseceFormView = new Default_StateOfAbseceFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_StateOfAbseceFormView(StateOfAbsece);



 
			var Name = b.FindElement(By.Id(nameof(Default_StateOfAbseceFormView.Name)));
            Name.SendKeys(Default_StateOfAbseceFormView.Name.ToString());

  			string xpath_Category = string.Format("//select[@id='{0}']/option[@value='{1}']", "Category", Default_StateOfAbseceFormView.Category.ToString());
            b.FindElement(By.XPath(xpath_Category)).Click();

 
			var Value = b.FindElement(By.Id(nameof(Default_StateOfAbseceFormView.Value)));
            Value.SendKeys(Default_StateOfAbseceFormView.Value.ToString());

			string xpath_TraineeId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TraineeId", Default_StateOfAbseceFormView.TraineeId.ToString());
            b.FindElement(By.XPath(xpath_TraineeId)).Click(); 
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
