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

namespace TrainingIS_UI_Tests.Mission_Working_Groups
{
    public class Base_Create_Mission_Working_Group_UI_Tests : Create_Entity_UI_Test<Mission_Working_Group>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Mission_Working_Groups";
            this.Entity_Reference = "Mission_Working_Group_CRUD_Test";
        }

		public Base_Create_Mission_Working_Group_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
		{
            //
            // GApp Context
            //
            this.UnitOfWork = new UnitOfWork<TrainingISModel>();
            this.GAppContext = new GAppContext(RoleBLO.Root_ROLE);
            TrainingYear CurrentTrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).getCurrentTrainingYear();
            this.GAppContext.Session.Add(UnitOfWorkBLO.UnitOfWork_Key, this.UnitOfWork);
            this.GAppContext.Session.Add(TrainingYearBLO.Current_TrainingYear_Key, CurrentTrainingYear);

        }
 
        
        [TestMethod]
        public virtual void Mission_Working_Group_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Mission_Working_Group_Create_Test()
        {
            Mission_Working_Group_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Mission_Working_Group_Create(Mission_Working_Group Mission_Working_Group)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Mission_Working_Group
            Default_Form_Mission_Working_Group_Model Default_Form_Mission_Working_Group_Model = new Default_Form_Mission_Working_Group_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Mission_Working_Group_Model(Mission_Working_Group);



	 


 
			var Code = b.FindElement(By.Id(nameof(Default_Form_Mission_Working_Group_Model.Code)));
            Code.SendKeys(Default_Form_Mission_Working_Group_Model.Code.ToString());

	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_Mission_Working_Group_Model.Name)));
            Name.SendKeys(Default_Form_Mission_Working_Group_Model.Name.ToString());

			this.Select.SelectValue("DecisionAuthority", Convert.ToInt32(Default_Form_Mission_Working_Group_Model.DecisionAuthority).ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_Mission_Working_Group_Model.Description)));
            Description.SendKeys(Default_Form_Mission_Working_Group_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new Mission_Working_GroupTestDataFactory(null, this.GAppContext).CreateValideMission_Working_GroupInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Mission_Working_Group_CRUD_Test if Exist
            Mission_Working_GroupBLO Mission_Working_GroupBLO = new Mission_Working_GroupBLO(this.UnitOfWork, this.GAppContext);
            Mission_Working_Group existante_entity = Mission_Working_GroupBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                Mission_Working_GroupBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Mission_Working_Group_UI_Tests : Base_Create_Mission_Working_Group_UI_Tests
    {
		public Create_Mission_Working_Group_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Mission_Working_Group_UI_Tests() : base(null){}
    }
}
