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

namespace TrainingIS_UI_Tests.ApplicationParams
{
    [TestCategory("Delete_UI_Test")]
	[TestCategory("ApplicationParam")]
    public class Base_Delete_ApplicationParam_UI_Tests : Base_Create_Entity_UI_Test<ApplicationParam>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public ApplicationParamTestDataFactory ApplicationParam_TestData { set; get; }
        public ApplicationParamBLO ApplicationParamBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/ApplicationParams";
            this.Entity_Reference = "ApplicationParam_CRUD_Test";

			// TestData and BLO
			ApplicationParam_TestData = new ApplicationParamTestDataFactory(this.UnitOfWork, this.GAppContext);
            ApplicationParamBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = ApplicationParam_TestData.Create_CRUD_ApplicationParam_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Delete_ApplicationParam_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           ApplicationParam Create_Data_Test = ApplicationParamBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                ApplicationParamBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void ApplicationParam_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void ApplicationParam_Delete_Test()
        {
            // Arrange
            // Add ApplicationParam to be delete
            this.ApplicationParamBLO.Save(this.Valide_Entity_Instance);

            // Delete entity
            this.GoTo_Index_And_Login_If_Not_Ahenticated();


            // Search the created entity
            this.DataTable.Search(this.Valide_Entity_Instance.Reference);

            // Delete the entity
            this.DataTable.Init("ApplicationParams_Entities");
			Assert.AreEqual(this.DataTable.Lines[0].ObjectId, this.Valide_Entity_Instance.Id); 
            this.DataTable.Lines[0].Delete_Element.Click();

            // Confirm Delete
            this.Html.Click("Delete_Entity_Confirm");

            // Assert
			Assert.IsTrue(  this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());

        }
    }

    [TestClass]
	
	public partial class Delete_ApplicationParam_UI_Tests : Base_Delete_ApplicationParam_UI_Tests
    {
		public Delete_ApplicationParam_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Delete_ApplicationParam_UI_Tests() : base(null){}
    }
}
