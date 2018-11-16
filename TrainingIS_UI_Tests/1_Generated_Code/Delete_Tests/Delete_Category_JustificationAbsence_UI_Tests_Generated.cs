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

namespace TrainingIS_UI_Tests.Category_JustificationAbsences
{
    [TestCategory("Delete_UI_Test")]
    public class Base_Delete_Category_JustificationAbsence_UI_Tests : Base_Create_Entity_UI_Test<Category_JustificationAbsence>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public Category_JustificationAbsenceTestDataFactory Category_JustificationAbsence_TestData { set; get; }
        public Category_JustificationAbsenceBLO Category_JustificationAbsenceBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Category_JustificationAbsences";
            this.Entity_Reference = "Category_JustificationAbsence_CRUD_Test";

			// TestData and BLO
			Category_JustificationAbsence_TestData = new Category_JustificationAbsenceTestDataFactory(this.UnitOfWork, this.GAppContext);
            Category_JustificationAbsenceBLO = new Category_JustificationAbsenceBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Category_JustificationAbsence_TestData.CreateValideCategory_JustificationAbsenceInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Delete_Category_JustificationAbsence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Category_JustificationAbsence Create_Data_Test = Category_JustificationAbsenceBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                Category_JustificationAbsenceBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Category_JustificationAbsence_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Category_JustificationAbsence_Delete_Test()
        {
            // Arrange
            // Add Category_JustificationAbsence to be delete
            this.Category_JustificationAbsenceBLO.Save(this.Valide_Entity_Instance);

            // Delete entity
            this.GoTo_Index_And_Login_If_Not_Ahenticated();


            // Search the created entity
            this.Html.GetElement("Search_GAppDataTable").SendKeys(this.Valide_Entity_Instance.Reference);
            this.Ajax.WaitForAjax();

            // Delete the entity
            this.DataTable.Init("DisciplineCategories_Entities");
            this.DataTable.Lines[0].Delete_Element.Click();

            // Confirm Delete
            this.Html.Click("Delete_Entity_Confirm");

            // Assert
            this.IndexPage.Is_In_IndexPage();
            this.Alert.Is_Info_Alert();

        }
    }

    [TestClass]
	
	public partial class Delete_Category_JustificationAbsence_UI_Tests : Base_Delete_Category_JustificationAbsence_UI_Tests
    {
		public Delete_Category_JustificationAbsence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Delete_Category_JustificationAbsence_UI_Tests() : base(null){}
    }
}
