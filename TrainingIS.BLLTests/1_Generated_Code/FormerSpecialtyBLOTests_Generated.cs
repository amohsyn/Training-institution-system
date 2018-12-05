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

using TrainingIS.Entities.Resources.FormerSpecialtyResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_FormerSpecialtyBLOTests : Base_BLO_Tests
    {
        public FormerSpecialtyTestDataFactory FormerSpecialty_TestData { set; get; }
		public FormerSpecialtyBLO FormerSpecialtyBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_FormerSpecialtyBLOTests()
        {
            FormerSpecialty_TestData = new FormerSpecialtyTestDataFactory(this.UnitOfWork, this.GAppContext);
            FormerSpecialtyBLO = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext);
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
            FormerSpecialty Create_Data_Test = FormerSpecialtyBLO.FindBaseEntityByReference(this.FormerSpecialty_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                FormerSpecialtyBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_FormerSpecialty_Test()
        {
            // BLO
            FormerSpecialtyBLO sanctionBLO = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("FormerSpecialtiesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[FormerSpecialtyState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_FormerSpecialty
            ExportService exportService = new ExportService(typeof(FormerSpecialty), typeof(Default_FormerSpecialty_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "FormerSpecialtysController");
            var data = new Default_FormerSpecialty_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_FormerSpecialty_Export_Model First_Exptected_FormerSpecialty = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_FormerSpecialty);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_FormerSpecialty)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class FormerSpecialtyBLOTests : Base_FormerSpecialtyBLOTests
    {

    }
}
