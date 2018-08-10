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
    public class Schedule_UI_Index_Tests : Base_UI_Tests
    {
       

        public Schedule_UI_Index_Tests()
        {
            this.Entity_Path = "/Schedules";
        }
       
        [TestMethod]
        public void Schedule_Index_Show_Test()
        {
            this.GoTo_Index();
        }

        [TestMethod]
        public void Schedule_Create_Test()
        {
            this.GoTo_Index();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Former
            Schedule Schedule = new SchedulesControllerTests_Service().CreateValideScheduleInstance();
            Default_ScheduleFormView Default_ScheduleFormView = new Default_ScheduleFormViewBLM(new TrainingIS.DAL.UnitOfWork())
                .ConverTo_Default_ScheduleFormView(Schedule);



			string xpath_TrainingYearId = string.Format("//select[@id='{0}']/option[@value='{1}']", "TrainingYearId", Default_ScheduleFormView.TrainingYearId.ToString());
            b.FindElement(By.XPath(xpath_TrainingYearId)).Click(); 

 
			var StartDate = b.FindElement(By.Id(nameof(Default_ScheduleFormView.StartDate)));
            StartDate.SendKeys(Default_ScheduleFormView.StartDate.ToString());

 
			var EndtDate = b.FindElement(By.Id(nameof(Default_ScheduleFormView.EndtDate)));
            EndtDate.SendKeys(Default_ScheduleFormView.EndtDate.ToString());

 
			var Description = b.FindElement(By.Id(nameof(Default_ScheduleFormView.Description)));
            Description.SendKeys(Default_ScheduleFormView.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }
}
