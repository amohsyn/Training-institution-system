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

using TrainingIS.Entities.Resources.TrainingTypeResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_TrainingTypeBLOTests : Base_BLO_Tests
    {
        public TrainingTypeTestDataFactory TrainingType_TestData { set; get; }
		public TrainingTypeBLO TrainingTypeBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_TrainingTypeBLOTests()
        {
            TrainingType_TestData = new TrainingTypeTestDataFactory(this.UnitOfWork, this.GAppContext);
            TrainingTypeBLO = new TrainingTypeBLO(this.UnitOfWork, this.GAppContext);
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
            TrainingType Create_Data_Test = TrainingTypeBLO.FindBaseEntityByReference(this.TrainingType_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                TrainingTypeBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_TrainingType_Test()
        {
            // BLO
            TrainingTypeBLO sanctionBLO = new TrainingTypeBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("TrainingTypesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[TrainingTypeState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_TrainingType
            ExportService exportService = new ExportService(typeof(TrainingType), typeof(Default_TrainingType_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "TrainingTypesController");
            var data = new Default_TrainingType_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_TrainingType_Export_Model First_Exptected_TrainingType = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_TrainingType);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_TrainingType)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class TrainingTypeBLOTests : Base_TrainingTypeBLOTests
    {

    }
}
