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

using TrainingIS.Entities.Resources.SpecialtyResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_SpecialtyBLOTests : Base_BLO_Tests
    {
        public SpecialtyTestDataFactory Specialty_TestData { set; get; }
		public SpecialtyBLO SpecialtyBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_SpecialtyBLOTests()
        {
            Specialty_TestData = new SpecialtyTestDataFactory(this.UnitOfWork, this.GAppContext);
            SpecialtyBLO = new SpecialtyBLO(this.UnitOfWork, this.GAppContext);
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
            Specialty Create_Data_Test = SpecialtyBLO.FindBaseEntityByReference(this.Specialty_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                SpecialtyBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Specialty_Test()
        {
            // BLO
            SpecialtyBLO sanctionBLO = new SpecialtyBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SpecialtiesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[SpecialtyState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Specialty
            ExportService exportService = new ExportService(typeof(Specialty), typeof(Default_Specialty_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "SpecialtysController");
            var data = new Default_Specialty_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Specialty_Export_Model First_Exptected_Specialty = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Specialty);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Specialty)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class SpecialtyBLOTests : Base_SpecialtyBLOTests
    {

    }
}
