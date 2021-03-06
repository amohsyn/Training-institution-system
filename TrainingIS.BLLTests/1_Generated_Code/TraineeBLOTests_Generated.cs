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

using TrainingIS.Entities.Resources.TraineeResources;
using TrainingIS.Models.Trainees;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_TraineeBLOTests : Base_BLO_Tests
    {
        public TraineeTestDataFactory Trainee_TestData { set; get; }
		public TraineeBLO TraineeBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_TraineeBLOTests()
        {
            Trainee_TestData = new TraineeTestDataFactory(this.UnitOfWork, this.GAppContext);
            TraineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
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
            Trainee Create_Data_Test = TraineeBLO.FindBaseEntityByReference(this.Trainee_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                TraineeBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Trainee_Test()
        {
            // BLO
            TraineeBLO sanctionBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("TraineesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[TraineeState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Trainee
            ExportService exportService = new ExportService(typeof(Trainee), typeof(Default_Trainee_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "TraineesController");
            var data = new Default_Trainee_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Trainee_Export_Model First_Exptected_Trainee = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Trainee);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Trainee)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class TraineeBLOTests : Base_TraineeBLOTests
    {

    }
}
