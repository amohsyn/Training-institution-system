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

using TrainingIS.Entities.Resources.TrainingYearResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_TrainingYearBLOTests : Base_BLO_Tests
    {
        public TrainingYearTestDataFactory TrainingYear_TestData { set; get; }
		public TrainingYearBLO TrainingYearBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_TrainingYearBLOTests()
        {
            TrainingYear_TestData = new TrainingYearTestDataFactory(this.UnitOfWork, this.GAppContext);
            TrainingYearBLO = new TrainingYearBLO(this.UnitOfWork, this.GAppContext);
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
            TrainingYear Create_Data_Test = TrainingYearBLO.FindBaseEntityByReference(this.TrainingYear_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                TrainingYearBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_TrainingYear_Test()
        {
            // BLO
            TrainingYearBLO sanctionBLO = new TrainingYearBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("TrainingYearsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[TrainingYearState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_TrainingYear
            ExportService exportService = new ExportService(typeof(TrainingYear), typeof(Default_TrainingYear_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "TrainingYearsController");
            var data = new Default_TrainingYear_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_TrainingYear_Export_Model First_Exptected_TrainingYear = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_TrainingYear);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_TrainingYear)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class TrainingYearBLOTests : Base_TrainingYearBLOTests
    {

    }
}
