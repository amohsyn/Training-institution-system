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
                                        where absence.isHaveAuthorization == false
                                        select absence;
            return not_authorized_absences;

        }
        #endregion

        #region CRUD
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
        public List<Absence> Find_Absences_By_States(long Trainee_Id, AbsenceStates absenceStates)
        {
            return this._UnitOfWork.context
                .Absences
                .Where(a=>a.Trainee.Id == Trainee_Id)
                .Where(a => a.AbsenceState == absenceStates).ToList();

        }
        #endregion

        #region States
        public void ChangeState_justified_Absence(Absence absence)
        {
            if(absence.AbsenceState == AbsenceStates.Valid_Absence 
                || absence.AbsenceState == AbsenceStates.InValid_Absence)
            {
                absence.AbsenceState = AbsenceStates.Justified_Absence;
                absence.isHaveAuthorization = true;
                this.Save(absence);
            }
            else
            {
                if(absence.AbsenceState == AbsenceStates.Sanctioned_Absence)
                {
                    if(absence.Sanction != null)
                    {
                        //[Localization]
                        string msg_ex = string.Format("L'absence que vous êtes entrain de justifier '{0}' est déja sanctionée par la sanction {1}"
                                               , absence, absence.Sanction);
                        throw new BLL_Exception("");
                    }
                    else
                    {
                        string msg_ex = string.Format("L'absence '{0}' est avec l'état {1} mais il n'a pas de sanction"
                                                                      , absence,  absence.AbsenceState);
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
                sanctionBLO.Update_InValide_Sanction(absence.Trainee.Id);

                absence.AbsenceState = AbsenceStates.Valid_Absence;
                absence.Valide = true;
                this.Save(absence);
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
            if(this.GAppContext.Current_User_Name == RoleBLO.Admin_ROLE)
            {
                var Absences_InValid = this._UnitOfWork.context.Absences.Where(a => a.Valide == false).ToList();

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
            var All_Absences = this.FindAll();
            GAppContext.Session.Add(ImportService.IMPORT_PROCESS_KEY, true);

            foreach (var absence in All_Absences)
            {
                if(absence.AbsenceState == AbsenceStates.InValid_Absence)
                {
                    if (absence.isHaveAuthorization)
                    {
                        absence.AbsenceState = AbsenceStates.Justified_Absence;
                        this.Save(absence);
                        continue;
                    }
                    if (absence.Valide)
                    {
                        absence.AbsenceState = AbsenceStates.Valid_Absence;
                        this.Save(absence);
                        continue;
                    }
                }
              
            }
        }
        #endregion
    }
}
