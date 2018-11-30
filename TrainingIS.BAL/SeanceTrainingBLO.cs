using GApp.DAL;
using GApp.Models.DataAnnotations;
using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.DAL;
using TrainingIS.Entities;
using TrainingIS.Models.SeanceInfos;

namespace TrainingIS.BLL
{
    public partial class SeanceTrainingBLO
    {
        public override List<string> GetSearchCreteria()
        {
          
            List<string> SearchCreteria = new List<string>();
            foreach (PropertyInfo model_property in typeof(SeanceInfo).GetProperties(typeof(GAppDataTableAttribute)))
            {
                GAppDataTableAttribute gappDataTableAttribute = model_property.GetCustomAttribute(typeof(GAppDataTableAttribute)) as GAppDataTableAttribute;
                string SearchBy = string.IsNullOrEmpty(gappDataTableAttribute.SearchBy) ? model_property.Name : gappDataTableAttribute.SearchBy;
                SearchCreteria.Add(SearchBy);
            }
            foreach (PropertyInfo model_property in typeof(SeanceInfo).GetProperties(typeof(SearchByAttribute)))
            {
                var attributes = model_property.GetCustomAttributes(typeof(SearchByAttribute));
                foreach (var attribute in attributes)
                {
                    SearchCreteria.Add((attribute as SearchByAttribute).PropertyPath);
                }

            }

            // SearchBy of Entity
            var entity_attributes = typeof(SeanceInfo).GetCustomAttributes(typeof(SearchByAttribute));
            foreach (var attribute in entity_attributes)
            {
                SearchCreteria.Add((attribute as SearchByAttribute).PropertyPath);
            }
            return SearchCreteria;
        }

        public override int Save(SeanceTraining item)
        {
            


            //
            // Persist information can be changed after
            //
            // Insert Duraction
            // the information must be persisted Because the SeanceNumber can be changed after
            // the seanceTraining creation
            item.Duration = item.SeancePlanning.SeanceNumber.Duration();

            // Insert
            if (item.Id == 0)
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

                    // is Current former is the former of SeanceTraining
                    if (this.Is_Current_Former_is_the_former_of_SeancePlanning(item.SeancePlanning))
                    {
                        item.FormerValidation = true;
                    }



                    var r = base.Save(item);
                    this.CalculatePlurality(item.SeancePlanning.TrainingId);
                    return r;

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
                // d'ont Update the Pluralty
                // Find the object from DataBase

                // CalculatePlurality case
                if (item.Plurality == 0)
                {
                    var item_db = this.Find_From_DB(item.Id);
                    item.Plurality = item_db.Plurality;
                }

                // is Current former is the former of SeanceTraining
                if (this.Is_Current_Former_is_the_former_of_SeancePlanning(item.SeancePlanning))
                {
                    item.FormerValidation = true;
                }
                
                return base.Save(item);
            }




        }

        private bool Is_Current_Former_is_the_former_of_SeancePlanning(SeancePlanning seancePlanning)
        {
            // BLO
            FormerBLO formerBLO = new FormerBLO(this._UnitOfWork, this.GAppContext);
            Former former = formerBLO.Get_Current_Former();
            if(former != null && seancePlanning.Training.Former.Id == former.Id)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// [Generalize]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private SeanceTraining Find_From_DB(long id)
        {
            SeanceTraining seanceTraining = null;
            UnitOfWork<TrainingISModel> unitOfWork = new UnitOfWork<TrainingISModel>();
            seanceTraining = new SeanceTrainingBLO(unitOfWork, this.GAppContext)
                 .FindBaseEntityByID(id);

            return seanceTraining;
        }

        private void CalculatePlurality(Int64 TrainingId)
        {

            this.Calculate_Plurality_for_all_SeanceTraining(TrainingId);


            // [Optimization] - Update only the pluralty of the seance item

            //var SeanceTraining_Query = from seance in this._UnitOfWork.context.SeanceTrainings
            //                           where seance.SeancePlanning.TrainingId == item.SeancePlanning.TrainingId
            //                           select seance;

            //// if one of the seance Training else item has plurality == 0
            //int Seance_With_Plurality_0_Count = SeanceTraining_Query.Where(s => s.Plurality == 0).Count();
            //if(Seance_With_Plurality_0_Count >= 2)
            //{
            //    this.Calculate_Plurality_for_all_SeanceTraining(item);
            //}


            //// Insert new SeanceTraining



            //// Delete SeanceTraining

        }

        private void Calculate_Plurality_for_all_SeanceTraining(Int64 TrainingId)
        {
            var SeanceTraining_Query = from seance in this._UnitOfWork.context.SeanceTrainings
                                       where seance.SeancePlanning.TrainingId == TrainingId
                                       orderby seance.SeanceDate, seance.SeancePlanning.SeanceNumber.StartTime
                                       select seance;
            int plurality = 0;
            foreach (SeanceTraining seanceTraining in SeanceTraining_Query.ToList())
            {
                plurality += seanceTraining.SeancePlanning.SeanceNumber.Duration();
                seanceTraining.Plurality = plurality;
                this.Save(seanceTraining);
            }


        }

       


        /// <summary>
        /// Find witout pagination
        /// </summary>
        /// <param name="filterRequestParams"></param>
        /// <param name="SearchCreteria"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public IQueryable<SeanceTraining> Find_WithOut_Pagination(FilterRequestParams filterRequestParams, List<string> SearchCreteria, Func<SeanceTraining, bool> Condition = null)
        {
            UserBLO userBLO = new UserBLO(this._UnitOfWork, this.GAppContext);
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
            UserBLO userBLO = new UserBLO(this._UnitOfWork, this.GAppContext);
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
        public void Add_Former_Filter_Constraint(FilterRequestParams filterRequestParams)
        {
            Former former = new FormerBLO(this._UnitOfWork, this.GAppContext).Get_Current_Former() as Former;
            if (former != null)
            {
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



        public override int Delete(SeanceTraining item)
        {

            Int64 TrainingId = item.SeancePlanning.TrainingId;
            var r = base.Delete(item);
            this.CalculatePlurality(TrainingId);
            return r;
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
            seanceTraining.SeanceDate = SeanceDate.Date;

            string SeanceTraining_Reference = seanceTraining.CalculateReference();

            SeanceTraining Existant_seanceTraining = this.Find(seancePlanning, SeanceDate.Date);
            if (Existant_seanceTraining == null)
            {
                this.Save(seanceTraining);
                return seanceTraining;
            }
            return Existant_seanceTraining;
        }

      
        public void Calculate_Plurality()
        {
            var Trainings = this._UnitOfWork.context.Trainings;


            foreach (var trainings in Trainings.ToList())
            {
                var SeanceTraining_Query = from seance in this._UnitOfWork.context.SeanceTrainings
                                           where seance.SeancePlanning.TrainingId == trainings.Id
                                           orderby seance.SeanceDate, seance.SeancePlanning.SeanceNumber.StartTime
                                           select seance;
                int plurality = 0;
                foreach (SeanceTraining seanceTraining in SeanceTraining_Query.ToList())
                {
                    plurality += seanceTraining.SeancePlanning.SeanceNumber.Duration();
                    seanceTraining.Plurality = plurality;
                    this.Save(seanceTraining);
                }
            }


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
        #region Find
        public SeanceTraining Find_By_Former_Date_Seance(Former former, DateTime seanceDate, SeanceNumber seanceNumber)
        {
            var query = from s in this._UnitOfWork.context.SeanceTrainings
                        where s.SeancePlanning.Training.Former.Id == former.Id
                        && s.SeanceDate == seanceDate.Date
                        && s.SeancePlanning.SeanceNumber.Id == seanceNumber.Id
                        select s;
            return query.FirstOrDefault();
        }
        public SeanceTraining Find(SeancePlanning seancePlanning, DateTime date)
        {
            var query = from s in this._UnitOfWork.context.SeanceTrainings
                        where s.SeancePlanning.Id == seancePlanning.Id && s.SeanceDate == date.Date
                        select s;
            return query.FirstOrDefault();
        }
        public SeanceTraining Find(Int64 seancePlanningId, DateTime date)
        {
            var query = from s in this._UnitOfWork.context.SeanceTrainings
                        where s.SeancePlanning.Id == seancePlanningId && s.SeanceDate == date.Date
                        select s;
            return query.FirstOrDefault();
        }

        #endregion
    }
}
