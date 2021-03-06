﻿using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.SeanceTrainings
{
    [TestCategory("SeanceTraining")]
    public  partial class SeanceTraining_UI_Tests : PageTest
    {
        public SeanceTraining_UI_Tests(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }

        public SeanceTraining_UI_Tests() : base(null) { }

        protected override void Constructor(UI_Test_Context UI_Test_Context)
        {
            base.Constructor(UI_Test_Context);
            this.UI_Test_Context.Login = "essarraj.fouad@gmail.com";
            this.UI_Test_Context.Password = "Formateur@123456";
            this.UI_Test_Context.ControllerName = "SeanceTrainings";
        }

        

        
        [TestMethod]
        public void SeanceTraining_Filter_By_Groupe_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // TDI 101
            this.Select.SelectValue("SeancePlanning.Training.Group.Id_Filter", "22");
            this.Ajax.WaitForAjax();

            this.DataTable.Init("SeanceTrainings_Entities");
            this.Ajax.WaitForAjax();

            this.DataTable.Init("SeanceTrainings_Entities");
            Assert.AreEqual(this.DataTable.Lines.Count(), 2);
            Assert.AreEqual(this.DataTable.Lines[0][0].Text, "TDI101");
            Assert.AreEqual(this.DataTable.Lines[1][0].Text, "TDI101");
        }
        [TestMethod]
        public void SeanceTraining_Filter_Former_Validation_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Valide SeanceTraining
            b.FindElement(By.Id("FormerValidation_Filter")).Click();
            this.Ajax.WaitForAjax();

            this.DataTable.Init("SeanceTrainings_Entities");
            Assert.AreEqual(this.DataTable.Lines.Count(), 0);
        }
        [TestMethod]
        public void SeanceTraining_Search_Test()
        {
            this.GoTo_Index_And_Login_If_Not_Ahenticated();

            // Valide SeanceTraining
            b.FindElement(By.Id("Search_GAppDataTable")).SendKeys("TDI101");
            this.Ajax.WaitForAjax();

            this.DataTable.Init("SeanceTrainings_Entities");
            this.Ajax.WaitForAjax();

            Assert.AreEqual(this.DataTable.Lines.Count(), 2);

            Assert.AreEqual(this.DataTable.Lines[0][0].Text, "TDI101");
            Assert.AreEqual(this.DataTable.Lines[1][0].Text, "TDI101");
        }

    }
}
