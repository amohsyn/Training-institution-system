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

namespace TrainingIS_UI_Tests.ModuleTrainings
{
    [TestCategory("Create_UI_Test")]
    public class Base_Create_ModuleTraining_UI_Tests : Create_Entity_UI_Test<ModuleTraining>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public ModuleTrainingTestDataFactory ModuleTraining_TestData { set; get; }
        public ModuleTrainingBLO ModuleTrainingBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/ModuleTrainings";
            this.Entity_Reference = "ModuleTraining_CRUD_Test";

			// TestData and BLO
			ModuleTraining_TestData = new ModuleTrainingTestDataFactory(this.UnitOfWork, this.GAppContext);
            ModuleTrainingBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = ModuleTraining_TestData.CreateValideModuleTrainingInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Create_ModuleTraining_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           ModuleTraining Create_Data_Test = ModuleTrainingBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                ModuleTrainingBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void ModuleTraining_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void ModuleTraining_Create_Test()
        {
            ModuleTraining_UI_Create(this.Valide_Entity_Instance);
			Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }
 
        public virtual void ModuleTraining_UI_Create(ModuleTraining ModuleTraining)
        {
			this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert ModuleTraining
            Default_ModuleTraining_Create_Model Default_ModuleTraining_Create_Model = new Default_ModuleTraining_Create_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_ModuleTraining_Create_Model(ModuleTraining);

			this.Select.SelectValue("SpecialtyId", Default_ModuleTraining_Create_Model.SpecialtyId.ToString());
			this.Select.SelectValue("MetierId", Default_ModuleTraining_Create_Model.MetierId.ToString());
			this.Select.SelectValue("YearStudyId", Default_ModuleTraining_Create_Model.YearStudyId.ToString());
			var Name = b.FindElement(By.Id(nameof(Default_ModuleTraining_Create_Model.Name)));
            Name.SendKeys(Default_ModuleTraining_Create_Model.Name.ToString());
			var Code = b.FindElement(By.Id(nameof(Default_ModuleTraining_Create_Model.Code)));
            Code.SendKeys(Default_ModuleTraining_Create_Model.Code.ToString());
			var HourlyMass = b.FindElement(By.Id(nameof(Default_ModuleTraining_Create_Model.HourlyMass)));
            HourlyMass.SendKeys(Default_ModuleTraining_Create_Model.HourlyMass.ToString());
			var Hourly_Mass_To_Teach = b.FindElement(By.Id(nameof(Default_ModuleTraining_Create_Model.Hourly_Mass_To_Teach)));
            Hourly_Mass_To_Teach.SendKeys(Default_ModuleTraining_Create_Model.Hourly_Mass_To_Teach.ToString());
			var Description = b.FindElement(By.Id(nameof(Default_ModuleTraining_Create_Model.Description)));
            Description.SendKeys(Default_ModuleTraining_Create_Model.Description.ToString());
			var Reference = b.FindElement(By.Id(nameof(Default_ModuleTraining_Create_Model.Reference)));
            Reference.SendKeys(Default_ModuleTraining_Create_Model.Reference.ToString());
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();
        }
    }

    [TestClass]
	
	public partial class Create_ModuleTraining_UI_Tests : Base_Create_ModuleTraining_UI_Tests
    {
		public Create_ModuleTraining_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_ModuleTraining_UI_Tests() : base(null){}
    }
}
