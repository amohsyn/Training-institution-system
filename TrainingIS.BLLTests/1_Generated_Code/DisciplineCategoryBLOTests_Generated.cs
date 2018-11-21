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

using TrainingIS.Entities.Resources.DisciplineCategoryResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_DisciplineCategoryBLOTests : Base_BLO_Tests
    {
        public DisciplineCategoryTestDataFactory DisciplineCategory_TestData { set; get; }
        public Base_DisciplineCategoryBLOTests()
        {
            DisciplineCategory_TestData = new DisciplineCategoryTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_DisciplineCategory_Test()
        {
            // BLO
            DisciplineCategoryBLO sanctionBLO = new DisciplineCategoryBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("DisciplineCategoriesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[DisciplineCategoryState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_DisciplineCategory
            ExportService exportService = new ExportService(typeof(DisciplineCategory), typeof(Default_DisciplineCategory_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "DisciplineCategorysController");
            var data = new Default_DisciplineCategory_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_DisciplineCategory_Export_Model First_Exptected_DisciplineCategory = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_DisciplineCategory);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_DisciplineCategory)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class DisciplineCategoryBLOTests : Base_DisciplineCategoryBLOTests
    {

    }
}
