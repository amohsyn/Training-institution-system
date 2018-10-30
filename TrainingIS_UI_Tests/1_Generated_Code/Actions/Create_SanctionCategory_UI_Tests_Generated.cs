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
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.SanctionCategories
{
    public class Base_Create_SanctionCategory_UI_Tests : Create_Entity_UI_Test<SanctionCategory>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public SanctionCategoryTestDataFactory SanctionCategory_TestData { set; get; }
        public SanctionCategoryBLO SanctionCategoryBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/SanctionCategories";
            this.Entity_Reference = "SanctionCategory_CRUD_Test";

			// TestData and BLO
			SanctionCategory_TestData = new SanctionCategoryTestDataFactory(this.UnitOfWork, this.GAppContext);
            SanctionCategoryBLO = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = SanctionCategory_TestData.CreateValideSanctionCategoryInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_SanctionCategory_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
		/// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                SanctionCategory_TestData.Insert_Test_Data_If_Not_Exist();
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
           SanctionCategory Create_Data_Test = SanctionCategoryBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                SanctionCategoryBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void SanctionCategory_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void SanctionCategory_Create_Test()
        {
            SanctionCategory_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void SanctionCategory_UI_Create(SanctionCategory SanctionCategory)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert SanctionCategory
            Default_Form_SanctionCategory_Model Default_Form_SanctionCategory_Model = new Default_Form_SanctionCategory_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_SanctionCategory_Model(SanctionCategory);

			this.Select.SelectValue("DisciplineCategoryId", Default_Form_SanctionCategory_Model.DisciplineCategoryId.ToString());
			var Name = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.Name)));
            Name.SendKeys(Default_Form_SanctionCategory_Model.Name.ToString());
			var Code = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.Code)));
            Code.SendKeys(Default_Form_SanctionCategory_Model.Code.ToString());
			this.Select.SelectValue("DecisionAuthority", Convert.ToInt32(Default_Form_SanctionCategory_Model.DecisionAuthority).ToString());
			var WorkflowOrder = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.WorkflowOrder)));
            WorkflowOrder.SendKeys(Default_Form_SanctionCategory_Model.WorkflowOrder.ToString());
			var Number_Of_Days_Of_Exclusion = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.Number_Of_Days_Of_Exclusion)));
            Number_Of_Days_Of_Exclusion.SendKeys(Default_Form_SanctionCategory_Model.Number_Of_Days_Of_Exclusion.ToString());
			var Plurality_Of_Absences = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.Plurality_Of_Absences)));
            Plurality_Of_Absences.SendKeys(Default_Form_SanctionCategory_Model.Plurality_Of_Absences.ToString());
			var Deducted_Points = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.Deducted_Points)));
            Deducted_Points.SendKeys(Default_Form_SanctionCategory_Model.Deducted_Points.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.Description)));
            Description.SendKeys(Default_Form_SanctionCategory_Model.Description.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_Form_SanctionCategory_Model.Reference)));
            Reference.SendKeys(Default_Form_SanctionCategory_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	public partial class Create_SanctionCategory_UI_Tests : Base_Create_SanctionCategory_UI_Tests
    {
		public Create_SanctionCategory_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_SanctionCategory_UI_Tests() : base(null){}
    }
}
