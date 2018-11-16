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

using TrainingIS.Entities.Resources.ClassroomResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLLTests
{
    public class Base_Classroom_BLO_Tests : Base_BLO_Tests
    {
        public ClassroomTestDataFactory Classroom_TestData { set; get; }
        public Base_Classroom_BLO_Tests()
        {
            Classroom_TestData = new ClassroomTestDataFactory(this.UnitOfWork, this.GAppContext);
           
        }

        [TestMethod()]
        public virtual void Export_Classroom_Test()
        {
            // BLO
            ClassroomBLO sanctionBLO = new ClassroomBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("ClassroomsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[ClassroomState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Classroom
            ExportService exportService = new ExportService(typeof(Classroom), typeof(Default_Classroom_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "ClassroomsController");
            var data = new Default_Classroom_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Classroom_Export_Model First_Exptected_Classroom = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Classroom);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Classroom)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class Classroom_BLO_Tests : Base_Classroom_BLO_Tests
    {

    }
}
