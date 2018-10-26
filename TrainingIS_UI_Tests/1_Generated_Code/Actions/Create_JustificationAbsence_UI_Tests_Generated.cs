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

namespace TrainingIS_UI_Tests.JustificationAbsences
{
    public class Base_Create_JustificationAbsence_UI_Tests : Create_Entity_UI_Test<JustificationAbsence>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public JustificationAbsenceTestDataFactory JustificationAbsence_TestData { set; get; }
        public JustificationAbsenceBLO JustificationAbsenceBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/JustificationAbsences";
            this.Entity_Reference = "JustificationAbsence_CRUD_Test";

			// TestData and BLO
			JustificationAbsence_TestData = new JustificationAbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
            JustificationAbsenceBLO = new JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);
        }

		public Base_Create_JustificationAbsence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
		/// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                JustificationAbsence_TestData.Insert_Test_Data_If_Not_Exist();
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
           JustificationAbsence Create_Data_Test = JustificationAbsenceBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                JustificationAbsenceBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void JustificationAbsence_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void JustificationAbsence_Create_Test()
        {
            JustificationAbsence_UI_Create(this.Valide_Entity_Insrance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void JustificationAbsence_UI_Create(JustificationAbsence JustificationAbsence)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert JustificationAbsence
            Default_Form_JustificationAbsence_Model Default_Form_JustificationAbsence_Model = new Default_Form_JustificationAbsence_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_JustificationAbsence_Model(JustificationAbsence);

			this.Select.SelectValue("TraineeId", Default_Form_JustificationAbsence_Model.TraineeId.ToString());
			this.Select.SelectValue("Category_JustificationAbsenceId", Default_Form_JustificationAbsence_Model.Category_JustificationAbsenceId.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_Form_JustificationAbsence_Model.StartDate), Default_Form_JustificationAbsence_Model.StartDate.ToString());
			
			this.DateTimePicker.SelectDate(nameof(Default_Form_JustificationAbsence_Model.EndtDate), Default_Form_JustificationAbsence_Model.EndtDate.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_Form_JustificationAbsence_Model.Description)));
            Description.SendKeys(Default_Form_JustificationAbsence_Model.Description.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_Form_JustificationAbsence_Model.Reference)));
            Reference.SendKeys(Default_Form_JustificationAbsence_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	public partial class Create_JustificationAbsence_UI_Tests : Base_Create_JustificationAbsence_UI_Tests
    {
		public Create_JustificationAbsence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_JustificationAbsence_UI_Tests() : base(null){}
    }
}
