using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestData;
using TrainingIS.BLL.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL;
using TrainingIS.BLL.Services.Import;
using GApp.Models.Pages;
using TrainingIS.Entities;
using TrainingIS.BLL.ModelsViews;
using System.Reflection;
using System.Data;
using GApp.Entities;

using GApp.Entities.Resources.ControllerAppResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_ControllerAppBLOTests : Base_BLO_Tests
    {
        public ControllerAppTestDataFactory ControllerApp_TestData { set; get; }
		public ControllerAppBLO ControllerAppBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_ControllerAppBLOTests()
        {
            ControllerApp_TestData = new ControllerAppTestDataFactory(this.UnitOfWork, this.GAppContext);
            ControllerAppBLO = new ControllerAppBLO(this.UnitOfWork, this.GAppContext);
        }
 

        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                this.CleanData();
                InitData_Initlizalize = true;
            }

        }

        [TestCleanup]
        public virtual void CleanData()
        {
            // Clean Create Data Test
            ControllerApp Create_Data_Test = ControllerAppBLO.FindBaseEntityByReference(this.ControllerApp_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                ControllerAppBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_ControllerApp_Test()
        {
            // BLO
            ControllerAppBLO sanctionBLO = new ControllerAppBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("ControllerAppsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[ControllerAppState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_ControllerApp
            ExportService exportService = new ExportService(typeof(ControllerApp), typeof(Default_ControllerApp_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "ControllerAppsController");
            var data = new Default_ControllerApp_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_ControllerApp_Export_Model First_Exptected_ControllerApp = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_ControllerApp);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_ControllerApp)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class ControllerAppBLOTests : Base_ControllerAppBLOTests
    {

    }
}
