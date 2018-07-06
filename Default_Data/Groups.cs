using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace Default_Data
{
    public class Groups
    {
        public void InsertDefaultData()
        {
            

            GroupBLO groupBLO = new GroupBLO();

            Specialty specialty_tdi = new SpecialtyBLO().FindBaseEntityByReference("TDI");
            Specialty specialty_tri = new SpecialtyBLO().FindBaseEntityByReference("TRI");
            TrainingType trainingType_cours_jour = new TrainingTypeBLO().FindBaseEntityByReference("cours-jour");
            TrainingYear TrainingYear_2017_2018 = new TrainingYearBLO().FindBaseEntityByReference("2017-2018");

        
            for (int i = 1; i <= 5; i++)
            {
                Group group = groupBLO.FindBaseEntityByReference("TDI10" + i);
                if (group == null)
                {
                    group = new Group();
                    group.Reference = "TDI10" + i;
                    group.Code = "TDI10" + i;
                    group.Specialty = specialty_tdi;
                    group.TrainingType = trainingType_cours_jour;
                    group.TrainingYear = TrainingYear_2017_2018;
                    group.Year = 1;

                    groupBLO.Save(group);
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                Group group = groupBLO.FindBaseEntityByReference("TDI20" + i);
                if (group == null)
                {
                    group = new Group();
                    group.Reference = "TDI20" + i;
                    group.Code = "TDI20" + i;
                    group.Specialty = specialty_tdi;
                    group.TrainingType = trainingType_cours_jour;
                    group.TrainingYear = TrainingYear_2017_2018;
                    group.Year = 2;

                    groupBLO.Save(group);
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                Group group = groupBLO.FindBaseEntityByReference("TRI10" + i);
                if (group == null)
                {
                    group = new Group();
                    group.Reference = "TRI10" + i;
                    group.Code = "TRI10" + i;
                    group.Specialty = specialty_tri;
                    group.TrainingType = trainingType_cours_jour;
                    group.TrainingYear = TrainingYear_2017_2018;
                    group.Year = 1;

                    groupBLO.Save(group);
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                Group group = groupBLO.FindBaseEntityByReference("TRI20" + i);
                if (group == null)
                {
                    group = new Group();
                    group.Reference = "TRI20" + i;
                    group.Code = "TRI20" + i;
                    group.Specialty = specialty_tri;
                    group.TrainingType = trainingType_cours_jour;
                    group.TrainingYear = TrainingYear_2017_2018;
                    group.Year = 2;

                    groupBLO.Save(group);
                }
            }

        }
    }
}
