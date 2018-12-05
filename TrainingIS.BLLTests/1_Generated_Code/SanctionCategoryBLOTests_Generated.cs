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

using TrainingIS.Entities.Resources.SanctionCategoryResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_SanctionCategoryBLOTests : Base_BLO_Tests
    {
        public SanctionCategoryTestDataFactory SanctionCategory_TestData { set; get; }
		public SanctionCategoryBLO SanctionCategoryBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_SanctionCategoryBLOTests()
        {
            SanctionCategory_TestData = new SanctionCategoryTestDataFactory(this.UnitOfWork, this.GAppContext);
            SanctionCategoryBLO = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext);
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
            SanctionCategory Create_Data_Test = SanctionCategoryBLO.FindBaseEntityByReference(this.SanctionCategory_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                SanctionCategoryBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_SanctionCategory_Test()
        {
            // BLO
            SanctionCategoryBLO sanctionBLO = new SanctionCategoryBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SanctionCategoriesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[SanctionCategoryState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_SanctionCategory
            ExportService exportService = new ExportService(typeof(SanctionCategory), typeof(Default_SanctionCategory_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "SanctionCategorysController");
            var data = new Default_SanctionCategory_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_SanctionCategory_Export_Model First_Exptected_SanctionCategory = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_SanctionCategory);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_SanctionCategory)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class SanctionCategoryBLOTests : Base_SanctionCategoryBLOTests
    {

    }
}
