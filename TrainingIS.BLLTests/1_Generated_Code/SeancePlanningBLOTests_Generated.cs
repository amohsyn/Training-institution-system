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

using TrainingIS.Entities.Resources.SeancePlanningResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_SeancePlanningBLOTests : Base_BLO_Tests
    {
        public SeancePlanningTestDataFactory SeancePlanning_TestData { set; get; }
		public SeancePlanningBLO SeancePlanningBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_SeancePlanningBLOTests()
        {
            SeancePlanning_TestData = new SeancePlanningTestDataFactory(this.UnitOfWork, this.GAppContext);
            SeancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
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
            SeancePlanning Create_Data_Test = SeancePlanningBLO.FindBaseEntityByReference(this.SeancePlanning_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                SeancePlanningBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_SeancePlanning_Test()
        {
            // BLO
            SeancePlanningBLO sanctionBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("SeancePlanningsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[SeancePlanningState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_SeancePlanning
            ExportService exportService = new ExportService(typeof(SeancePlanning), typeof(Default_SeancePlanning_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "SeancePlanningsController");
            var data = new Default_SeancePlanning_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_SeancePlanning_Export_Model First_Exptected_SeancePlanning = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_SeancePlanning);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_SeancePlanning)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class SeancePlanningBLOTests : Base_SeancePlanningBLOTests
    {

    }
}
