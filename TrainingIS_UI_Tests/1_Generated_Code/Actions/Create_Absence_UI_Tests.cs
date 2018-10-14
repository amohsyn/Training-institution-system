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
using TrainingIS.Models.Absences;

namespace TrainingIS_UI_Tests.Absences
{
    public class Base_Create_Absence_UI_Tests : Create_Entity_UI_Test<Absence>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/Absences";
            this.Entity_Reference = "Absence_CRUD_Test";
        }

		public Base_Create_Absence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void Absence_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void Absence_Create_Test()
        {
            Absence_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void Absence_Create(Absence Absence)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert Absence
            Create_Absence_Model Create_Absence_Model = new Create_Absence_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Create_Absence_Model(Absence);



			this.Select.SelectValue("TraineeId", Create_Absence_Model.TraineeId.ToString());

			var isHaveAuthorization = b.FindElement(By.Id(nameof(Create_Absence_Model.isHaveAuthorization)));
			if (Create_Absence_Model.isHaveAuthorization)
                isHaveAuthorization.Click();

			this.Select.SelectValue("SeanceTrainingId", Create_Absence_Model.SeanceTrainingId.ToString());

	 


 
			var FormerComment = b.FindElement(By.Id(nameof(Create_Absence_Model.FormerComment)));
            FormerComment.SendKeys(Create_Absence_Model.FormerComment.ToString());

	 


 
			var TraineeComment = b.FindElement(By.Id(nameof(Create_Absence_Model.TraineeComment)));
            TraineeComment.SendKeys(Create_Absence_Model.TraineeComment.ToString());

	 


 
			var SupervisorComment = b.FindElement(By.Id(nameof(Create_Absence_Model.SupervisorComment)));
            SupervisorComment.SendKeys(Create_Absence_Model.SupervisorComment.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new AbsenceTestDataFactory(null, this.GAppContext).CreateValideAbsenceInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete Absence_CRUD_Test if Exist
            AbsenceBLO AbsenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            Absence existante_entity = AbsenceBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                AbsenceBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_Absence_UI_Tests : Base_Create_Absence_UI_Tests
    {
		public Create_Absence_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_Absence_UI_Tests() : base(null){}
    }
}
