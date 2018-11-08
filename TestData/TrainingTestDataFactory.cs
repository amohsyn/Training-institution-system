using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class TrainingTestDataFactory
    {
        protected override List<Training> Generate_TestData()
        {

            List<Training> Data = new List<Training>();

            // BLO
            TrainingBLO trainingBLO = new TrainingBLO(this.UnitOfWork, this.GAppContext);

            // Data members
            var TrainingYears = this.UnitOfWork.context.TrainingYears.ToList();
            var Specialties = this.UnitOfWork.context.Specialties.ToList();
            var Formers = this.UnitOfWork.context.Formers.ToList();

            // TrainingYear = 2019
            var TrainingYear = TrainingYears.First();

            int Former_Index =0;
            int Trainings_Order = 1;
            foreach (var Specialty in Specialties)
            {
                // Groups fo current Specialty
                var Groups = this.UnitOfWork.context.Groups
                    .Where(g => g.Specialty.Id == Specialty.Id)
                    .ToList();


                foreach (var Group in Groups)
                {

                    // Find ModuleTraining by Specialty and YearStudy of Current Group
                    var ModuleTrainings = this.UnitOfWork
                    .context
                    .ModuleTrainings
                    .Where(m => m.Specialty.Id == Specialty.Id)
                    .Where(m => m.YearStudy.Id == Group.YearStudy.Id)
                    .ToList();


                    foreach (var ModuleTraining in ModuleTrainings)
                    {

                        Training training = trainingBLO.CreateInstance();
                        training.Group = Group;
                        training.ModuleTraining = ModuleTraining;
                        training.Former = Formers[Former_Index];
                        training.TrainingYear = TrainingYear;
                        training.Reference = training.CalculateReference();
                        training.Ordre = Trainings_Order++;
                        Data.Add(training);
                        // Next former
                        Former_Index++;
                        if (Former_Index >= (Formers.Count() - 1))
                            Former_Index = 0;
                    }
                }

            }




            return Data;
        }
    }
}
