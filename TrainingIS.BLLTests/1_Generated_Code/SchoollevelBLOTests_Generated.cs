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

using TrainingIS.Entities.Resources.SchoollevelResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_SchoollevelBLOTests : Base_BLO_Tests
    {
        public SchoollevelTestDataFactory Schoollevel_TestData { set; get; }
		public SchoollevelBLO SchoollevelBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_SchoollevelBLOTests()
        {
            Schoollevel_TestData = new SchoollevelTestDataFactory(this.UnitOfWork, this.GAppContext);
            SchoollevelBLO = new SchoollevelBLO(this.UnitOfWork, this.GAppContext);
        }
 

        [TestInitialize]
        public virtual void InitData()
        {
            if (!InitData_Initlizalize)
            {
                this.CleanData();
                InitData_Initlizalize = true;
            }

        }

        [TestCleanup]
        public virtual void CleanData()
        {
            // Clean Create Data Test
            Schoollevel Create_Data_Test = SchoollevelBLO.FindBaseEntityByReference(this.Schoollevel_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                SchoollevelBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Schoollevel_Test()
        {
            // BLO
            SchoollevelBLO sanctionBLO = new SchoollevelBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SchoollevelsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[SchoollevelState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Schoollevel
            ExportService exportService = new ExportService(typeof(Schoollevel), typeof(Default_Schoollevel_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "SchoollevelsController");
            var data = new Default_Schoollevel_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Schoollevel_Export_Model First_Exptected_Schoollevel = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Schoollevel);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Schoollevel)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class SchoollevelBLOTests : Base_SchoollevelBLOTests
    {

    }
}
