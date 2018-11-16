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

namespace TrainingIS_UI_Tests.TrainingLevels
{
    [TestCategory("Details_UI_Test")]
    public class Base_Details_TrainingLevel_UI_Tests : Base_Delete_Entity_UI_Test<TrainingLevel>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public TrainingLevelTestDataFactory TrainingLevel_TestData { set; get; }
        public TrainingLevelBLO TrainingLevelBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/TrainingLevels";
            this.Entity_Reference = "TrainingLevel_CRUD_Test";

			// TestData and BLO
			TrainingLevel_TestData = new TrainingLevelTestDataFactory(this.UnitOfWork, this.GAppContext);
            TrainingLevelBLO = new TrainingLevelBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = TrainingLevel_TestData.CreateValideTrainingLevelInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Details_TrainingLevel_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           TrainingLevel Create_Data_Test = TrainingLevelBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                TrainingLevelBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void TrainingLevel_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void TrainingLevel_Details_Test()
        {
           
        }
    }

    [TestClass]
	
	public partial class Details_TrainingLevel_UI_Tests : Base_Details_TrainingLevel_UI_Tests
    {
		public Details_TrainingLevel_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Details_TrainingLevel_UI_Tests() : base(null){}
    }
}
