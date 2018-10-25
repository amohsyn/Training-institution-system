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
using TrainingIS.Models.WorkGroups;

namespace TrainingIS_UI_Tests.WorkGroups
{
    public class Base_Create_WorkGroup_UI_Tests : Create_Entity_UI_Test<WorkGroup>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/WorkGroups";
            this.Entity_Reference = "WorkGroup_CRUD_Test";
        }

		public Base_Create_WorkGroup_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void WorkGroup_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void WorkGroup_Create_Test()
        {
            WorkGroup_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void WorkGroup_Create(WorkGroup WorkGroup)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert WorkGroup
            Form_WorkGroup_Model Form_WorkGroup_Model = new Form_WorkGroup_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Form_WorkGroup_Model(WorkGroup);



	 


 
			var Name = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.Name)));
            Name.SendKeys(Form_WorkGroup_Model.Name.ToString());

	 


 
			var Code = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.Code)));
            Code.SendKeys(Form_WorkGroup_Model.Code.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.Description)));
            Description.SendKeys(Form_WorkGroup_Model.Description.ToString());

			this.Select.SelectValue("President_FormerId", Form_WorkGroup_Model.President_FormerId.ToString());

			this.Select.SelectValue("President_TraineeId", Form_WorkGroup_Model.President_TraineeId.ToString());

			this.Select.SelectValue("President_AdministratorId", Form_WorkGroup_Model.President_AdministratorId.ToString());

			this.Select.SelectValue("VicePresident_FormerId", Form_WorkGroup_Model.VicePresident_FormerId.ToString());

			this.Select.SelectValue("VicePresident_TraineeId", Form_WorkGroup_Model.VicePresident_TraineeId.ToString());

			this.Select.SelectValue("VicePresident_AdministratorId", Form_WorkGroup_Model.VicePresident_AdministratorId.ToString());

			this.Select.SelectValue("Protractor_FormerId", Form_WorkGroup_Model.Protractor_FormerId.ToString());

			this.Select.SelectValue("Protractor_AdministratorId", Form_WorkGroup_Model.Protractor_AdministratorId.ToString());

			this.Select.SelectValue("Protractor_TraineeId", Form_WorkGroup_Model.Protractor_TraineeId.ToString());

			var Selected_MemebersFormers = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.Selected_MemebersFormers)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_MemebersFormers);
            foreach (var item in Form_WorkGroup_Model.Selected_MemebersFormers)
            {
                selectElement.SelectByValue(item);
            }	 


			var Selected_MemebersAdministrators = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.Selected_MemebersAdministrators)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_MemebersAdministrators);
            foreach (var item in Form_WorkGroup_Model.Selected_MemebersAdministrators)
            {
                selectElement.SelectByValue(item);
            }	 


			var Selected_MemebersTrainees = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.Selected_MemebersTrainees)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_MemebersTrainees);
            foreach (var item in Form_WorkGroup_Model.Selected_MemebersTrainees)
            {
                selectElement.SelectByValue(item);
            }	 


			var GuestFormers = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.GuestFormers)));
			if (Form_WorkGroup_Model.GuestFormers)
                GuestFormers.Click();

			var GuestTrainees = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.GuestTrainees)));
			if (Form_WorkGroup_Model.GuestTrainees)
                GuestTrainees.Click();

			var GuestAdministrator = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.GuestAdministrator)));
			if (Form_WorkGroup_Model.GuestAdministrator)
                GuestAdministrator.Click();

			var Selected_Mission_Working_Groups = b.FindElement(By.Id(nameof(Form_WorkGroup_Model.Selected_Mission_Working_Groups)));
            OpenQA.Selenium.Support.UI.SelectElement selectElement = new OpenQA.Selenium.Support.UI.SelectElement(Selected_Mission_Working_Groups);
            foreach (var item in Form_WorkGroup_Model.Selected_Mission_Working_Groups)
            {
                selectElement.SelectByValue(item);
            }	 

 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new WorkGroupTestDataFactory(null, this.GAppContext).CreateValideWorkGroupInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete WorkGroup_CRUD_Test if Exist
            WorkGroupBLO WorkGroupBLO = new WorkGroupBLO(this.UnitOfWork, this.GAppContext);
            WorkGroup existante_entity = WorkGroupBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                WorkGroupBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_WorkGroup_UI_Tests : Base_Create_WorkGroup_UI_Tests
    {
		public Create_WorkGroup_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_WorkGroup_UI_Tests() : base(null){}
    }
}
