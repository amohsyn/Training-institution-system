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

using TrainingIS.Entities.Resources.TrainingLevelResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_TrainingLevelBLOTests : Base_BLO_Tests
    {
        public TrainingLevelTestDataFactory TrainingLevel_TestData { set; get; }
		public TrainingLevelBLO TrainingLevelBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_TrainingLevelBLOTests()
        {
            TrainingLevel_TestData = new TrainingLevelTestDataFactory(this.UnitOfWork, this.GAppContext);
            TrainingLevelBLO = new TrainingLevelBLO(this.UnitOfWork, this.GAppContext);
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
            TrainingLevel Create_Data_Test = TrainingLevelBLO.FindBaseEntityByReference(this.TrainingLevel_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                TrainingLevelBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_TrainingLevel_Test()
        {
            // BLO
            TrainingLevelBLO sanctionBLO = new TrainingLevelBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("TrainingLevelsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[TrainingLevelState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_TrainingLevel
            ExportService exportService = new ExportService(typeof(TrainingLevel), typeof(Default_TrainingLevel_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "TrainingLevelsController");
            var data = new Default_TrainingLevel_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_TrainingLevel_Export_Model First_Exptected_TrainingLevel = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_TrainingLevel);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_TrainingLevel)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class TrainingLevelBLOTests : Base_TrainingLevelBLOTests
    {

    }
}
