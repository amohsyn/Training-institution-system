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

using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLLTests
{
    public class Base_SeancePlanning_BLO_Tests : Base_BLO_Tests
    {
        public SeancePlanningTestDataFactory SeancePlanning_TestData { set; get; }
        public Base_SeancePlanning_BLO_Tests()
        {
            SeancePlanning_TestData = new SeancePlanningTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_SeancePlanning_Test()
        {
            // BLO
            SeancePlanningBLO sanctionBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SeancePlanningsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[SeancePlanningState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_SeancePlanning
            ExportService exportService = new ExportService(typeof(SeancePlanning), typeof(Default_SeancePlanning_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "SeancePlanningsController");
            var data = new Default_SeancePlanning_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_SeancePlanning_Export_Model First_Exptected_SeancePlanning = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_SeancePlanning);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_SeancePlanning)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class SeancePlanning_BLO_Tests : Base_SeancePlanning_BLO_Tests
    {

    }
}
