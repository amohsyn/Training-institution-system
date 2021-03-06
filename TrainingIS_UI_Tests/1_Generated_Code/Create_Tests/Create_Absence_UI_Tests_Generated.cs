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
using TrainingIS.Models.Absences;

namespace TrainingIS_UI_Tests.Absences
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("Absence")]
    public class Base_Create_Absence_UI_Tests : Base_Create_Entity_UI_Test<Absence>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public AbsenceTestDataFactory Absence_TestData { set; get; }
        public AbsenceBLO AbsenceBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Absences";
            this.Entity_Reference = "Absence_CRUD_Test";

			// TestData and BLO
			Absence_TestData = new AbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
            AbsenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Absence_TestData.Create_CRUD_Absence_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_Absence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Absence Create_Data_Test = AbsenceBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                AbsenceBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Absence_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Absence_Create_Test()
        {
            Absence_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void Absence_UI_Create(Absence Absence)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Absence
            Create_Absence_Model Create_Absence_Model = new Create_Absence_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Create_Absence_Model(Absence);

			this.Select.SelectValue("TraineeId", Create_Absence_Model.TraineeId.ToString());
			this.Select.SelectValue("AbsenceState", Convert.ToInt32(Create_Absence_Model.AbsenceState).ToString());
			this.Select.SelectValue("SeanceTrainingId", Create_Absence_Model.SeanceTrainingId.ToString());
			var FormerComment = b.FindElement(By.Id(nameof(Create_Absence_Model.FormerComment)));
            FormerComment.SendKeys(Create_Absence_Model.FormerComment.ToString());
			var TraineeComment = b.FindElement(By.Id(nameof(Create_Absence_Model.TraineeComment)));
            TraineeComment.SendKeys(Create_Absence_Model.TraineeComment.ToString());
			var SupervisorComment = b.FindElement(By.Id(nameof(Create_Absence_Model.SupervisorComment)));
            SupervisorComment.SendKeys(Create_Absence_Model.SupervisorComment.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_Absence_UI_Tests : Base_Create_Absence_UI_Tests
    {
		public Create_Absence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Absence_UI_Tests() : base(null){}
    }
}
