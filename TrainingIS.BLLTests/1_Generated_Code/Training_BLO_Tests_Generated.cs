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

using TrainingIS.Entities.Resources.TrainingResources;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.Trainings;

namespace TrainingIS.BLLTests
{
    public class Base_Training_BLO_Tests : Base_BLO_Tests
    {
        public TrainingTestDataFactory Training_TestData { set; get; }
        public Base_Training_BLO_Tests()
        {
            Training_TestData = new TrainingTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_Training_Test()
        {
            // BLO
            TrainingBLO sanctionBLO = new TrainingBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("TrainingsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[TrainingState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Training
            ExportService exportService = new ExportService(typeof(Training), typeof(Default_Training_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "TrainingsController");
            var data = new Default_Training_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Training_Export_Model First_Exptected_Training = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Training);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Training)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class Training_BLO_Tests : Base_Training_BLO_Tests
    {

    }
}
