using GApp.DAL;
using GApp.DAL.ReadExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;
using AutoFixture;
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
            Generated_Data = new List<Absence>();

            // BLO
            AbsenceBLO AbsenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);

            var SeanceTrainings = this.UnitOfWork.context.SeanceTrainings.ToList();

            foreach (var SeanceTraining in SeanceTrainings)
            {

                var Trainees = SeanceTraining.SeancePlanning.Training.Group.Trainees;

                for (int i = 0; i < Trainees.Count; i++)
                {
                    // Absence of first and Last 4 trainne 
                    if (i <= (4 - 1) || i >= (Trainees.Count - 1 - 4))
                    {
                        var trainee = Trainees[i];

                        Absence absence = AbsenceBLO.CreateInstance();
                        absence.AbsenceDate = (DateTime)SeanceTraining.SeanceDate;
                        absence.SeanceTraining = SeanceTraining;
                        absence.Trainee = trainee;
                        absence.Reference = absence.CalculateReference();
                        Generated_Data.Add(absence);

                        // Add Absence of Firsr Trainee
                        break;

                    }

                }


            }



            return Generated_Data;
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
                if (db_absence.AbsenceState != TrainingIS.Entities.enums.AbsenceStates.Valid_Absence)
                    AbsenceBLO.ChangeState_to_Valid(db_absence);

            }

        }

        // [Generalize]
        /// <summary>
        /// Load Data from Excel File witout DataBase
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data_From_ExcelFile_as_DataTable()
        {

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Absence.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Absence>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                return firstTable;

            }
            return null;
        }

        public override Absence CreateValideAbsenceInstance()
        {
            if (UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();

            Absence Valide_Absence = this._Fixture.Create<Absence>();
            Valide_Absence.Id = 0;
            // Many to One 
            //   
            // SeanceTraining
            var SeanceTraining = new SeanceTrainingTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstSeanceTraining();
            Valide_Absence.SeanceTraining = SeanceTraining;
            Valide_Absence.SeanceTrainingId = SeanceTraining.Id;

            // Trainee
            var Trainee = new TraineeTestDataFactory(UnitOfWork, GAppContext).CreateOrLouadFirstTrainee();
            Valide_Absence.Trainee = Trainee;
            Valide_Absence.TraineeId = Trainee.Id;

            // One to Many
            //
            return Valide_Absence;
        }
    }
}
