﻿using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class SeanceTrainingBLO
    {
        /// <summary>
        /// Find witout pagination
        /// </summary>
        /// <param name="filterRequestParams"></param>
        /// <param name="SearchCreteria"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public IQueryable<SeanceTraining> Find_WithOut_Pagination(FilterRequestParams filterRequestParams, List<string> SearchCreteria, Func<SeanceTraining, bool> Condition = null)
        {
            UserBLO userBLO = new UserBLO(this.GAppContext);
            if (userBLO.Is_Current_User_Has_Role(RoleBLO.Former_ROLE))
            {
                this.Add_Former_Filter_Constraint(filterRequestParams);
                int totalRecords = 0;
                return base.entityDAO.Find_WithOut_Pagination(filterRequestParams, SearchCreteria, out totalRecords);
            }
            else
            {
                int totalRecords = 0;
                return base.entityDAO.Find_WithOut_Pagination(filterRequestParams, SearchCreteria, out totalRecords);
            }
        }

        public override IQueryable<SeanceTraining> Find_as_Queryable(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords, Func<SeanceTraining, bool> Condition = null)
        {
            UserBLO userBLO = new UserBLO(this.GAppContext);
            if (userBLO.Is_Current_User_Has_Role(RoleBLO.Former_ROLE))
            {
                this.Add_Former_Filter_Constraint(filterRequestParams);
                return base.Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);
            }
            else
            {
                return base.Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);
            }
        }
        private void Add_Former_Filter_Constraint(FilterRequestParams filterRequestParams)
        {
            Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former() as Former;
            if (former == null) throw new ArgumentNullException(nameof(Former));

            string FilterBy_Former = string.Format("[SeancePlanning.Training.Former.Id,{0}]", former.Id);
            if (string.IsNullOrEmpty(filterRequestParams.FilterBy))
            {
                filterRequestParams.FilterBy = FilterBy_Former;
            }
            else
            {
                filterRequestParams.FilterBy += ";" + FilterBy_Former;
            }
        }

      

       

        /// <summary>
        ///  Find All SeanceTraining according to User Role
        ///  For the former it return its seanceTrainig
        ///  For the PedagogicalDirector its return All SeanceTraining
        /// </summary>
        /// <returns></returns>
        public override List<SeanceTraining> FindAll()
        {
            int total;
            FilterRequestParams filterRequestParam = new FilterRequestParams();
            return this.Find_as_Queryable(filterRequestParam, null, out total).ToList();
        }


        public override int Save(SeanceTraining item)
        {
            // Insert
            if(item.Id == 0)
            {
                // checking the hourly mass
                float Trainings_HourlyMass = item.SeancePlanning.Training.Hourly_Mass_To_Teach;
                float Module_Hourly_Mass_To_Teach = item.SeancePlanning.Training.ModuleTraining.Hourly_Mass_To_Teach;

                if (Trainings_HourlyMass == 0)
                {
                    Trainings_HourlyMass = Module_Hourly_Mass_To_Teach;
                }

                var seance_duration_sum = this._UnitOfWork.context.SeanceTrainings
                .Where(s => s.SeancePlanning.Training.Id == item.SeancePlanning.Training.Id)
                .Select(s => DbFunctions.DiffMinutes(s.SeancePlanning.SeanceNumber.StartTime, s.SeancePlanning.SeanceNumber.EndTime))
                .Sum();
                float Current_HourlyMass = (float)Convert.ToInt32(seance_duration_sum) / 60F;

                Current_HourlyMass = Current_HourlyMass + (float)item.SeancePlanning.SeanceNumber.Duration() / 60F;

                if (Convert.ToDouble(Current_HourlyMass) <= Trainings_HourlyMass)
                {
                    return base.Save(item);
                }
                else
                {

                    string msg_ex = string.Format("La masse horaire {0:0.##} heures du module a été achevée, vous ne pouvez pas ajouter une autre séance", Trainings_HourlyMass);
                    throw new BLL_Exception(msg_ex);
                }
            }
            // Update
            else
            {
                return base.Save(item);
            }
          


            
        }
        public override int Delete(SeanceTraining item)
        {
            return base.Delete(item);
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

        //public void Create_Not_Created_SeanceTraining()
        //{
        //    AbsenceBLO AbsenceBLO = new AbsenceBLO(this._UnitOfWork, this.GAppContext);
        //    List<Absence> All_Absences = AbsenceBLO.FindAll();

        //    foreach (Absence absence in All_Absences)
        //    {

        //         SeanceTraining seanceTraining = this.CreateIfNotExist(absence.AbsenceDate, absence.SeancePlanningId);

        //        if(absence.SeanceTraining == null)
        //        {
        //            absence.SeanceTraining = seanceTraining;
        //            absence.SeanceTrainingId = seanceTraining.Id;
        //            AbsenceBLO.Save(absence);
        //        }
        //    }
        //}
    }
}
