﻿using System;
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

using TrainingIS.Entities.Resources.NationalityResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_NationalityBLOTests : Base_BLO_Tests
    {
        public NationalityTestDataFactory Nationality_TestData { set; get; }
		public NationalityBLO NationalityBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_NationalityBLOTests()
        {
            Nationality_TestData = new NationalityTestDataFactory(this.UnitOfWork, this.GAppContext);
            NationalityBLO = new NationalityBLO(this.UnitOfWork, this.GAppContext);
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
            Nationality Create_Data_Test = NationalityBLO.FindBaseEntityByReference(this.Nationality_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                NationalityBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Nationality_Test()
        {
            // BLO
            NationalityBLO sanctionBLO = new NationalityBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("NationalitiesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[NationalityState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Nationality
            ExportService exportService = new ExportService(typeof(Nationality), typeof(Default_Nationality_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "NationalitysController");
            var data = new Default_Nationality_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Nationality_Export_Model First_Exptected_Nationality = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Nationality);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Nationality)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class NationalityBLOTests : Base_NationalityBLOTests
    {

    }
}
