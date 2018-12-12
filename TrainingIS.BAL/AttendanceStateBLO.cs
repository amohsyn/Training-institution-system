using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
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


        #region Update Sanctions
        public void Update(long tainee_Id)
        {
            var state = this.Find_Or_Create_AttendanceState(tainee_Id);
            this.Fill_AttendanceState(state);
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

        public AttendanceState Create_AttendanceState(long tainee_Id)
        {
            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this._UnitOfWork, this.GAppContext);

            AttendanceState attendanceState = this.CreateInstance();
            attendanceState.Trainee = traineeBLO.FindBaseEntityByID(tainee_Id);
            this.Fill_AttendanceState(attendanceState);
            return attendanceState;
        }

        public AttendanceState Fill_AttendanceState(AttendanceState attendanceState)
        {
            long tainee_Id = attendanceState.Trainee.Id;
           
           
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
 
            // Valide State
            attendanceState.Valid_Note = this.Calculate_Valid_Note(tainee_Id);
            attendanceState.Valid_Sanction = sanctionBLO.Find_Current_Valide_Sanction(tainee_Id);

            // InValide State
            attendanceState.Invalid_Note = this.Calculate_Invalid_Note(tainee_Id);
            attendanceState.Invalid_Sanction = sanctionBLO.Find_Current_Invalid_Sanction(tainee_Id);

            this.Save(attendanceState);
            return attendanceState;
        }

        private float Calculate_Invalid_Note(long tainee_Id)
        {
            float InValid_Note = 15;
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
            var InValid_sanctions = sanctionBLO.Find_InValide_Sanction(tainee_Id);
            foreach (var item in InValid_sanctions)
            {
                InValid_Note -= item.SanctionCategory.Deducted_Points;
            }
            InValid_Note -= (15 - this.Calculate_Valid_Note( tainee_Id));

            return InValid_Note;

        }

        private float Calculate_Valid_Note(long tainee_Id)
        {
            float Valid_Note = 15;
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
            var valid_sanctions = sanctionBLO.Find_Valide_Sanction(tainee_Id);
            foreach (var item in valid_sanctions)
            {
                Valid_Note -= item.SanctionCategory.Deducted_Points;
            }
            return Valid_Note;
        }

        public void RecalculateAttendanceState()
        {
            if (this.GAppContext.Current_User_Name != RoleBLO.Root_ROLE)
            {
                string msg_ex = "Vous devez être root pour exécuter cette action";
                throw new BLL_Exception(msg_ex);
            }

            // BLO
            TraineeBLO traineeBLO = new TraineeBLO(this._UnitOfWork, this.GAppContext);
            var all_trainnes = traineeBLO.FindAll();
            foreach (var trainee in all_trainnes)
            {
                this.Find_Or_Create_AttendanceState(trainee.Id);
            }
 
        }

    }
}
