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

namespace TrainingIS_UI_Tests.RoleApps
{
    [TestCategory("Create_UI_Test")]
    [TestCategory("RoleApp")]
    public class Base_Create_RoleApp_UI_Tests : Base_Create_Entity_UI_Test<RoleApp>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public RoleAppTestDataFactory RoleApp_TestData { set; get; }
        public RoleAppBLO RoleAppBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/RoleApps";
            this.Entity_Reference = "RoleApp_CRUD_Test";

			// TestData and BLO
			RoleApp_TestData = new RoleAppTestDataFactory(this.UnitOfWork, this.GAppContext);
            RoleAppBLO = new RoleAppBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = RoleApp_TestData.Create_CRUD_RoleApp_Test_Instance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_RoleApp_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           RoleApp Create_Data_Test = RoleAppBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                RoleAppBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void RoleApp_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void RoleApp_Create_Test()
        {
            RoleApp_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void RoleApp_UI_Create(RoleApp RoleApp)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert RoleApp
            Default_RoleApp_Create_Model Default_RoleApp_Create_Model = new Default_RoleApp_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_RoleApp_Create_Model(RoleApp);

			var Code = b.FindElement(By.Id(nameof(Default_RoleApp_Create_Model.Code)));
            Code.SendKeys(Default_RoleApp_Create_Model.Code.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_RoleApp_Create_Model.Description)));
            Description.SendKeys(Default_RoleApp_Create_Model.Description.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_RoleApp_Create_Model.Reference)));
            Reference.SendKeys(Default_RoleApp_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_RoleApp_UI_Tests : Base_Create_RoleApp_UI_Tests
    {
		public Create_RoleApp_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_RoleApp_UI_Tests() : base(null){}
    }
}
