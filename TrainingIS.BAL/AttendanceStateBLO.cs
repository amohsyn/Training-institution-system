using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class AttendanceStateBLO
    {
        #region Find
        private AttendanceState Find_By_TraineeId(long Trainee_Id)
        {
            return this._UnitOfWork.context.AttendanceStates
                .Where(a => a.Trainee.Id == Trainee_Id)
                .FirstOrDefault();
        }
        #endregion

        /// <summary>
        /// Find or Create AttentenceState of Trainee
        /// </summary>
        /// <param name="Tainee_Id"></param>
        /// <returns></returns>
        public AttendanceState Find_Or_Create_AttendanceState(long Tainee_Id)
        {
            AttendanceState attendanceState = this.Find_By_TraineeId(Tainee_Id);
            if (attendanceState == null)
                attendanceState = this.Create_AttendanceState(Tainee_Id);
            return attendanceState;
        }

        private AttendanceState Create_AttendanceState(long tainee_Id)
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this._UnitOfWork, this.GAppContext);
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);

            AttendanceState attendanceState = this.CreateInstance();
            attendanceState.Trainee = traineeBLO.FindBaseEntityByID(tainee_Id);

            // Valide State
            attendanceState.Valid_Note = this.Calculate_Valid_Note(tainee_Id);
            attendanceState.Valid_Sanction = sanctionBLO.Find_Current_Valide_Sanction(tainee_Id);

            // InValide State
            attendanceState.Invalid_Note = this.Calculate_Invalid_Note(tainee_Id);
            attendanceState.Invalid_Sanction = sanctionBLO.Find_Current_Invalid_Sanction(tainee_Id);

            return attendanceState;
        }

        private float Calculate_Invalid_Note(long tainee_Id)
        {
            float InValid_Note = 20;
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
            var InValid_sanctions = sanctionBLO.Find_InValide_Sanction(tainee_Id);
            foreach (var item in InValid_sanctions)
            {
                InValid_Note -= item.SanctionCategory.Deducted_Points;
            }
            InValid_Note -= (20 - this.Calculate_Valid_Note( tainee_Id));

            return InValid_Note;

        }

        private float Calculate_Valid_Note(long tainee_Id)
        {
            float Valid_Note = 0;
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
            var valid_sanctions = sanctionBLO.Find_Valide_Sanction(tainee_Id);
            foreach (var item in valid_sanctions)
            {
                Valid_Note += item.SanctionCategory.Deducted_Points;
            }
            return Valid_Note;
        }

 
    }
}
