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
    public class SeanceTraining_UI_Index_Tests : Base_UI_Tests
    {
       

        public SeanceTraining_UI_Index_Tests()
        {
            this.Entity_Path = "/SeanceTrainings";
        }
       
        [TestMethod]
        public void SeanceTraining_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void SeanceTraining_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeanceTraining SeanceTraining = new SeanceTrainingsControllerTests_Service().CreateValideSeanceTrainingInstance();
            Default_SeanceTrainingFormView Default_SeanceTrainingFormView = new Default_SeanceTrainingFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_SeanceTrainingFormView(SeanceTraining);



 
			var SeanceDate = b.FindElement(By.Id(nameof(Default_SeanceTrainingFormView.SeanceDate)));
            SeanceDate.SendKeys(Default_SeanceTrainingFormView.SeanceDate.ToString());

			string xpath_SeancePlanningId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeancePlanningId", Default_SeanceTrainingFormView.SeancePlanningId.ToString());
            b.FindElement(By.XPath(xpath_SeancePlanningId)).Click(); 
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
