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

namespace TrainingIS_UI_Tests.LogWorks
{
    public class Base_Create_LogWork_UI_Tests : Create_Entity_UI_Test<LogWork>
    {
		// GApp Context
        public UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        public GAppContext GAppContext { set; get; }
        public TrainingYear CurrentTrainingYear { set; get; }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.ControllerName = "/LogWorks";
            this.Entity_Reference = "LogWork_CRUD_Test";
        }

		public Base_Create_LogWork_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
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
        public virtual void LogWork_Index_Show_Test()
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();
        }

		[TestMethod]
        public virtual void LogWork_Create_Test()
        {
            LogWork_Create(this.Valide_Entity_Insrance);
        }
 
        public virtual void LogWork_Create(LogWork LogWork)
        {
             this.GoTo_Index_And_Login_If_Not_Ahenticated();

			GAppContext GAppContext = new GAppContext("Root");

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert LogWork
            Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_ModelBLM(new UnitOfWork<TrainingISModel>(),GAppContext)
                .ConverTo_Default_Form_LogWork_Model(LogWork);



	 


 
			var UserId = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.UserId)));
            UserId.SendKeys(Default_Form_LogWork_Model.UserId.ToString());

			this.Select.SelectValue("OperationWorkType", Convert.ToInt32(Default_Form_LogWork_Model.OperationWorkType).ToString());

	 


 
			var OperationReference = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.OperationReference)));
            OperationReference.SendKeys(Default_Form_LogWork_Model.OperationReference.ToString());

	 


 
			var EntityType = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.EntityType)));
            EntityType.SendKeys(Default_Form_LogWork_Model.EntityType.ToString());

	 


 
			var Description = b.FindElement(By.Id(nameof(Default_Form_LogWork_Model.Description)));
            Description.SendKeys(Default_Form_LogWork_Model.Description.ToString());
 
            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

		[TestInitialize]
        public virtual void InitData()
        {
            this.CleanData();
            this.Valide_Entity_Insrance = new LogWorkTestDataFactory(null, this.GAppContext).CreateValideLogWorkInstance();
            this.Valide_Entity_Insrance.Reference = this.Entity_Reference;
        }

		[TestCleanup]
        public override void CleanData()
        {
            base.CleanData();
            // Delete LogWork_CRUD_Test if Exist
            LogWorkBLO LogWorkBLO = new LogWorkBLO(this.UnitOfWork, this.GAppContext);
            LogWork existante_entity = LogWorkBLO.FindBaseEntityByReference(this.Entity_Reference);
            if (existante_entity != null)
                LogWorkBLO.Delete(existante_entity);

        }

    }

    [TestClass]
	public partial class Create_LogWork_UI_Tests : Base_Create_LogWork_UI_Tests
    {
		public Create_LogWork_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context){}
        public Create_LogWork_UI_Tests() : base(null){}
    }
}
