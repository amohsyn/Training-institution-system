using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class AbsenceTestDataFactory
    {
        /// <summary>
        /// The First and the last Absences is used to Test Calculate_InvalideSanction
        /// </summary>
        /// <returns></returns>
        protected override List<Absence> Generate_TestData()
        {
            List<Absence> Data = new List<Absence>();

            // BLO
            AbsenceBLO AbsenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            
            var SeanceTrainings = this.UnitOfWork.context.SeanceTrainings.ToList();

            foreach (var SeanceTraining in SeanceTrainings)
            {
                
                var Trainees = SeanceTraining.SeancePlanning.Training.Group.Trainees;

                for (int i = 0; i < Trainees.Count; i++)
                {
                    // Absence of first and Last 4 trainne 
                    if (i <= (4-1) || i >= (Trainees.Count - 1 - 4))
                    {
                        var trainee = Trainees[i];

                        Absence absence = AbsenceBLO.CreateInstance();
                        absence.AbsenceDate = (DateTime)SeanceTraining.SeanceDate;
                        absence.SeanceTraining = SeanceTraining;
                        absence.Trainee = trainee;
                        absence.Reference = absence.CalculateReference();
                        Data.Add(absence);
                    }
                    
                }
                
                 
            }



            return Data;
        }

        public override void Prepare_Data_Aftter_Insert()
        {
            AbsenceBLO AbsenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            var SeanceTrainings = this.UnitOfWork.context.SeanceTrainings.ToList();

            // Validate the First Absence to be used in Calculate_InValide_SanctionsTest
            foreach (var SeanceTraining in SeanceTrainings)
            {
                var Trainees = SeanceTraining.SeancePlanning.Training.Group.Trainees.OrderBy(t => t.Ordre).ToList();
                var trainee = Trainees.First();

                Absence absence = AbsenceBLO.CreateInstance();
                absence.AbsenceDate = (DateTime)SeanceTraining.SeanceDate;
                absence.SeanceTraining = SeanceTraining;
                absence.Trainee = trainee;
                absence.Reference = absence.CalculateReference();

                Absence db_absence = AbsenceBLO.FindBaseEntityByReference(absence.Reference);
                AbsenceBLO.ChangeState_to_Valid(db_absence);

            }

        }
    }
}
