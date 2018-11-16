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
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.SeancePlannings
{
    [TestCategory("Index_UI_Test")]
    public class Base_Index_SeancePlanning_UI_Tests : Base_Index_Entity_UI_Test<SeancePlanning>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public SeancePlanningTestDataFactory SeancePlanning_TestData { set; get; }
        public SeancePlanningBLO SeancePlanningBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/SeancePlannings";
            // this.Entity_Reference = "SeancePlanning_CRUD_Test";

			// TestData and BLO
			SeancePlanning_TestData = new SeancePlanningTestDataFactory(this.UnitOfWork, this.GAppContext);
            SeancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            //this.Valide_Entity_Instance = SeancePlanning_TestData.CreateValideSeancePlanningInstance();
            // this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Index_SeancePlanning_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           //SeancePlanning Create_Data_Test = SeancePlanningBLO.FindBaseEntityByReference(this.Entity_Reference);
           // if (Create_Data_Test != null)
           //     SeancePlanningBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void SeancePlanning_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void SeancePlanning_Index_Test()
        {
           
        }
		[TestMethod]
        public virtual void Export_SeancePlannings_Tests()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();
            this.Html.Click("Export_All_Entities");
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
        }

		[TestMethod]
        public virtual void Export_Import_File_Example_SeancePlannings_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();
            this.Html.Click("Export_Import_File_Example");
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
        }

		[TestMethod]
        public virtual void Import_SeancePlannings_Test()
        {
            Assert.Fail();
        }

    }

    [TestClass]
	
	public partial class Index_SeancePlanning_UI_Tests : Base_Index_SeancePlanning_UI_Tests
    {
		public Index_SeancePlanning_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Index_SeancePlanning_UI_Tests() : base(null){}
    }
}
