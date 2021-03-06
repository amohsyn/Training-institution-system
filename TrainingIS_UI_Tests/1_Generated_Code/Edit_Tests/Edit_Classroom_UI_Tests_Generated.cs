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

namespace TrainingIS_UI_Tests.Classrooms
{
    [TestCategory("Edit_UI_Test")]
	[TestCategory("Classroom")]
    public class Base_Edit_Classroom_UI_Tests : Base_Edit_Index_Entity_UI_Test<Classroom>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public ClassroomTestDataFactory Classroom_TestData { set; get; }
        public ClassroomBLO ClassroomBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Classrooms";
            this.Entity_Reference = "Classroom_CRUD_Test";

			// TestData and BLO
			Classroom_TestData = new ClassroomTestDataFactory(this.UnitOfWork, this.GAppContext);
            ClassroomBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Classroom_TestData.Create_CRUD_Classroom_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Edit_Classroom_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Classroom Create_Data_Test = ClassroomBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                ClassroomBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Classroom_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Classroom_Edit_Test()
        {
            // Arrange
            // Add Classroom to be Edited
            this.ClassroomBLO.Save(this.Valide_Entity_Instance);


            this.GoTo_Index_And_Login_If_Not_Ahenticated();


            // Search the created entity
            this.DataTable.Search(this.Valide_Entity_Instance.Reference);

            // Edit the entity
            this.DataTable.Init("Classrooms_Entities");
			Assert.AreEqual(this.DataTable.Lines.Count, 1);
            this.DataTable.Lines[0].Edit_Element.Click();

            // Submit Edit Form
            this.Html.Click("Edit_Entity_Submit");

            // Assert
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

    }

    [TestClass]
	
	public partial class Edit_Classroom_UI_Tests : Base_Edit_Classroom_UI_Tests
    {
		public Edit_Classroom_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Edit_Classroom_UI_Tests() : base(null){}
    }
}
