using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace Default_Data
{
    public class Trainees
    {
        public void InsertDefaultData()
        {
            TraineeBLO traineeBLO = new TraineeBLO();

            Group group_TDI201 = new GroupBLO().FindBaseEntityByReference("TDI201");
            Group group_TRI201 = new GroupBLO().FindBaseEntityByReference("TRI201");


            for (int i = 1; i <= 30; i++)
            {
                Trainee trainee  = traineeBLO.FindBaseEntityByReference("Madani_" + i);
                if (trainee == null)
                {
                    trainee = new Trainee();
                    trainee.Reference = "Madani_" + i;
                    trainee.CNE = "00000" + i;
                    trainee.FirstName = "Madani" + i;
                    trainee.Group = group_TDI201;
                    trainee.LastName = "Ali" + i;
                    trainee.Sex = true;
  
                    traineeBLO.Save(trainee);
                }
            }

            for (int i = 1; i <= 30; i++)
            {
                Trainee trainee = traineeBLO.FindBaseEntityByReference("Chami_" + i);
                if (trainee == null)
                {
                    trainee = new Trainee();
                    trainee.Reference = "Chami_" + i;
                    trainee.CNE = "20000" + i;
                    trainee.FirstName = "Chami" + i;
                    trainee.Group = group_TRI201;
                    trainee.LastName = "Kamal" + i;
                    trainee.Sex = true;

                    traineeBLO.Save(trainee);
                }
            }
        }
    }
}
