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

using GApp.Entities.Resources.AuthrorizationAppResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_AuthrorizationAppBLOTests : Base_BLO_Tests
    {
        public AuthrorizationAppTestDataFactory AuthrorizationApp_TestData { set; get; }
		public AuthrorizationAppBLO AuthrorizationAppBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_AuthrorizationAppBLOTests()
        {
            AuthrorizationApp_TestData = new AuthrorizationAppTestDataFactory(this.UnitOfWork, this.GAppContext);
            AuthrorizationAppBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);
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
            AuthrorizationApp Create_Data_Test = AuthrorizationAppBLO.FindBaseEntityByReference(this.AuthrorizationApp_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                AuthrorizationAppBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_AuthrorizationApp_Test()
        {
            // BLO
            AuthrorizationAppBLO sanctionBLO = new AuthrorizationAppBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("AuthrorizationAppsController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[AuthrorizationAppState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_AuthrorizationApp
            ExportService exportService = new ExportService(typeof(AuthrorizationApp), typeof(Default_AuthrorizationApp_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "AuthrorizationAppsController");
            var data = new Default_AuthrorizationApp_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_AuthrorizationApp_Export_Model First_Exptected_AuthrorizationApp = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_AuthrorizationApp);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_AuthrorizationApp)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class AuthrorizationAppBLOTests : Base_AuthrorizationAppBLOTests
    {

    }
}
