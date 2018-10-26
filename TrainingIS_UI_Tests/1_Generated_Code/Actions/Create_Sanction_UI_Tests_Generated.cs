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

namespace TrainingIS_UI_Tests.Sanctions
{
    public class Base_Create_Sanction_UI_Tests : Create_Entity_UI_Test<Sanction>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Sanctions";
            this.Entity_Reference = "Sanction_CRUD_Test";
        }

		public Base_Create_Sanction_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void Sanction_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Sanction_Create_Test()
        {
            Sanction_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Sanction_Create(Sanction Sanction)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Sanction
            Default_Form_Sanction_Model Default_Form_Sanction_Model = new Default_Form_Sanction_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_Sanction_Model(Sanction);



			this.Select.SelectValue("TraineeId", Default_Form_Sanction_Model.TraineeId.ToString());

			this.Select.SelectValue("SanctionCategoryId", Default_Form_Sanction_Model.SanctionCategoryId.ToString());

			this.Select.SelectValue("MeetingId", Default_Form_Sanction_Model.MeetingId.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new SanctionTestDataFactory(null, this.GAppContext).CreateValideSanctionInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Sanction_CRUD_Test if Exist
            SanctionBLO SanctionBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);
            Sanction existante_entity = SanctionBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                SanctionBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Sanction_UI_Tests : Base_Create_Sanction_UI_Tests
    {
		public Create_Sanction_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Sanction_UI_Tests() : base(null){}
    }
}
