﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class SeanceTrainingBLO
    {

        public override int Save(SeanceTraining item)
        {
            return base.Save(item);
        }
        public override int Delete(SeanceTraining item)
        {
            return base.Delete(item);
        }

        /// <summary>
        ///  Find All SeanceTraining according to User Role
        ///  For the former it return its seanceTrainig
        ///  For the PedagogicalDirector its return All SeanceTraining
        /// </summary>
        /// <returns></returns>
        public override List<SeanceTraining> FindAll()
        {
            List<SeanceTraining> SeanceTrainings;
            UserBLO userBLO = new UserBLO(this.GAppContext);
            if (userBLO.Is_Current_User_Has_Role(RoleBLO.Former_ROLE))
            {
                Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former() as Former;
                if (former == null) throw new ArgumentNullException(nameof(Former));

                SeanceTrainings = (from s in this._UnitOfWork.context.SeanceTrainings
                                   where s.SeancePlanning.Training.Former.Id == former.Id
                                   orderby  s.SeanceDate descending
                                   select s).ToList();
            }
            else
            {
                SeanceTrainings = (from s in this._UnitOfWork.context.SeanceTrainings
                                   orderby s.SeanceDate descending
                                   select s).ToList();
            }

            return SeanceTrainings;
        }

        public string GetReference(SeanceTraining seanceTraining)
        {
            string reference = "";



            return reference;
        }

        public SeanceTraining CreateIfNotExist(DateTime SeanceDate, long seancePlanningId)
        {
            SeancePlanning seancePlanning = new SeancePlanningBLO(this._UnitOfWork, this.GAppContext).FindBaseEntityByID(seancePlanningId);

            SeanceTraining seanceTraining = this.CreateInstance();
            seanceTraining.SeancePlanning = seancePlanning;
            seanceTraining.SeancePlanningId = seancePlanning.Id;
            seanceTraining.SeanceDate = SeanceDate;

            string SeanceTraining_Reference = seanceTraining.CalculateReference();

            SeanceTraining Existant_seanceTraining = this.FindBaseEntityByReference(SeanceTraining_Reference);
            if(Existant_seanceTraining == null)
            {
                this.Save(seanceTraining);
                return seanceTraining;
            }
            return Existant_seanceTraining;
        }

        public void Create_Not_Created_SeanceTraining()
        {
            AbsenceBLO AbsenceBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);
            List<Absence> All_Absences = AbsenceBLO.FindAll();

            foreach (Absence absence in All_Absences)
            {

                 SeanceTraining seanceTraining = this.CreateIfNotExist(absence.AbsenceDate, absence.SeancePlanningId);

                if(absence.SeanceTraining == null)
                {
                    absence.SeanceTraining = seanceTraining;
                    absence.SeanceTrainingId = seanceTraining.Id;
                    AbsenceBLO.Save(absence);
                }
            }
        }
    }
}