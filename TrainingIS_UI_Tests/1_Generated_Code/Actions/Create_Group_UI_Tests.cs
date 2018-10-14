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
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS_UI_Tests.Groups
{
    public class Base_Create_Group_UI_Tests : Create_Entity_UI_Test<Group>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Groups";
            this.Entity_Reference = "Group_CRUD_Test";
        }

		public Base_Create_Group_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void Group_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Group_Create_Test()
        {
            Group_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Group_Create(Group Group)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Group
            CreateGroupView CreateGroupView = new CreateGroupViewBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_CreateGroupView(Group);



			this.Select.SelectValue("TrainingYearId", CreateGroupView.TrainingYearId.ToString());

			this.Select.SelectValue("SpecialtyId", CreateGroupView.SpecialtyId.ToString());

			this.Select.SelectValue("TrainingTypeId", CreateGroupView.TrainingTypeId.ToString());

			this.Select.SelectValue("YearStudyId", CreateGroupView.YearStudyId.ToString());

	 


 
			var Code = b.FindElement(By.Id(nameof(CreateGroupView.Code)));
            Code.SendKeys(CreateGroupView.Code.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new GroupTestDataFactory(null, this.GAppContext).CreateValideGroupInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Group_CRUD_Test if Exist
            GroupBLO GroupBLO = new GroupBLO(this.UnitOfWork, this.GAppContext);
            Group existante_entity = GroupBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                GroupBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Group_UI_Tests : Base_Create_Group_UI_Tests
    {
		public Create_Group_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Group_UI_Tests() : base(null){}
    }
}
