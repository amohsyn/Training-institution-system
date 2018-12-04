using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;

namespace TrainingIS.Models.Absences
{
    /// <summary>
    /// Used to entry absence by the former and the supervisor
    /// </summary>
    public class Entry_Absence_Model
    {
        public override string ToString()
        {
            return string.Format("{0} {1}", this.TraineeFirstName, this.TraineeLastName);
        }
        public Entry_Absence_Model()
        {
          
        }

        // Trainee
        public Trainee Trainee { set; get; }
        public Int64 TraineeId { set; get; }
        public string TraineeFirstName { set; get; }
        public string TraineeLastName { set; get; }

        // Absence
        public Absence Absence { set; get; }
        private Int32? _AbsenceCount;
        public bool isHaveAuthorization { set; get; }
        public AbsenceStates AbsenceState { set; get; }

        // Absence Statistic
        public List<Entities.Absence> Absences_In_Current_Module { get; set; }
        public List<Entities.Absence> InValideAbsences { get; set; }
        
        // Seance Training
        public Int64 SeanceTrainingId { get; set; }

        // AttendanceState
        public Sanction Last_Valid_Attendance_Sanction { set; get; }
        public Sanction Last_Valid_Assiduite_Sanction { set; get; }
        public float? Valid_Note { set; get; }
        public float? Invalid_Note { set; get; }
        public virtual AttendanceState AttendanceState { set; get; }

        #region Properties
        private bool _IsAbsent;
        public bool IsAbsent
        {
            get
            {
                if (this.Absence == null)
                    return true;
                else
                    return false;
            }
        }
        public Int32? AbsenceCount
        {
            set
            {
                _AbsenceCount = value;

            }
            get
            {
                if (_AbsenceCount == null)
                    return 0;
                else
                    return _AbsenceCount;
            }
        }

        public int InValideAbsenceCount
        {
            get
            {
                if (this.InValideAbsences == null)
                    return 0;
                else
                    return this.InValideAbsences.Count();
            }
        }

        public Int32 AbsenceCount_In_Current_Module {

            get
            {
                if (this.Absences_In_Current_Module == null)
                    return 0;
                else
                    return this.Absences_In_Current_Module.Count();
            }
        }
        #endregion

    }
}
