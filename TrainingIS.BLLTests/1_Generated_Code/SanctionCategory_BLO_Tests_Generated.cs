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

namespace TrainingIS.BLLTests
{
    public class Base_SanctionCategory_BLO_Tests : Base_BLO_Tests
    {
        public SanctionCategoryTestDataFactory SanctionCategory_TestData { set; get; }
        public Base_SanctionCategory_BLO_Tests()
        {
            SanctionCategory_TestData = new SanctionCategoryTestDataFactory(this.UnitOfWork, this.GAppContext);
           
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
    public partial class SanctionCategory_BLO_Tests : Base_SanctionCategory_BLO_Tests
    {

    }
}
