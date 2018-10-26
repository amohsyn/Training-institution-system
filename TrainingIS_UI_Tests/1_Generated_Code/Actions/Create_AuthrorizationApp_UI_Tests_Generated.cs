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

namespace TrainingIS_UI_Tests.AuthrorizationApps
{
    public class Base_Create_AuthrorizationApp_UI_Tests : Create_Entity_UI_Test<AuthrorizationApp>
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
                AuthrorizationApp_TestData.Insert_Test_Data_If_Not_Exist();
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
            AuthrorizationApp_UI_Create(this.Valide_Entity_Insrance);
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
            Default_Form_AuthrorizationApp_Model Default_Form_AuthrorizationApp_Model = new Default_Form_AuthrorizationApp_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_AuthrorizationApp_Model(AuthrorizationApp);

			this.Select.SelectValue("RoleAppId", Default_Form_AuthrorizationApp_Model.RoleAppId.ToString());
			this.Select.SelectValue("ControllerAppId", Default_Form_AuthrorizationApp_Model.ControllerAppId.ToString());
			var isAllAction = b.FindElement(By.Id(nameof(Default_Form_AuthrorizationApp_Model.isAllAction)));
			if (Default_Form_AuthrorizationApp_Model.isAllAction)
                isAllAction.Click();
			var Selected_ActionControllerApps = b.FindElement(By.Id(nameof(Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement_Selected_ActionControllerApps = new OpenQA.Selenium.Support.UI.SelectElement(Selected_ActionControllerApps);
            foreach (var item in Default_Form_AuthrorizationApp_Model.Selected_ActionControllerApps)
            {
                selectElement_Selected_ActionControllerApps.SelectByValue(item);
            }	 
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
