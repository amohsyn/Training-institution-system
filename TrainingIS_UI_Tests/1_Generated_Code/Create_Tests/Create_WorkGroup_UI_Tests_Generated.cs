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
using TrainingIS.Models.WorkGroups;

namespace TrainingIS_UI_Tests.WorkGroups
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("WorkGroup")]
    public class Base_Create_WorkGroup_UI_Tests : Base_Create_Entity_UI_Test<WorkGroup>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public WorkGroupTestDataFactory WorkGroup_TestData { set; get; }
        public WorkGroupBLO WorkGroupBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/WorkGroups";
            this.Entity_Reference = "WorkGroup_CRUD_Test";

			// TestData and BLO
			WorkGroup_TestData = new WorkGroupTestDataFactory(this.UnitOfWork, this.GAppContext);
            WorkGroupBLO = new WorkGroupBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = WorkGroup_TestData.Create_CRUD_WorkGroup_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_WorkGroup_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           WorkGroup Create_Data_Test = WorkGroupBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                WorkGroupBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void WorkGroup_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void WorkGroup_Create_Test()
        {
            WorkGroup_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void WorkGroup_UI_Create(WorkGroup WorkGroup)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert WorkGroup
            Create_WorkGroup_Model Create_WorkGroup_Model = new Create_WorkGroup_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Create_WorkGroup_Model(WorkGroup);

			var Name = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.Name)));
            Name.SendKeys(Create_WorkGroup_Model.Name.ToString());
			var Code = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.Code)));
            Code.SendKeys(Create_WorkGroup_Model.Code.ToString());
			var Description = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.Description)));
            Description.SendKeys(Create_WorkGroup_Model.Description.ToString());
			this.Select.SelectValue("President_FormerId", Create_WorkGroup_Model.President_FormerId.ToString());
			this.Select.SelectValue("President_TraineeId", Create_WorkGroup_Model.President_TraineeId.ToString());
			this.Select.SelectValue("President_AdministratorId", Create_WorkGroup_Model.President_AdministratorId.ToString());
			this.Select.SelectValue("VicePresident_FormerId", Create_WorkGroup_Model.VicePresident_FormerId.ToString());
			this.Select.SelectValue("VicePresident_TraineeId", Create_WorkGroup_Model.VicePresident_TraineeId.ToString());
			this.Select.SelectValue("VicePresident_AdministratorId", Create_WorkGroup_Model.VicePresident_AdministratorId.ToString());
			this.Select.SelectValue("Protractor_FormerId", Create_WorkGroup_Model.Protractor_FormerId.ToString());
			this.Select.SelectValue("Protractor_AdministratorId", Create_WorkGroup_Model.Protractor_AdministratorId.ToString());
			this.Select.SelectValue("Protractor_TraineeId", Create_WorkGroup_Model.Protractor_TraineeId.ToString());
			var Selected_MemebersFormers = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.Selected_MemebersFormers)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement_Selected_MemebersFormers = new OpenQA.Selenium.Support.UI.SelectElement(Selected_MemebersFormers);
            foreach (var item in Create_WorkGroup_Model.Selected_MemebersFormers)
            {
                selectElement_Selected_MemebersFormers.SelectByValue(item);
            }	 
			var Selected_MemebersAdministrators = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.Selected_MemebersAdministrators)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement_Selected_MemebersAdministrators = new OpenQA.Selenium.Support.UI.SelectElement(Selected_MemebersAdministrators);
            foreach (var item in Create_WorkGroup_Model.Selected_MemebersAdministrators)
            {
                selectElement_Selected_MemebersAdministrators.SelectByValue(item);
            }	 
			var Selected_MemebersTrainees = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.Selected_MemebersTrainees)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement_Selected_MemebersTrainees = new OpenQA.Selenium.Support.UI.SelectElement(Selected_MemebersTrainees);
            foreach (var item in Create_WorkGroup_Model.Selected_MemebersTrainees)
            {
                selectElement_Selected_MemebersTrainees.SelectByValue(item);
            }	 
			var GuestFormers = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.GuestFormers)));
			if (Create_WorkGroup_Model.GuestFormers)
                GuestFormers.Click();
			var GuestTrainees = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.GuestTrainees)));
			if (Create_WorkGroup_Model.GuestTrainees)
                GuestTrainees.Click();
			var GuestAdministrator = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.GuestAdministrator)));
			if (Create_WorkGroup_Model.GuestAdministrator)
                GuestAdministrator.Click();
			var Selected_Mission_Working_Groups = b.FindElement(By.Id(nameof(Create_WorkGroup_Model.Selected_Mission_Working_Groups)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement_Selected_Mission_Working_Groups = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Mission_Working_Groups);
            foreach (var item in Create_WorkGroup_Model.Selected_Mission_Working_Groups)
            {
                selectElement_Selected_Mission_Working_Groups.SelectByValue(item);
            }	 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_WorkGroup_UI_Tests : Base_Create_WorkGroup_UI_Tests
    {
		public Create_WorkGroup_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_WorkGroup_UI_Tests() : base(null){}
    }
}
