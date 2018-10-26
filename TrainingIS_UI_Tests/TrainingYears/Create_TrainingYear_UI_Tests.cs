using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.UnitTest.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TestData;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS_UI_Tests.TrainingYears
{
    public partial class Create_TrainingYear_UI_Tests
    {
        [TestMethod]
        public override void TrainingYear_Create_Test()
        {
            // Create TestData
            var Create_Data_Test = TrainingYear_TestData.CreateValideTrainingYearInstance();
            Create_Data_Test.Reference = "Create_Data_Test";
            Create_Data_Test.StartDate =  Convert.ToDateTime("1/9/2022");
            Create_Data_Test.EndtDate = Convert.ToDateTime("31/8/2023");
            Create_Data_Test.Code = this.Entity_Reference;
            

            // Create in UI
            TrainingYear_UI_Create(Create_Data_Test);

            //Assert
            Assert.IsTrue(this.IndexPage.Is_In_IndexPage());
            Assert.IsTrue(this.Alert.Is_Info_Alert());

        }

        [TestMethod]
        public void Create_TrainingYear_with_existant_cycle_Test()
        {
            // TrainingYear_with_existant_cycle TestData
            var Existant_TrainingYear_Cyle = TrainingYear_TestData.Get_TestData().First();
            Existant_TrainingYear_Cyle.Reference = "Create_Data_Test";
            Existant_TrainingYear_Cyle.Code = "2019_2";
            Existant_TrainingYear_Cyle.StartDate = Existant_TrainingYear_Cyle.StartDate.AddDays(10);
            Existant_TrainingYear_Cyle.EndtDate = Existant_TrainingYear_Cyle.StartDate.AddDays(10);
            this.Reference_Created_Object = Existant_TrainingYear_Cyle.CalculateReference();

            // Create in UI
            TrainingYear_UI_Create(Existant_TrainingYear_Cyle);

            //Assert
            Assert.IsTrue(this.CreatePage.Is_In_CreatePage());
            Assert.IsTrue(this.Alert.Is_Error_Alert());
        }

        //[TestMethod]
        //public void Create_TrainingYear_with_Existant_Code_Test()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
