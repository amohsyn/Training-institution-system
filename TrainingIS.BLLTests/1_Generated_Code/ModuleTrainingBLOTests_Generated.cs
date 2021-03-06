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

using TrainingIS.Entities.Resources.ModuleTrainingResources;
using TrainingIS.Models.ModuleTrainings;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_ModuleTrainingBLOTests : Base_BLO_Tests
    {
        public ModuleTrainingTestDataFactory ModuleTraining_TestData { set; get; }
		public ModuleTrainingBLO ModuleTrainingBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_ModuleTrainingBLOTests()
        {
            ModuleTraining_TestData = new ModuleTrainingTestDataFactory(this.UnitOfWork, this.GAppContext);
            ModuleTrainingBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
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
            ModuleTraining Create_Data_Test = ModuleTrainingBLO.FindBaseEntityByReference(this.ModuleTraining_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                ModuleTrainingBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_ModuleTraining_Test()
        {
            // BLO
            ModuleTrainingBLO sanctionBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("ModuleTrainingsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[ModuleTrainingState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_ModuleTraining
            ExportService exportService = new ExportService(typeof(ModuleTraining), typeof(Default_ModuleTraining_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "ModuleTrainingsController");
            var data = new Default_ModuleTraining_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_ModuleTraining_Export_Model First_Exptected_ModuleTraining = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_ModuleTraining);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_ModuleTraining)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class ModuleTrainingBLOTests : Base_ModuleTrainingBLOTests
    {

    }
}
