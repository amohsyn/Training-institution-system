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
using System.Linq;
using TrainingIS_UI_Tests.Base;
using System.Threading;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.Schedules
{
    [TestCategory("Index_UI_Test")]
	[TestCategory("Schedule")]
    public class Base_Index_Schedule_UI_Tests : Base_Index_Entity_UI_Test<Schedule>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }
		protected string GAppDataTable_Html_Id = "Schedules_Entities";

		// Properties
		public bool InitData_Initlizalize = false;
        public ScheduleTestDataFactory Schedule_TestData { set; get; }
        public ScheduleBLO ScheduleBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Schedules";
            this.Entity_Reference = "Schedule_CRUD_Test";

			// TestData and BLO
			Schedule_TestData = new ScheduleTestDataFactory(this.UnitOfWork, this.GAppContext);
            ScheduleBLO = new ScheduleBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Schedule_TestData.Create_CRUD_Schedule_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Index_Schedule_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Schedule Create_Data_Test = ScheduleBLO.FindBaseEntityByReference(this.Entity_Reference);
           if (Create_Data_Test != null)
                ScheduleBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Schedule_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Schedule_Index_Test()
        {
           
        }
		[TestMethod]
        public virtual void Export_Schedules_Tests()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();
            this.Html.Click("Export_All_Entities");
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
        }


		[TestMethod]
		public virtual void Import_And_Import_File_Example_Schedules_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

			this.ScheduleBLO.Save(this.Valide_Entity_Instance);

            // Export
            this.Html.Click("Export_Import_File_Example");
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
			Thread.Sleep(2000);

            // Import from TestMachin
            this.Html.Click("Import_Entities");
            string file_path = this.Test_Machine.Get_Downloads_Directory() + this.ScheduleBLO.Get_Import_File_Name();
            this.Html.Input_File("import_objects", file_path);
            this.Html.Click("Import_Submit");
         
            // Assert updated_rows 
            Assert.IsTrue(this.Elements.IsElementIdExist("Number_of_updated_rows"));

            // Assert Update only without eroors
            Assert.IsFalse(this.Elements.IsElementIdExist("Number_of_inserted_erros_rows"));
            Assert.IsFalse(this.Elements.IsElementIdExist("Number_of_updated_erros_rows"));
            Assert.IsFalse(this.Elements.IsElementIdExist("Number_of_inserted_rows"));
        }

    }

    [TestClass]
	
	public partial class Index_Schedule_UI_Tests : Base_Index_Schedule_UI_Tests
    {
		public Index_Schedule_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Index_Schedule_UI_Tests() : base(null){}
    }
}
