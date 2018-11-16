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

using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Models.FormerModelsViews;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

namespace TrainingIS.BLLTests
{
    public class Base_Former_BLO_Tests : Base_BLO_Tests
    {
        public FormerTestDataFactory Former_TestData { set; get; }
        public Base_Former_BLO_Tests()
        {
            Former_TestData = new FormerTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_Former_Test()
        {
            // BLO
            FormerBLO sanctionBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("FormersController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[FormerState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Former
            ExportService exportService = new ExportService(typeof(Former), typeof(Default_Former_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "FormersController");
            var data = new Default_Former_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Former_Export_Model First_Exptected_Former = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Former);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Former)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class Former_BLO_Tests : Base_Former_BLO_Tests
    {

    }
}
