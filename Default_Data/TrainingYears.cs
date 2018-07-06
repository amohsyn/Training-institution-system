using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace Default_Data
{
    public class TrainingYears
    {
        public void InsertDefaultData()
        {

            TrainingYearBLO trainingYearBLO = new TrainingYearBLO();

            TrainingYear trainingYear = null;

            trainingYear = trainingYearBLO.FindBaseEntityByReference("2017-2018");
            if (trainingYear == null)
            {
                trainingYear = new TrainingYear();
                trainingYear.Reference = "2017-2018";
                trainingYear.Code = "2017-2018";
                trainingYear.StartDate = new DateTime(2017, 9, 1);
                trainingYear.EndtDate = new DateTime(2018, 7, 31);
                trainingYearBLO.Save(trainingYear);
            }
        }
    }
}
