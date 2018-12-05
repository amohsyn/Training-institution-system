using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class JustificationAbsenceTestDataFactory
    {
        public override JustificationAbsence CreateValideJustificationAbsenceInstance()
        {
            JustificationAbsence justificationAbsence = base.CreateValideJustificationAbsenceInstance();

            return justificationAbsence;
        }

        public override JustificationAbsence Create_CRUD_JustificationAbsence_Test_Instance()
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            Trainee trainee = traineeBLO.FindBaseEntityByReference(TestData_Descriptions.Trainee_TestData_Description.Justification_Absence_Trainee_Test_Reference);

            JustificationAbsence justificationAbsence = base.Create_CRUD_JustificationAbsence_Test_Instance();
            justificationAbsence.Trainee = trainee;
            justificationAbsence.TraineeId = trainee.Id;
            justificationAbsence.StartDate = new DateTime(2018, 9, 3);
            justificationAbsence.EndtDate = new DateTime(2018, 9, 5); // 5 not included, because it and at 00:00
            justificationAbsence.Description = "Justification of 5 Absences of Trainee 421";
            return justificationAbsence;
        }
    }
}
