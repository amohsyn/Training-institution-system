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
namespace TrainingIS_UI_Tests.Schedules
{
    public class Base_Schedule_UI_Tests : Base_UI_Tests
    {
       

        public Base_Schedule_UI_Tests()
        {
            this.Entity_Path = "/Schedules";
        }
       
        [TestMethod]
        public virtual void Schedule_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void Schedule_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Schedule
            Schedule Schedule = new SchedulesControllerTests_Service().CreateValideScheduleInstance(null,GAppContext);
            Default_Form_Schedule_Model Default_Form_Schedule_Model = new Default_Form_Schedule_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Schedule_Model(Schedule);



			this.Select.SelectValue("TrainingYearId", Default_Form_Schedule_Model.TrainingYearId.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Schedule_Model.StartDate), Default_Form_Schedule_Model.StartDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_Schedule_Model.EndtDate), Default_Form_Schedule_Model.EndtDate.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Schedule_Model.Description)));
            Description.SendKeys(Default_Form_Schedule_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class Schedule_UI_Tests : Base_Schedule_UI_Tests
    {

    }
}
