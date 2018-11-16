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

using GApp.Entities.Resources.RoleAppResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLLTests
{
    public class Base_RoleApp_BLO_Tests : Base_BLO_Tests
    {
        public RoleAppTestDataFactory RoleApp_TestData { set; get; }
        public Base_RoleApp_BLO_Tests()
        {
            RoleApp_TestData = new RoleAppTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_RoleApp_Test()
        {
            // BLO
            RoleAppBLO sanctionBLO = new RoleAppBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("RoleAppsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[RoleAppState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_RoleApp
            ExportService exportService = new ExportService(typeof(RoleApp), typeof(Default_RoleApp_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "RoleAppsController");
            var data = new Default_RoleApp_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_RoleApp_Export_Model First_Exptected_RoleApp = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_RoleApp);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_RoleApp)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class RoleApp_BLO_Tests : Base_RoleApp_BLO_Tests
    {

    }
}
