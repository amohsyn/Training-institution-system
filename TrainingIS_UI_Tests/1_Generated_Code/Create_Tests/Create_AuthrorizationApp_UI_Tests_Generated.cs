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

namespace TrainingIS_UI_Tests.AuthrorizationApps
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("AuthrorizationApp")]
    public class Base_Create_AuthrorizationApp_UI_Tests : Base_Create_Entity_UI_Test<AuthrorizationApp>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public AuthrorizationAppTestDataFactory AuthrorizationApp_TestData { set; get; }
        public AuthrorizationAppBLO AuthrorizationAppBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/AuthrorizationApps";
            this.Entity_Reference = "AuthrorizationApp_CRUD_Test";

			// TestData and BLO
			AuthrorizationApp_TestData = new AuthrorizationAppTestDataFactory(this.UnitOfWork, this.GAppContext);
            AuthrorizationAppBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = AuthrorizationApp_TestData.Create_CRUD_AuthrorizationApp_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_AuthrorizationApp_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           AuthrorizationApp Create_Data_Test = AuthrorizationAppBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                AuthrorizationAppBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void AuthrorizationApp_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void AuthrorizationApp_Create_Test()
        {
            AuthrorizationApp_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void AuthrorizationApp_UI_Create(AuthrorizationApp AuthrorizationApp)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert AuthrorizationApp
            Default_AuthrorizationApp_Create_Model Default_AuthrorizationApp_Create_Model = new Default_AuthrorizationApp_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_AuthrorizationApp_Create_Model(AuthrorizationApp);

			this.Select.SelectValue("RoleAppId", Default_AuthrorizationApp_Create_Model.RoleAppId.ToString());
			this.Select.SelectValue("ControllerAppId", Default_AuthrorizationApp_Create_Model.ControllerAppId.ToString());
			var isAllAction = b.FindElement(By.Id(nameof(Default_AuthrorizationApp_Create_Model.isAllAction)));
			if (Default_AuthrorizationApp_Create_Model.isAllAction)
                isAllAction.Click();
			var Selected_ActionControllerApps = b.FindElement(By.Id(nameof(Default_AuthrorizationApp_Create_Model.Selected_ActionControllerApps)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement_Selected_ActionControllerApps = new OpenQA.Selenium.Support.UI.SelectElement(Selected_ActionControllerApps);
            foreach (var item in Default_AuthrorizationApp_Create_Model.Selected_ActionControllerApps)
            {
                selectElement_Selected_ActionControllerApps.SelectByValue(item);
            }	 
			var Reference = b.FindElement(By.Id(nameof(Default_AuthrorizationApp_Create_Model.Reference)));
            Reference.SendKeys(Default_AuthrorizationApp_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_AuthrorizationApp_UI_Tests : Base_Create_AuthrorizationApp_UI_Tests
    {
		public Create_AuthrorizationApp_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_AuthrorizationApp_UI_Tests() : base(null){}
    }
}
