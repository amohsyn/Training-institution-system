using System;
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
using System.Linq;
using TrainingIS_UI_Tests.Base;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.CalendarDays
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("CalendarDay")]
    public class Base_Create_CalendarDay_UI_Tests : Base_Create_Entity_UI_Test<CalendarDay>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public CalendarDayTestDataFactory CalendarDay_TestData { set; get; }
        public CalendarDayBLO CalendarDayBLO  { set; get; }
        public string Reference_Created_Object = null;

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);

			//
            // GApp Context
            //
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();
            this.GAppContext = new GAppContext(RoleBLO.Root_ROLE);
            TrainingYear CurrentTrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).getCurrentTrainingYear();
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
            this.GAppContext.Session.Add(TrainingYearBLO.Current_TrainingYear_Key, CurrentTrainingYear);

			// Controller Name
            this.UI_Test_Context.ControllerName = "/CalendarDays";
            this.Entity_Reference = "CalendarDay_CRUD_Test";

			// TestData and BLO
			CalendarDay_TestData = new CalendarDayTestDataFactory(this.UnitOfWork, this.GAppContext);
            CalendarDayBLO = new CalendarDayBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = CalendarDay_TestData.Create_CRUD_CalendarDay_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_CalendarDay_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
		/// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                this.CleanData();
                InitData_Initlizalize = true;
            }
           
        }

        /// <summary>
        /// CleanData well be executed after each TestMethod
        /// </summary>
        [TestCleanup]
        public virtual void CleanData()
        {
            // Clean Create Data Test
           CalendarDay Create_Data_Test = CalendarDayBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                CalendarDayBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void CalendarDay_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void CalendarDay_Create_Test()
        {
            CalendarDay_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void CalendarDay_UI_Create(CalendarDay CalendarDay)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert CalendarDay
            Default_CalendarDay_Create_Model Default_CalendarDay_Create_Model = new Default_CalendarDay_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_CalendarDay_Create_Model(CalendarDay);

			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.Date), Default_CalendarDay_Create_Model.Date.ToString());
			var DateName = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.DateName)));
            DateName.SendKeys(Default_CalendarDay_Create_Model.DateName.ToString());
			var DateNameAbbrev = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.DateNameAbbrev)));
            DateNameAbbrev.SendKeys(Default_CalendarDay_Create_Model.DateNameAbbrev.ToString());
			var DayOfWeek = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.DayOfWeek)));
            DayOfWeek.SendKeys(Default_CalendarDay_Create_Model.DayOfWeek.ToString());
			var IsWeekend = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.IsWeekend)));
			if (Default_CalendarDay_Create_Model.IsWeekend)
                IsWeekend.Click();
			var WeekNumber = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.WeekNumber)));
            WeekNumber.SendKeys(Default_CalendarDay_Create_Model.WeekNumber.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.WeekBeginDate), Default_CalendarDay_Create_Model.WeekBeginDate.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.WeekEndDate), Default_CalendarDay_Create_Model.WeekEndDate.ToString());
			var CalendarMonthName = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.CalendarMonthName)));
            CalendarMonthName.SendKeys(Default_CalendarDay_Create_Model.CalendarMonthName.ToString());
			var CalendarMonthNameAbbrev = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.CalendarMonthNameAbbrev)));
            CalendarMonthNameAbbrev.SendKeys(Default_CalendarDay_Create_Model.CalendarMonthNameAbbrev.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.CalendarMonthBegin), Default_CalendarDay_Create_Model.CalendarMonthBegin.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.CalendarMonthEnd), Default_CalendarDay_Create_Model.CalendarMonthEnd.ToString());
			var CalendarMonthNumber = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.CalendarMonthNumber)));
            CalendarMonthNumber.SendKeys(Default_CalendarDay_Create_Model.CalendarMonthNumber.ToString());
			var CalendarYear = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.CalendarYear)));
            CalendarYear.SendKeys(Default_CalendarDay_Create_Model.CalendarYear.ToString());
			var FiscalYear = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.FiscalYear)));
            FiscalYear.SendKeys(Default_CalendarDay_Create_Model.FiscalYear.ToString());
			var DayOfYear = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.DayOfYear)));
            DayOfYear.SendKeys(Default_CalendarDay_Create_Model.DayOfYear.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.CalendarYearBegin), Default_CalendarDay_Create_Model.CalendarYearBegin.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.CalendarYearEnd), Default_CalendarDay_Create_Model.CalendarYearEnd.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.FiscalYearBegin), Default_CalendarDay_Create_Model.FiscalYearBegin.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_CalendarDay_Create_Model.FiscalYearEnd), Default_CalendarDay_Create_Model.FiscalYearEnd.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_CalendarDay_Create_Model.Reference)));
            Reference.SendKeys(Default_CalendarDay_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_CalendarDay_UI_Tests : Base_Create_CalendarDay_UI_Tests
    {
		public Create_CalendarDay_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_CalendarDay_UI_Tests() : base(null){}
    }
}
