using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using System.Data.Entity;

namespace TrainingIS.BLL.Tests
{
    [TestClass()]
    public class SeanceTrainingBLOTests : Base_BLO_Tests
    {
        [TestMethod()]
        public void SaveTest()
        {

            //SeanceTraining item = this.UnitOfWork.context.SeanceTrainings.First();


            //var Current_HourlyMass = this.UnitOfWork.context.SeanceTrainings
            //.Where(s => s.SeancePlanning.Training.Id == item.SeancePlanning.Training.Id)
            //.Select(s => DbFunctions.DiffMinutes(s.SeancePlanning.SeanceNumber.StartTime,s.SeancePlanning.SeanceNumber.EndTime))
            //.Count();



        }
    }
}