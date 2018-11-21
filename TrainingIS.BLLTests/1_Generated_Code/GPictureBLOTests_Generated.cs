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

using GApp.Entities.Resources.GPictureResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_GPictureBLOTests : Base_BLO_Tests
    {
        public GPictureTestDataFactory GPicture_TestData { set; get; }
        public Base_GPictureBLOTests()
        {
            GPicture_TestData = new GPictureTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_GPicture_Test()
        {
            // BLO
            GPictureBLO sanctionBLO = new GPictureBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("GPicturesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[GPictureState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_GPicture
            ExportService exportService = new ExportService(typeof(GPicture), typeof(Default_GPicture_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "GPicturesController");
            var data = new Default_GPicture_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_GPicture_Export_Model First_Exptected_GPicture = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_GPicture);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_GPicture)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class GPictureBLOTests : Base_GPictureBLOTests
    {

    }
}
