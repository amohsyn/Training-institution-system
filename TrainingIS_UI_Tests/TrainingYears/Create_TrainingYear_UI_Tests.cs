using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.UnitTest.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TestData;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.TrainingYears
{
    public partial class Create_TrainingYear_UI_Tests
    {
        public bool InitData_Initlizalize = false;
        public bool ClassCleanup = false;

        public TrainingYearTestDataFactory trainingYear_TestData { set; get; }
        public TrainingYearBLO TrainingYearBLO  { set; get; }
        public string Reference_TrainingYear_Create_TestData = null;

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            trainingYear_TestData = new TrainingYearTestDataFactory(this.UnitOfWork, this.GAppContext);
            TrainingYearBLO = new TrainingYearBLO(this.UnitOfWork, this.GAppContext);
        }

        /// <summary>
        /// InitData well be executed one time for all TestMethod
        /// </summary>
        [TestInitialize]
        public override void InitData()
        {
            if (!InitData_Initlizalize)
            {
                trainingYear_TestData.Insert_Test_Data_If_Not_Exist();
                this.CleanData();
                InitData_Initlizalize = true;
            }
           
        }

        /// <summary>
        /// CleanData well be executed after each TestMethod
        /// </summary>
        [TestCleanup]
        public override void CleanData()
        {
            // Clean Create Data Test
            TrainingYear Create_Data_Test = TrainingYearBLO.FindBaseEntityByReference(Reference_TrainingYear_Create_TestData);
            if (Create_Data_Test != null)
                TrainingYearBLO.Delete(Create_Data_Test);
        }

        [TestMethod]
        public override void TrainingYear_Create_Test()
        {
            var Create_Data_Test = trainingYear_TestData.CreateValideTrainingYearInstance();
            Create_Data_Test.Reference = "Create_Data_Test";
            Create_Data_Test.StartDate =  Convert.ToDateTime("1/9/2022");
            Create_Data_Test.EndtDate = Convert.ToDateTime("31/8/2023");
            this.Reference_TrainingYear_Create_TestData = Create_Data_Test.CalculateReference();
            TrainingYear_Create(Create_Data_Test);
        }

        public override void TrainingYear_Create(TrainingYear TrainingYear)
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Index create click Test
            var CreateElement = b.FindElement(By.Id("Create_New_Entity"));
            CreateElement.Click();

            // Insert TrainingYear
            Default_Form_TrainingYear_Model Default_Form_TrainingYear_Model = new Default_Form_TrainingYear_ModelBLM(this.UnitOfWork, this.GAppContext)
                .ConverTo_Default_Form_TrainingYear_Model(TrainingYear);

            var Code = b.FindElement(By.Id(nameof(Default_Form_TrainingYear_Model.Code)));
            Code.SendKeys(Default_Form_TrainingYear_Model.Code.ToString());

            this.DateTimePicker.SelectDate(nameof(Default_Form_TrainingYear_Model.StartDate), Default_Form_TrainingYear_Model.StartDate.ToString());

            this.DateTimePicker.SelectDate(nameof(Default_Form_TrainingYear_Model.EndtDate), Default_Form_TrainingYear_Model.EndtDate.ToString());

            var Create_Entity_Form = b.FindElement(By.Id("Create_Entity_Form"));
            Create_Entity_Form.Submit();

            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());
        }

        [TestMethod]
        public void Create_TrainingYear_with_existant_period_Test()
        {
            throw new NotImplementedException();
        }
    }
}
