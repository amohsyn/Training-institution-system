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
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.Category_WarningTrainees
{
    public class Base_Create_Category_WarningTrainee_UI_Tests : Create_Entity_UI_Test<Category_WarningTrainee>
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
        }

		public Base_Create_Category_WarningTrainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
		/// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                Category_WarningTrainee_TestData.Insert_Test_Data_If_Not_Exist();
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
        public virtual void Category_WarningTrainee_Create_Test()
        {
            Category_WarningTrainee_UI_Create(this.Valide_Entity_Insrance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void Category_WarningTrainee_UI_Create(Category_WarningTrainee Category_WarningTrainee)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Category_WarningTrainee
            Default_Form_Category_WarningTrainee_Model Default_Form_Category_WarningTrainee_Model = new Default_Form_Category_WarningTrainee_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Category_WarningTrainee_Model(Category_WarningTrainee);

			var Name = b.FindElement(By.Id(nameof(Default_Form_Category_WarningTrainee_Model.Name)));
            Name.SendKeys(Default_Form_Category_WarningTrainee_Model.Name.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_Form_Category_WarningTrainee_Model.Description)));
            Description.SendKeys(Default_Form_Category_WarningTrainee_Model.Description.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	public partial class Create_Category_WarningTrainee_UI_Tests : Base_Create_Category_WarningTrainee_UI_Tests
    {
		public Create_Category_WarningTrainee_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Category_WarningTrainee_UI_Tests() : base(null){}
    }
}
