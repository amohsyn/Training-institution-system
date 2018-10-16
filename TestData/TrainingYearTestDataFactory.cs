using GApp.UnitTest.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TestData
{
    public partial class TrainingYearTestDataFactory
    {
        public override List<TrainingYear> Generate()
        {
            var Data = new List<TrainingYear>();

            // 2019 
            TrainingYear trainingYear = new TrainingYear();
            trainingYear.StartDate = Convert.ToDateTime("01/09/2018");
            trainingYear.EndtDate = Convert.ToDateTime("31/08/2019");
            trainingYear.Reference = "2009";

            Data.Add(trainingYear);
            return base.Generate();
        }

    }
}
