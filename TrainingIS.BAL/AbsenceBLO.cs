using GApp.Exceptions;
using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.Entities;
using TrainingIS.Entities.enums;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL
{
    /// <summary>
    /// See olso : Entry_Absence_Model_BLM
    /// </summary>
    public partial class AbsenceBLO
    {
        #region Query
        public IQueryable<Absence> Absences_NotAuthorized_Query()
        {
            var not_authorized_absences = from absence in this._UnitOfWork.context.Absences
                                          where absence.AbsenceState != AbsenceStates.Justified_Absence
                                          select absence;
            return not_authorized_absences;

        }
        public IQueryable<Absence> Absences_Query()
        {
            var not_authorized_absences = from absence in this._UnitOfWork.context.Absences
                                          select absence;
            return not_authorized_absences;

        }
        #endregion

        #region CRUD

        /// <summary>
        /// Create Absence of Trainne of SeanceTraining
        /// </summary>
        /// <param name="traineeId"></param>
        /// <param name="seanceTainingId"></param>
        public void Create_Absence(long TraineeId, long SeanceTainingId)
        {
            SeanceTraining seanceTraining = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(SeanceTainingId);

            // Create Absence if not exist
            Absence absence = this.Find_By_TraineeId_SeanceTraining(TraineeId, SeanceTainingId);
            if (absence == null)
            {
                absence = this.CreateInstance();
                absence.TraineeId = TraineeId;
                absence.Trainee = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(TraineeId);
                absence.AbsenceDate = Convert.ToDateTime(seanceTraining.SeanceDate);
                absence.SeanceTraining = seanceTraining;
                absence.SeanceTrainingId = seanceTraining.Id;
                this.Save(absence);

            }
        }
        public void Delete_Absence(long TraineeId, long SeanceTainingId)
        {
            Absence absence = this.Find_By_TraineeId_SeanceTraining(TraineeId, SeanceTainingId);
            Trainee trainee = null;
            SeanceTraining seanceTraining = null;
            if (absence != null)
            {
                trainee = absence.Trainee;
                seanceTraining = absence.SeanceTraining;
                this.Delete(absence);
            }
            
        }
        
        /// <summary>
        /// Create Absences of Justified Absence for a SanceTraining
        /// </summary>
        /// <param name="SeaneTrainingId"></param>
        public void Create_Justified_Absences(long SeaneTrainingId)
        {
            // BLO
            JustificationAbsenceBLO justificationAbsenceBLO = new JustificationAbsenceBLO(this._UnitOfWork, this.GAppContext);
            SeanceTrainingBLO seanceTrainingBLO = new SeanceTrainingBLO(this._UnitOfWork, this.GAppContext);

            // Params
            SeanceTraining seanceTraining = seanceTrainingBLO.FindBaseEntityByID(SeaneTrainingId);
 
            // Find Justifications
            List<JustificationAbsence> Justifications = justificationAbsenceBLO.Find_By_Date_And_Group( Convert.ToDateTime( seanceTraining.SeanceDate), seanceTraining.SeancePlanning.Training.Group.Id);
            foreach (var justificationAbsence in Justifications)
            {
                var absence = this.CreateInstance();
                absence.TraineeId = justificationAbsence.TraineeId;
                absence.Trainee = new TraineeBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(absence.TraineeId);
                absence.AbsenceDate = Convert.ToDateTime(seanceTraining.SeanceDate);
                absence.SeanceTraining = seanceTraining;
                absence.SeanceTrainingId = seanceTraining.Id;
                absence.AbsenceState = AbsenceStates.Justified_Absence;
                this.Save(absence);
            }
        }
        public override int Save(Absence absence)
        {
            bool isImportProcess = GAppContext.Session.ContainsKey(ImportService.IMPORT_PROCESS_KEY) ? true : false;
            if (!isImportProcess)
            {
                this.Throw_GAppException_if_not_valide(absence);
            }

            int returned_value = base.Save(absence);
            return returned_value;
        }

        public override int Delete(Absence item)
        {
            if(item.AbsenceState != AbsenceStates.InValid_Absence)
            {
                string msg_ex = "Vous ne pouvez pas supprimer une absence Valide, Sanctionée ou Justifiée";
                throw new BLL_Exception(msg_ex);
            }
            this.Throw_GAppException_if_not_valide(item);

            Trainee trainee = item.Trainee;

            DateTime AbsenceDate = item.AbsenceDate;
            SeancePlanning seancePlanning = item.SeanceTraining.SeancePlanning;
            int returned_value = base.Delete(item);
            if (returned_value == 1)
            {
                StateOfAbseceBLO stateOfAbseceBLO = new StateOfAbseceBLO(this._UnitOfWork, this.GAppContext);
                stateOfAbseceBLO.Calculate_State_Of_Absence(trainee, AbsenceDate, seancePlanning, false);
            }

            return returned_value;
        }

        private void Throw_GAppException_if_not_valide(Absence absence)
        {
            if (absence.SeanceTrainingId == 0 && absence.SeanceTraining != null) absence.SeanceTrainingId = absence.SeanceTraining.Id;
            if (absence.TraineeId == 0 && absence.Trainee != null) absence.TraineeId = absence.Trainee.Id;


            SeancePlanning seancePlanning = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(absence.SeanceTraining.SeancePlanning.Id);

            // the trainne can be in the group different with the groupe of SeancePlanning , if he chage the groupe
            //// is the trainee has this SeancePlanning
            //if (!seancePlanning.Training.Group.Trainees.Select(t => t.Id).Contains(absence.TraineeId))
            //{
            //    string msg_ex = string.Format("The Trainee '{0}' not exsit in the group {1}", absence.TraineeId, seancePlanning.Training.Group.ToString());
            //    throw new GAppException(msg_ex);
            //}

            // if user is former
            Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former();
            if (former != null)
            {
                // is the seance Planing is for the current Former
                if (seancePlanning.Training.Former.Id != former.Id)
                {
                    string msg_ex = string.Format("The former {0} not have the SeancePlaning {1}", former.ToString(), seancePlanning.ToString());
                    throw new GAppException(msg_ex);
                }
            }
        }

      
        #endregion

        #region Find Absences By

        public int Count_NotJustified_Absences(long Trainee_Id)
        {
            return this._UnitOfWork.context
              .Absences
              .Where(a => a.Trainee.Id == Trainee_Id)
              .Where(a => (a.AbsenceState == AbsenceStates.Sanctioned_Absence || a.AbsenceState == AbsenceStates.Valid_Absence))
              .Count();
        }


        /// <summary>
        /// Get Abseces By Justification
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endtDate"></param>
        /// <returns></returns>
        public List<Absence> Find_By_TraineeId_StartDate_EndDate(long Trainee_Id, DateTime StartDate, DateTime EndtDate)
        {
            var query = from absence in this._UnitOfWork.context.Absences
                        where absence.AbsenceDate >= StartDate && absence.AbsenceDate <= EndtDate
                        && absence.Trainee.Id == Trainee_Id
                        select absence;

            return query.ToList();
        }

        public Absence Find_By_TraineeId_SeancePlanningId(long traineeId, long seancePlanningId, DateTime AbsenceDate)
        {
            Absence absence = this._UnitOfWork.context.Absences
                .Where(
                a => a.Trainee.Id == traineeId
                && a.SeanceTraining.SeancePlanning.Id == seancePlanningId
                && DbFunctions.TruncateTime(a.AbsenceDate) == DbFunctions.TruncateTime(AbsenceDate)
                ).FirstOrDefault();
            return absence;
        }
        public Absence Find_By_TraineeId_SeanceTraining(long traineeId, long seanceTrainingId)
        {
            Absence absence = this._UnitOfWork.context.Absences
                .Where(a => a.Trainee.Id == traineeId && a.SeanceTraining.Id == seanceTrainingId)
                .FirstOrDefault();
            return absence;
        }
        public List<Absence> Find_By_TraineeId(long traineeId)
        {
            var Query = this._UnitOfWork.context.Absences
                 .Where(a => a.Trainee.Id == traineeId)
                 .ToList();
            return Query;
        }
        public List<Absence> Find_Absences_By_States(long Trainee_Id, AbsenceStates absenceStates)
        {
            return this._UnitOfWork.context
                .Absences
                .Where(a => a.Trainee.Id == Trainee_Id)
                .Where(a => a.AbsenceState == absenceStates).ToList();

        }
        #endregion

        #region States
        public void ChangeState_to_Sanctioned(long Absence_Id)
        {
            Absence absence = this.FindBaseEntityByID(Absence_Id);
            absence.AbsenceState = AbsenceStates.Sanctioned_Absence;
            this.Save(absence);
        }
        public void ChangeState_justified_Absence(Absence absence)
        {
            if (absence.AbsenceState == AbsenceStates.Valid_Absence
                || absence.AbsenceState == AbsenceStates.InValid_Absence)
            {
                absence.AbsenceState = AbsenceStates.Justified_Absence;
                absence.isHaveAuthorization = true;
                this.Save(absence);
            }
            else
            {
                if (absence.AbsenceState == AbsenceStates.Sanctioned_Absence)
                {
                    if (absence.Sanction != null)
                    {
                        //[Localization]
                        string msg_ex = string.Format("L'absence que vous êtes entrain de justifier '{0}' est déja sanctionée par la sanction {1}"
                                               , absence, absence.Sanction);
                        throw new BLL_Exception("");
                    }
                    else
                    {
                        string msg_ex = string.Format("L'absence '{0}' est avec l'état {1} mais il n'a pas de sanction"
                                                                      , absence, absence.AbsenceState);
                        throw new GApp.Exceptions.GAppException(msg_ex);
                    }

                }
            }

        }

        public void ChangeState_to_Valid(Absence item)
        {
            // BLO
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);

            Absence absence = this.Load_if_not_attached_in_current_context(item);
            if (absence.AbsenceState == AbsenceStates.InValid_Absence)
            {


                absence.AbsenceState = AbsenceStates.Valid_Absence;
                absence.Valide = true;
                this.Save(absence);

                sanctionBLO.Update_InValide_Sanction(absence.Trainee.Id);
            }
            else
            {
                // [Localization]
                string msg_ex = string.Format("pour valider une absence il doit être non valide");
                throw new BLL_Exception(msg_ex);
            }


        }

        public void ChangeState_to_InValid(Absence item)
        {
            // BLO
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);

            Absence absence = this.Load_if_not_attached_in_current_context(item);
            if (absence.AbsenceState == AbsenceStates.Valid_Absence)
            {

                absence.AbsenceState = AbsenceStates.InValid_Absence;
                sanctionBLO.Update_InValide_Sanction(absence.Trainee.Id);
                absence.Valide = false;
                this.Save(absence);
            }
            else
            {
                // [Localization]
                string msg_ex = string.Format("pour dévalider une absence il doit être état non valide");
                throw new BLL_Exception(msg_ex);
            }


        }



        #endregion

        #region Used by Only Root User
        /// <summary>
        /// Valide All Absence if the user is Admin
        /// </summary>
        public void Validate_All_Absences()
        {
            if (this.GAppContext.Current_User_Name == RoleBLO.Admin_ROLE)
            {
                var Absences_InValid = this._UnitOfWork.context.Absences.Where(a => a.AbsenceState  == AbsenceStates.InValid_Absence).ToList();

                foreach (var item in Absences_InValid)
                {
                    this.ChangeState_to_Valid(item);
                }
            }
            else
            {
                // [Localization]       
                string msg_ex = string.Format("Vous devez être Admin, pour valider les absences de la base de données");
                throw new BLL_Exception(msg_ex);
            }
        }
        #endregion

        #region Obsolete Function
        [Obsolete("this funtion is used to correct the AbsenceState in version 0.0.6")]
        public void Correct_Absence_State()
        {
            // BLO
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);

            var All_Absences = this.FindAll();
            GAppContext.Session.Add(ImportService.IMPORT_PROCESS_KEY, true);

            var Trainees_Absences = All_Absences
                .GroupBy(a => a.Trainee)
                .Select(g => new { Trainee = g.Key, Absences = g.ToList() })
                .ToList();

            int i = 0;
            foreach (var trainee_absences in Trainees_Absences)
            {
                bool absences_changed = false;
                foreach (var absence in trainee_absences.Absences)
                {
                    // in the lase update - Change State to Update Attendance State
                    if (absence.AbsenceState == AbsenceStates.InValid_Absence)
                    {
                        if (absence.isHaveAuthorization)
                        {
                            absence.AbsenceState = AbsenceStates.Justified_Absence;
                            this.Save(absence);
                            absences_changed = true;
                            continue;
                        }
                        if (absence.Valide)
                        {
                            absence.AbsenceState = AbsenceStates.Valid_Absence;
                            this.Save(absence);
                            absences_changed = true;
                            continue;
                        }
                    }
                }

                if (absences_changed)
                    sanctionBLO.Update_InValide_Sanction(trainee_absences.Trainee.Id);

                i++;
            }
        }
        #endregion
    }
}
