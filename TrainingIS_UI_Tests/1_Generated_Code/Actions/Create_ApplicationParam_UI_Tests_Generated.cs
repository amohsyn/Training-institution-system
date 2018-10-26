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

namespace TrainingIS_UI_Tests.ApplicationParams
{
    public class Base_Create_ApplicationParam_UI_Tests : Create_Entity_UI_Test<ApplicationParam>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/ApplicationParams";
            this.Entity_Reference = "ApplicationParam_CRUD_Test";
        }

		public Base_Create_ApplicationParam_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void ApplicationParam_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void ApplicationParam_Create_Test()
        {
            ApplicationParam_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void ApplicationParam_Create(ApplicationParam ApplicationParam)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert ApplicationParam
            Default_Form_ApplicationParam_Model Default_Form_ApplicationParam_Model = new Default_Form_ApplicationParam_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_ApplicationParam_Model(ApplicationParam);



	 


 
			var Code = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Code)));
            Code.SendKeys(Default_Form_ApplicationParam_Model.Code.ToString());

	 


 
			var Name = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Name)));
            Name.SendKeys(Default_Form_ApplicationParam_Model.Name.ToString());

	 


 
			var Value = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Value)));
            Value.SendKeys(Default_Form_ApplicationParam_Model.Value.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_ApplicationParam_Model.Description)));
            Description.SendKeys(Default_Form_ApplicationParam_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new ApplicationParamTestDataFactory(null, this.GAppContext).CreateValideApplicationParamInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete ApplicationParam_CRUD_Test if Exist
            ApplicationParamBLO ApplicationParamBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);
            ApplicationParam existante_entity = ApplicationParamBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                ApplicationParamBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_ApplicationParam_UI_Tests : Base_Create_ApplicationParam_UI_Tests
    {
		public Create_ApplicationParam_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_ApplicationParam_UI_Tests() : base(null){}
    }
}
