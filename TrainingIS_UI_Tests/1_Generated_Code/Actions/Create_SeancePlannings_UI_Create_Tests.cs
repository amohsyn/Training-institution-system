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
    public class SeancePlanning_UI_Index_Tests : Base_UI_Tests
    {
       

        public SeancePlanning_UI_Index_Tests()
        {
            this.Entity_Path = "/SeancePlannings";
        }
       
        [TestMethod]
        public void SeancePlanning_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void SeancePlanning_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            SeancePlanning SeancePlanning = new SeancePlanningsControllerTests_Service().CreateValideSeancePlanningInstance();
            Default_SeancePlanningFormView Default_SeancePlanningFormView = new Default_SeancePlanningFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_SeancePlanningFormView(SeancePlanning);



			string xpath_ScheduleId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ScheduleId", Default_SeancePlanningFormView.ScheduleId.ToString());
            b.FindElement(By.XPath(xpath_ScheduleId)).Click(); 

			string xpath_TrainingId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingId", Default_SeancePlanningFormView.TrainingId.ToString());
            b.FindElement(By.XPath(xpath_TrainingId)).Click(); 

			string xpath_SeanceDayId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeanceDayId", Default_SeancePlanningFormView.SeanceDayId.ToString());
            b.FindElement(By.XPath(xpath_SeanceDayId)).Click(); 

			string xpath_SeanceNumberId = string.Format("//select[@id='{0}']/option[@value='{1}']", "SeanceNumberId", Default_SeancePlanningFormView.SeanceNumberId.ToString());
            b.FindElement(By.XPath(xpath_SeanceNumberId)).Click(); 

			string xpath_ClassroomId = string.Format("//select[@id='{0}']/option[@value='{1}']", "ClassroomId", Default_SeancePlanningFormView.ClassroomId.ToString());
            b.FindElement(By.XPath(xpath_ClassroomId)).Click(); 

 
			var Description = b.FindElement(By.Id(nameof(Default_SeancePlanningFormView.Description)));
            Description.SendKeys(Default_SeancePlanningFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
