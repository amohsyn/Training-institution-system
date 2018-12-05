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

using TrainingIS.Entities.Resources.AdministratorResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_AdministratorBLOTests : Base_BLO_Tests
    {
        public AdministratorTestDataFactory Administrator_TestData { set; get; }
		public AdministratorBLO AdministratorBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_AdministratorBLOTests()
        {
            Administrator_TestData = new AdministratorTestDataFactory(this.UnitOfWork, this.GAppContext);
            AdministratorBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);
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
            Administrator Create_Data_Test = AdministratorBLO.FindBaseEntityByReference(this.Administrator_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                AdministratorBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Administrator_Test()
        {
            // BLO
            AdministratorBLO sanctionBLO = new AdministratorBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("AdministratorsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[AdministratorState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Administrator
            ExportService exportService = new ExportService(typeof(Administrator), typeof(Default_Administrator_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "AdministratorsController");
            var data = new Default_Administrator_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Administrator_Export_Model First_Exptected_Administrator = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Administrator);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Administrator)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class AdministratorBLOTests : Base_AdministratorBLOTests
    {

    }
}
