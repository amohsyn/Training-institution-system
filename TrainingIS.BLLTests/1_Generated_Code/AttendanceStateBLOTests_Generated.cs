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

using TrainingIS.Entities.Resources.AttendanceStateResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_AttendanceStateBLOTests : Base_BLO_Tests
    {
        public AttendanceStateTestDataFactory AttendanceState_TestData { set; get; }
		public AttendanceStateBLO AttendanceStateBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_AttendanceStateBLOTests()
        {
            AttendanceState_TestData = new AttendanceStateTestDataFactory(this.UnitOfWork, this.GAppContext);
            AttendanceStateBLO = new AttendanceStateBLO(this.UnitOfWork, this.GAppContext);
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
            AttendanceState Create_Data_Test = AttendanceStateBLO.FindBaseEntityByReference(this.AttendanceState_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                AttendanceStateBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_AttendanceState_Test()
        {
            // BLO
            AttendanceStateBLO sanctionBLO = new AttendanceStateBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("AttendanceStatesController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[AttendanceStateState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_AttendanceState
            ExportService exportService = new ExportService(typeof(AttendanceState), typeof(Default_AttendanceState_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "AttendanceStatesController");
            var data = new Default_AttendanceState_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_AttendanceState_Export_Model First_Exptected_AttendanceState = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_AttendanceState);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_AttendanceState)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class AttendanceStateBLOTests : Base_AttendanceStateBLOTests
    {

    }
}
