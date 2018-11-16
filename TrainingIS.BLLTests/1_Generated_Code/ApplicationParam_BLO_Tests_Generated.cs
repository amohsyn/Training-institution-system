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

using GApp.Entities.Resources.ApplicationParamResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLLTests
{
    public class Base_ApplicationParam_BLO_Tests : Base_BLO_Tests
    {
        public ApplicationParamTestDataFactory ApplicationParam_TestData { set; get; }
        public Base_ApplicationParam_BLO_Tests()
        {
            ApplicationParam_TestData = new ApplicationParamTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_ApplicationParam_Test()
        {
            // BLO
            ApplicationParamBLO sanctionBLO = new ApplicationParamBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("ApplicationParamsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[ApplicationParamState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_ApplicationParam
            ExportService exportService = new ExportService(typeof(ApplicationParam), typeof(Default_ApplicationParam_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "ApplicationParamsController");
            var data = new Default_ApplicationParam_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_ApplicationParam_Export_Model First_Exptected_ApplicationParam = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_ApplicationParam);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_ApplicationParam)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class ApplicationParam_BLO_Tests : Base_ApplicationParam_BLO_Tests
    {

    }
}
