using System;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TrainingIS.Entities;
using TrainingIS.WebApp.Controllers.Tests;
using TrainingIS.BLL.ModelsViews;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.DAL;
using TrainingIS.WebApp.Tests.Services;
using GApp.Core.Context;
using TrainingIS.Models.Absences;

namespace TrainingIS_UI_Tests.Absences
{
    public partial class Absence_UI_Tests
    {
        [TestMethod]
        public override void Absence_Create_Test()
        {
            // Not Exist
           //  base.Absence_Create_Test();
        }
    }
}
