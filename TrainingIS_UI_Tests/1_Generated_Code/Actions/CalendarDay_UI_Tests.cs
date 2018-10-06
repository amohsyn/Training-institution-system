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
namespace TrainingIS_UI_Tests.CalendarDays
{
    public class Base_CalendarDay_UI_Tests : Base_UI_Tests
    {
       

        public Base_CalendarDay_UI_Tests()
        {
            this.Entity_Path = "/CalendarDays";
        }
       
        [TestMethod]
        public virtual void CalendarDay_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

        [TestMethod]
        public virtual void CalendarDay_Create_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert CalendarDay
            CalendarDay CalendarDay = new CalendarDaysControllerTests_Service().CreateValideCalendarDayInstance(null,GAppContext);
            Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = new Default_Form_CalendarDay_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_CalendarDay_Model(CalendarDay);



			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.Date), Default_Form_CalendarDay_Model.Date.ToString());

	 


 
			var DateName = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.DateName)));
            DateName.SendKeys(Default_Form_CalendarDay_Model.DateName.ToString());

	 


 
			var DateNameAbbrev = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.DateNameAbbrev)));
            DateNameAbbrev.SendKeys(Default_Form_CalendarDay_Model.DateNameAbbrev.ToString());

			var IsWeekend = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.IsWeekend)));
			if (Default_Form_CalendarDay_Model.IsWeekend)
                IsWeekend.Click();

	 


 
			var WeekNumber = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.WeekNumber)));
            WeekNumber.SendKeys(Default_Form_CalendarDay_Model.WeekNumber.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.WeekBeginDate), Default_Form_CalendarDay_Model.WeekBeginDate.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.WeekEndDate), Default_Form_CalendarDay_Model.WeekEndDate.ToString());

	 


 
			var CalendarMonthName = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.CalendarMonthName)));
            CalendarMonthName.SendKeys(Default_Form_CalendarDay_Model.CalendarMonthName.ToString());

	 


 
			var CalendarMonthNameAbbrev = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.CalendarMonthNameAbbrev)));
            CalendarMonthNameAbbrev.SendKeys(Default_Form_CalendarDay_Model.CalendarMonthNameAbbrev.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.CalendarMonthBegin), Default_Form_CalendarDay_Model.CalendarMonthBegin.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.CalendarMonthEnd), Default_Form_CalendarDay_Model.CalendarMonthEnd.ToString());

	 


 
			var CalendarMonthNumber = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.CalendarMonthNumber)));
            CalendarMonthNumber.SendKeys(Default_Form_CalendarDay_Model.CalendarMonthNumber.ToString());

	 


 
			var CalendarYear = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.CalendarYear)));
            CalendarYear.SendKeys(Default_Form_CalendarDay_Model.CalendarYear.ToString());

	 


 
			var FiscalYear = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.FiscalYear)));
            FiscalYear.SendKeys(Default_Form_CalendarDay_Model.FiscalYear.ToString());

	 


 
			var DayOfYear = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.DayOfYear)));
            DayOfYear.SendKeys(Default_Form_CalendarDay_Model.DayOfYear.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.CalendarYearBegin), Default_Form_CalendarDay_Model.CalendarYearBegin.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.CalendarYearEnd), Default_Form_CalendarDay_Model.CalendarYearEnd.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.FiscalYearBegin), Default_Form_CalendarDay_Model.FiscalYearBegin.ToString());

			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.FiscalYearEnd), Default_Form_CalendarDay_Model.FiscalYearEnd.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.Is_In_IndexPage());
            Assert.IsTrue(this.Is_Info_Alert());
        }


    }

    [TestClass]
	public partial class CalendarDay_UI_Tests : Base_CalendarDay_UI_Tests
    {

    }
}
