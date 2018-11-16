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

using TrainingIS.Entities.Resources.Category_WarningTraineeResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLLTests
{
    public class Base_Category_WarningTrainee_BLO_Tests : Base_BLO_Tests
    {
        public Category_WarningTraineeTestDataFactory Category_WarningTrainee_TestData { set; get; }
        public Base_Category_WarningTrainee_BLO_Tests()
        {
            Category_WarningTrainee_TestData = new Category_WarningTraineeTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_Category_WarningTrainee_Test()
        {
            // BLO
            Category_WarningTraineeBLO sanctionBLO = new Category_WarningTraineeBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("Category_WarningTraineesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[Category_WarningTraineeState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Category_WarningTrainee
            ExportService exportService = new ExportService(typeof(Category_WarningTrainee), typeof(Default_Category_WarningTrainee_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "Category_WarningTraineesController");
            var data = new Default_Category_WarningTrainee_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Category_WarningTrainee_Export_Model First_Exptected_Category_WarningTrainee = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Category_WarningTrainee);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Category_WarningTrainee)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class Category_WarningTrainee_BLO_Tests : Base_Category_WarningTrainee_BLO_Tests
    {

    }
}
