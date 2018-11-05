using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class ModuleTrainingTestDataFactory
    {
        protected override List<ModuleTraining> Generate_TestData()
        {
            List<ModuleTraining> Data = new List<ModuleTraining>();
            ModuleTrainingBLO moduleTrainingBLO = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
            int ModuleNumber = 0;
            foreach (Specialty specialty in this.UnitOfWork.context.Specialties.ToList())
            {
                ModuleNumber = 0;
                foreach (var Metier in this.UnitOfWork.context.Metiers.ToList())
                {
                    foreach (var YearStudy in this.UnitOfWork.context.YearStudies.ToList())
                    {
                        List<int> HourlyMass_ls = new List<int>() { 30, 60, 100, 120, 140 };
                        List<int>  Hourly_Mass_To_Teach_ls = new List<int>() { 5, 10, 15 };

                        foreach (var HourlyMass in HourlyMass_ls)
                        {
                            foreach (var Hourly_Mass_To_Teach in Hourly_Mass_To_Teach_ls)
                            {
                                ModuleNumber++;
                                ModuleTraining moduleTraining = moduleTrainingBLO.CreateInstance();
                                moduleTraining.Specialty = specialty;
                                moduleTraining.Metier = Metier;
                                moduleTraining.YearStudy = YearStudy;
                                moduleTraining.Name = string.Format("Nom Module {0} {1}", ModuleNumber, specialty.Code);
                                moduleTraining.Code = string.Format("M{0}", ModuleNumber);
                                moduleTraining.HourlyMass = HourlyMass;
                                moduleTraining.Hourly_Mass_To_Teach = HourlyMass - Hourly_Mass_To_Teach;
                                moduleTraining.Reference = moduleTraining.CalculateReference();
                                Data.Add(moduleTraining);
                            }
                           

                        }
                      


                    }
                }
            }

            return Data;
        }
    }
}
