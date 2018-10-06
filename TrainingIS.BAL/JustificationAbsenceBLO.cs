﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TrainingIS.BLL.Exceptions;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class JustificationAbsenceBLO
    {
        public override JustificationAbsence CreateInstance()
        {
            JustificationAbsence JustificationAbsence =  base.CreateInstance();
            JustificationAbsence.StartDate = DateTime.Now.AddDays(-1);

            DateTime yesterday = DateTime.Now.AddDays(-1);
            TimeSpan ts_yesterday = new TimeSpan(0, 0, 0);
            yesterday = yesterday.Date + ts_yesterday;
            JustificationAbsence.StartDate = yesterday;

            DateTime toDay = DateTime.Now;
            TimeSpan ts_toDay = new TimeSpan(23, 59, 59);
            toDay = toDay.Date + ts_toDay;
            JustificationAbsence.EndtDate = toDay;

            return JustificationAbsence;

        }

        public override int Save(JustificationAbsence item)
        {
            // BL :  StartDate < EndDate
            if(item.StartDate  >= item.EndtDate)
            {
                // [Localization]
                string msg_ex = string.Format("La date de fin ne doit pas être inférieur ou égale à la date de début");
                throw new BLL_Exception(msg_ex);
            }
                

            // Insert
            if(item.Id == 0)
            {
                // Save and Add_Justification_To_Absences
               
                var return_value = base.Save(item);
                this.Add_Justification_To_Absences(item);
               

                return return_value;
            }
            // Update
            else
            {
                this.Delete_Justification_Form_Absences(item);
                var return_value =  base.Save(item);
                this.Add_Justification_To_Absences(item);
                return return_value;
            }
            
        }

    

        public override int Delete(JustificationAbsence item)
        {
            var return_value = base.Delete(item);

            // Update All Absences in relation with this Justification
            this.Delete_Justification_Form_Absences(item);

            return return_value;
        }

        private void Delete_Justification_Form_Absences(JustificationAbsence item)
        {
            var AbsencesBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);
            var Absences_to_authorize = AbsencesBLO.GetAbsences(item);
            foreach (Absence absence in Absences_to_authorize)
            {
                absence.isHaveAuthorization = false;
                absence.JustificationAbsence = null;
                AbsencesBLO.Save(absence);
            }
        }
        private void Add_Justification_To_Absences(JustificationAbsence item)
        {
            // Authorize All Absences
            var AbsencesBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);
            var Absences_to_authorize = AbsencesBLO.GetAbsences(item);
            foreach (Absence absence in Absences_to_authorize)
            {
                absence.isHaveAuthorization = true;
                absence.JustificationAbsence = item;
                AbsencesBLO.Save(absence);
            }
        }

    }
}