using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL.ModelsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.UnitTest;
using TrainingIS.BLL.Tests;
using GApp.UnitTest.DataAnnotations;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews.Tests
{
    [TestClass()]
    [CleanTestDB]
    public class SeanceModelBLMTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void GetSeancesTest()
        {
            DateTime SeanceDate = Convert.ToDateTime("10/09/2018");
            SeanceModelBLM seanceModelBLM = new SeanceModelBLM(this.UnitOfWork, this.GAppContext);
            var seances_all_seancesNmerrs = seanceModelBLM.GetSeances(SeanceDate, null);
            var seances_S1 = seanceModelBLM.GetSeances(SeanceDate, "S1");

            Assert.AreEqual(seances_all_seancesNmerrs.Count, 3);

            Assert.IsNotNull(seances_S1.First().SeanceTraining);
            Assert.IsNull(seances_S1.Last().SeanceTraining);


        }
    }
}