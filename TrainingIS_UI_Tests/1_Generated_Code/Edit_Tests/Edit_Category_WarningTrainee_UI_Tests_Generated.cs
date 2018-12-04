using System;
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

namespace TrainingIS_UI_Tests.Category_WarningTrainees
{
    [TestCategory("Edit_UI_Test")]
	[TestCategory("Category_WarningTrainee")]
    public class Base_Edit_Category_WarningTrainee_UI_Tests : Base_Edit_Index_Entity_UI_Test<Category_WarningTrainee>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public Category_WarningTraineeTestDataFactory Category_WarningTrainee_TestData { set; get; }
        public Category_WarningTraineeBLO Category_WarningTraineeBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Category_WarningTrainees";
            this.Entity_Reference = "Category_WarningTrainee_CRUD_Test";

			// TestData and BLO
			Category_WarningTrainee_TestData = new Category_WarningTraineeTestDataFactory(this.UnitOfWork, this.GAppContext);
            Category_WarningTraineeBLO = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Category_WarningTrainee_TestData.CreateValideCategory_WarningTraineeInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Edit_Category_WarningTrainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Category_WarningTrainee Create_Data_Test = Category_WarningTraineeBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                Category_WarningTraineeBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Category_WarningTrainee_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Category_WarningTrainee_Edit_Test()
        {
            // Arrange
            // Add Category_WarningTrainee to be Edited
            this.Category_WarningTraineeBLO.Save(this.Valide_Entity_Instance);


            this.GoTo_Index_And_Login_If_Not_Ahenticated();


            // Search the created entity
            this.DataTable.Search(this.Valide_Entity_Instance.Reference);

            // Edit the entity
            this.DataTable.Init("Category_WarningTrainees_Entities");
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
	
	public partial class Edit_Category_WarningTrainee_UI_Tests : Base_Edit_Category_WarningTrainee_UI_Tests
    {
		public Edit_Category_WarningTrainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Edit_Category_WarningTrainee_UI_Tests() : base(null){}
    }
}
