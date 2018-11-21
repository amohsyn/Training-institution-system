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

using GApp.Entities.Resources.LogWorkResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_LogWorkBLOTests : Base_BLO_Tests
    {
        public LogWorkTestDataFactory LogWork_TestData { set; get; }
        public Base_LogWorkBLOTests()
        {
            LogWork_TestData = new LogWorkTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_LogWork_Test()
        {
            // BLO
            LogWorkBLO sanctionBLO = new LogWorkBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("LogWorksController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[LogWorkState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_LogWork
            ExportService exportService = new ExportService(typeof(LogWork), typeof(Default_LogWork_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "LogWorksController");
            var data = new Default_LogWork_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_LogWork_Export_Model First_Exptected_LogWork = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_LogWork);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_LogWork)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class LogWorkBLOTests : Base_LogWorkBLOTests
    {

    }
}
