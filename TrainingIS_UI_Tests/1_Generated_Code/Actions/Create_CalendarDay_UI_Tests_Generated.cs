﻿using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.DAL;
using GApp.Core.Context;
using GApp.UnitTest.UI_Tests;
using GApp.UnitTest.Context;
using TestData;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.CalendarDays
{
    public class Base_Create_CalendarDay_UI_Tests : Create_Entity_UI_Test<CalendarDay>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/CalendarDays";
            this.Entity_Reference = "CalendarDay_CRUD_Test";
        }

		public Base_Create_CalendarDay_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
		{
            //
            // GApp Context
            //
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();
            this.GAppContext = new GAppContext(RoleBLO.Root_ROLE);
            TrainingYear CurrentTrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).getCurrentTrainingYear();
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
            this.GAppContext.Session.Add(TrainingYearBLO.Current_TrainingYear_Key, CurrentTrainingYear);

        }
 
        
        [TestMethod]
        public virtual void CalendarDay_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void CalendarDay_Create_Test()
        {
            CalendarDay_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void CalendarDay_Create(CalendarDay CalendarDay)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert CalendarDay
            Default_Form_CalendarDay_Model Default_Form_CalendarDay_Model = new Default_Form_CalendarDay_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_CalendarDay_Model(CalendarDay);



			
			this.DateTimePicker.SelectDate(nameof(Default_Form_CalendarDay_Model.Date), Default_Form_CalendarDay_Model.Date.ToString());

	 


 
			var DateName = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.DateName)));
            DateName.SendKeys(Default_Form_CalendarDay_Model.DateName.ToString());

	 


 
			var DateNameAbbrev = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.DateNameAbbrev)));
            DateNameAbbrev.SendKeys(Default_Form_CalendarDay_Model.DateNameAbbrev.ToString());

	 


 
			var DayOfWeek = b.FindElement(By.Id(nameof(Default_Form_CalendarDay_Model.DayOfWeek)));
            DayOfWeek.SendKeys(Default_Form_CalendarDay_Model.DayOfWeek.ToString());

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

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new CalendarDayTestDataFactory(null, this.GAppContext).CreateValideCalendarDayInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete CalendarDay_CRUD_Test if Exist
            CalendarDayBLO CalendarDayBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);
            CalendarDay existante_entity = CalendarDayBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                CalendarDayBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_CalendarDay_UI_Tests : Base_Create_CalendarDay_UI_Tests
    {
		public Create_CalendarDay_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_CalendarDay_UI_Tests() : base(null){}
    }
}