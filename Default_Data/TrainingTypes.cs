using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace Default_Data
{
    public class TrainingTypes
    {
        public void InsertDefaultData()
        {

            TrainingTypeBLO trainingTypeBLO = new TrainingTypeBLO();

            TrainingType trainingType  = null;

            trainingType = trainingTypeBLO.FindBaseEntityByReference("cours-jour");
            if (trainingType == null)
            {
                trainingType = new TrainingType();
                trainingType.Reference = "cours-jour";
                trainingType.Code = "cours-jour";
                trainingType.Name = "Cours de jour";
                trainingTypeBLO.Save(trainingType);
            }

            trainingType = trainingTypeBLO.FindBaseEntityByReference("cours-soir");
            if (trainingType == null)
            {
                trainingType = new TrainingType();
                trainingType.Reference = "cours-soir";
                trainingType.Code = "cours-soir";
                trainingType.Name = "Cours de soire";
                trainingTypeBLO.Save(trainingType);
            }

        }
    }
}
