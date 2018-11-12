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

namespace TrainingIS_UI_Tests.Mission_Working_Groups
{
    [TestCategory("Details_UI_Test")]
    public class Base_Details_Mission_Working_Group_UI_Tests : Create_Entity_UI_Test<Mission_Working_Group>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

		// Properties
		public bool InitData_Initlizalize = false;
        public Mission_Working_GroupTestDataFactory Mission_Working_Group_TestData { set; get; }
        public Mission_Working_GroupBLO Mission_Working_GroupBLO  { set; get; }
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
            this.UI_Test_Context.ControllerName = "/Mission_Working_Groups";
            this.Entity_Reference = "Mission_Working_Group_CRUD_Test";

			// TestData and BLO
			Mission_Working_Group_TestData = new Mission_Working_GroupTestDataFactory(this.UnitOfWork, this.GAppContext);
            Mission_Working_GroupBLO = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext);

			//  Init Valide_Entity_Instance
            this.Valide_Entity_Instance = Mission_Working_Group_TestData.CreateValideMission_Working_GroupInstance();
            this.Valide_Entity_Instance.Reference = this.Entity_Reference;
        }

		public Base_Details_Mission_Working_Group_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context) {}
 
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
           Mission_Working_Group Create_Data_Test = Mission_Working_GroupBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (Create_Data_Test != null)
                Mission_Working_GroupBLO.Delete(Create_Data_Test);
        }
        
     
        public virtual void Mission_Working_Group_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Mission_Working_Group_Details_Test()
        {
           
        }
    }

    [TestClass]
	
	public partial class Details_Mission_Working_Group_UI_Tests : Base_Details_Mission_Working_Group_UI_Tests
    {
		public Details_Mission_Working_Group_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Details_Mission_Working_Group_UI_Tests() : base(null){}
    }
}
