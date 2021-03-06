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

using TrainingIS.Entities.Resources.MetierResources;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_MetierBLOTests : Base_BLO_Tests
    {
        public MetierTestDataFactory Metier_TestData { set; get; }
		public MetierBLO MetierBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_MetierBLOTests()
        {
            Metier_TestData = new MetierTestDataFactory(this.UnitOfWork, this.GAppContext);
            MetierBLO = new MetierBLO(this.UnitOfWork, this.GAppContext);
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
            Metier Create_Data_Test = MetierBLO.FindBaseEntityByReference(this.Metier_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                MetierBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Metier_Test()
        {
            // BLO
            MetierBLO sanctionBLO = new MetierBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("MetiersController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[MetierState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Metier
            ExportService exportService = new ExportService(typeof(Metier), typeof(Default_Metier_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "MetiersController");
            var data = new Default_Metier_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Metier_Export_Model First_Exptected_Metier = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Metier);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Metier)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class MetierBLOTests : Base_MetierBLOTests
    {

    }
}
