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

using TrainingIS.Entities.Resources.FormerResources;
using TrainingIS.Models.FormerModelsViews;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

namespace TrainingIS.BLL.Tests
{
    public class Base_FormerBLOTests : Base_BLO_Tests
    {
        public FormerTestDataFactory Former_TestData { set; get; }
		public FormerBLO FormerBLO { set; get; }
		public bool InitData_Initlizalize { get; set; }

        public Base_FormerBLOTests()
        {
            Former_TestData = new FormerTestDataFactory(this.UnitOfWork, this.GAppContext);
            FormerBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
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
            Former Create_Data_Test = FormerBLO.FindBaseEntityByReference(this.Former_TestData.Entity_CRUD_Test_Reference);
            if (Create_Data_Test != null)
                FormerBLO.Delete(Create_Data_Test);
        }

        [TestMethod()]
        public virtual void Export_Former_Test()
        {
            // BLO
            FormerBLO sanctionBLO = new FormerBLO(this.UnitOfWork, this.GAppContext);
            DataTable dataTable = sanctionBLO.Export("FormersController");

            // Arrange
            FilterRequestParams filterRequestParams = new FilterRequestParams();
            // filterRequestParams.FilterBy = "[FormerState,0]";
            var First_Row = dataTable.Rows[0];

            // Exprected First_Exptected_Former
            ExportService exportService = new ExportService(typeof(Former), typeof(Default_Former_Export_Model));
            var ExportedProperties = exportService.GetExportedProperties();
            Assert.AreEqual(dataTable.Columns.Count, ExportedProperties.Count);
            filterRequestParams = sanctionBLO.Save_OR_Load_filterRequestParams_State(filterRequestParams, "FormersController");
            var data = new Default_Former_Export_ModelBLM(this.UnitOfWork, this.GAppContext)
                .Find(filterRequestParams, sanctionBLO.GetSearchCreteria(), out int t);
            Default_Former_Export_Model First_Exptected_Former = data.First();


            // Assert - First Data
            int i = 0;
            foreach (PropertyInfo propertyInfo in ExportedProperties)
            {
                var Exptected_value = propertyInfo
                    .GetValue(First_Exptected_Former);
                if (Exptected_value == null) continue;

                var Exptected_value_string = propertyInfo
                    .GetValue(First_Exptected_Former)?
                    .ToString();
                var value = First_Row[i].ToString();
                Assert.AreEqual(Exptected_value_string, value);
                i++;
            }
            

        }
    }

    [TestClass]
    public partial class FormerBLOTests : Base_FormerBLOTests
    {

    }
}
