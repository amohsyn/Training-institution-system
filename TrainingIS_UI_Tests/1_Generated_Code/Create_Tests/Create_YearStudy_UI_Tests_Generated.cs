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

namespace TrainingIS_UI_Tests.YearStudies
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("YearStudy")]
    public class Base_Create_YearStudy_UI_Tests : Base_Create_Entity_UI_Test<YearStudy>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public YearStudyTestDataFactory YearStudy_TestData { set; get; }
        public YearStudyBLO YearStudyBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/YearStudies";
            this.Entity_Reference = "YearStudy_CRUD_Test";

			// TestData and BLO
			YearStudy_TestData = new YearStudyTestDataFactory(this.UnitOfWork, this.GAppContext);
            YearStudyBLO = new YearStudyBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = YearStudy_TestData.Create_CRUD_YearStudy_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_YearStudy_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           YearStudy Create_Data_Test = YearStudyBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                YearStudyBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void YearStudy_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void YearStudy_Create_Test()
        {
            YearStudy_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void YearStudy_UI_Create(YearStudy YearStudy)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert YearStudy
            Default_YearStudy_Create_Model Default_YearStudy_Create_Model = new Default_YearStudy_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_YearStudy_Create_Model(YearStudy);

			var Code = b.FindElement(By.Id(nameof(Default_YearStudy_Create_Model.Code)));
            Code.SendKeys(Default_YearStudy_Create_Model.Code.ToString());
			var Name = b.FindElement(By.Id(nameof(Default_YearStudy_Create_Model.Name)));
            Name.SendKeys(Default_YearStudy_Create_Model.Name.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_YearStudy_Create_Model.Description)));
            Description.SendKeys(Default_YearStudy_Create_Model.Description.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_YearStudy_Create_Model.Reference)));
            Reference.SendKeys(Default_YearStudy_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_YearStudy_UI_Tests : Base_Create_YearStudy_UI_Tests
    {
		public Create_YearStudy_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_YearStudy_UI_Tests() : base(null){}
    }
}
