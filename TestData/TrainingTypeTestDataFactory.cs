using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TestData
{
    public partial class TrainingTypeTestDataFactory
    {
       
        protected override List<TrainingType> Generate_TestData()
        {
            List<TrainingType> Data = new List<TrainingType>();
            TrainingType trainingtype_1 = new TrainingType();
            trainingtype_1.Code = "Cours de jours";
            trainingtype_1.Name = "Cours de jours";
            trainingtype_1.Reference = trainingtype_1.Code;
            Data.Add(trainingtype_1);

            TrainingType trainingtype_2 = new TrainingType();
            trainingtype_2.Code = "Cours de soir";
            trainingtype_2.Name = "Cours de soir";
            trainingtype_2.Reference = trainingtype_2.Code;
            Data.Add(trainingtype_2);

            return Data;
        }
    }
}
