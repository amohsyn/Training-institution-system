using GApp.Models.Pages.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Index_Absence_ModelBLM
    {
        #region Find
        public List<Index_Absence_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
           // Dictionary<string, string> Filters = this.GetFilters(out SearchBy);
    
            AbsenceBLO absenceBLO = new AbsenceBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Absence> Query_Entity = absenceBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);


            var Query_Index_Absence_Model = from absence in Query_Entity
                                            select new Index_Absence_Model
                                            {
                                                Id = absence.Id,
                                                AbsenceDate = absence.AbsenceDate,
                                                Trainee = absence.Trainee,
                                                Group = absence.SeanceTraining.SeancePlanning.Training.Group,
                                                SeanceTraining = absence.SeanceTraining,
                                                Valide = absence.Valide,
                                                isHaveAuthorization = absence.isHaveAuthorization,
                                                Description = absence.SeanceTraining.SeancePlanning.SeanceNumber.Code
                                                + " , " + absence.SeanceTraining.SeancePlanning.Training.Former.FirstName
                                                + " , " + absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Code
                                            };

            return Query_Index_Absence_Model.ToList();
        }

        public List<Index_Absence_Model> FindAll(string OrderBy, string SearchBy, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            Dictionary<string, string> Filters = this.GetFilters(out SearchBy);

            if (PageSize == null) PageSize = 25;
            if (CurrentPage == null) CurrentPage = 0;




            IQueryable<Absence> Query_Entity = from absence in this.UnitOfWork.context.Absences
                                                select absence;




            if (!string.IsNullOrEmpty(SearchBy))
            {
                Query_Entity = from absence in Query_Entity
                                where
                                absence.Trainee.FirstName.ToUpper().Contains(SearchBy.ToUpper())
                                || absence.Trainee.LastName.ToUpper().Contains(SearchBy.ToUpper())
                                || absence.AbsenceDate.ToString().Contains(SearchBy.ToUpper())
                                select absence;

            }



            if (!string.IsNullOrEmpty(OrderBy))
            {
                switch (OrderBy)
                {
                    case "AbsenceDate_Asc":
                        Query_Entity = Query_Entity.OrderBy(O => O.AbsenceDate);
                        break;

                    case "AbsenceDate_Desc":
                        Query_Entity = Query_Entity.OrderByDescending(O => O.AbsenceDate);
                        break;

                    case "Trainee_Asc":
                        Query_Entity = Query_Entity.OrderBy(O => O.Trainee.FirstName)
                            .OrderBy(O => O.Trainee.LastName);
                        break;

                    case "Trainee_Desc":
                        Query_Entity = Query_Entity.OrderByDescending(O => O.Trainee.FirstName)
                        .OrderByDescending(O => O.Trainee.LastName);
                        break;

                    case "Group_Asc":
                        Query_Entity = Query_Entity.OrderBy(O => O.SeanceTraining.SeancePlanning.Training.Group.Code);
                        break;

                    case "Group_Desc":
                        Query_Entity = Query_Entity.OrderByDescending(O => O.SeanceTraining.SeancePlanning.Training.Group.Code);
                        break;

                    case "isHaveAuthorization_Asc":
                        Query_Entity = Query_Entity.OrderBy(O => O.isHaveAuthorization);
                        break;

                    case "isHaveAuthorization_Desc":
                        Query_Entity = Query_Entity.OrderByDescending(O => O.isHaveAuthorization);
                        break;

                    case "Description_Asc":
                        Query_Entity = Query_Entity.OrderBy(O => O.SeanceTraining.SeancePlanning.SeanceNumber.Code);
                        break;

                    case "Description_Desc":
                        Query_Entity = Query_Entity.OrderByDescending(O => O.SeanceTraining.SeancePlanning.SeanceNumber.Code);
                        break;

                    case "Valide_Asc":
                        Query_Entity = Query_Entity.OrderBy(O => O.Valide);
                        break;

                    case "Valide_Desc":
                        Query_Entity = Query_Entity.OrderByDescending(O => O.Valide);
                        break;


                }
            }
            else
            {
                Query_Entity = Query_Entity.OrderBy(x => x.Id);
            }


            totalRecords = Query_Entity.Count();

            if (PageSize != null && CurrentPage != null)
            {
                if (CurrentPage == 0)
                {
                    Query_Entity = Query_Entity.Take((int)PageSize);
                }
                else
                {
                    Query_Entity = Query_Entity.Skip((int)PageSize * ((int)CurrentPage - 1))
                   .Take((int)PageSize);
                }

            }
           
            var Query_Index_Absence_Model = from absence in Query_Entity
                                            select new Index_Absence_Model
                                            {
                                                Id = absence.Id,
                                                AbsenceDate = absence.AbsenceDate,
                                                Trainee = absence.Trainee,
                                                Group = absence.SeanceTraining.SeancePlanning.Training.Group,
                                                SeanceTraining = absence.SeanceTraining,
                                                Valide = absence.Valide,
                                                isHaveAuthorization = absence.isHaveAuthorization,
                                                Description = absence.SeanceTraining.SeancePlanning.SeanceNumber.Code
                                                + " , " + absence.SeanceTraining.SeancePlanning.Training.Former.FirstName
                                                + " , " + absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Code
                                            };

          

            return Query_Index_Absence_Model.ToList();
        }

        /// <summary>
        /// Get the Filters from SearchBy
        /// </summary>
        /// <param name="searchBy"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetFilters(out string searchBy)
        {
            // [Group=TDI102], [Filière=TDI], [Date d'absence='10/09/2018']

            throw new NotImplementedException();
           
        }

        #endregion

        #region Convert 
        public override Index_Absence_Model ConverTo_Index_Absence_Model(Absence Absence)
        {
            Index_Absence_Model index_Absence_Model = base.ConverTo_Index_Absence_Model(Absence);

            if (index_Absence_Model.Trainee == null) return index_Absence_Model;

            var StateOfAbsences = index_Absence_Model.Trainee.StateOfAbseces;

            // Number_Absences_In_This_DayOfWeek
            StateOfAbsece StateOfAbsece_DayOfWeek = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.DayOfWeek && s.Name == Absence.AbsenceDate.DayOfWeek.ToString()).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_DayOfWeek = (StateOfAbsece_DayOfWeek == null) ? 0 : StateOfAbsece_DayOfWeek.Value;

            //Number_Absences_In_This_Week
            StateOfAbsece StateOfAbsece_Week = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.Week && s.Name == Absence.AbsenceDate.GetWeekOfYear().ToString()).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Week = (StateOfAbsece_Week == null) ? 0 : StateOfAbsece_Week.Value;

            // Number_Absences_In_This_Month
            StateOfAbsece StateOfAbsece_Month = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.Month && s.Name == Absence.AbsenceDate.Month.ToString()).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Month = (StateOfAbsece_Month == null) ? 0 : StateOfAbsece_Month.Value;

            // Number_Absences_In_This_Module
            StateOfAbsece StateOfAbsece_Module = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.Module && s.Name == Absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Reference).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Module = (StateOfAbsece_Module == null) ? 0 : StateOfAbsece_Module.Value;


            // Number_Absences_In_This_Month
            string Current_TrainingYear = (this.GAppContext.Session[TrainingYearBLO.Current_TrainingYear_Key] as TrainingYear).Reference;
            StateOfAbsece StateOfAbsece_Year = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.TrainingYear && s.Name == Current_TrainingYear).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Year = (StateOfAbsece_Year == null) ? 0 : StateOfAbsece_Year.Value;

            index_Absence_Model.Description = string.Format("{0}, {1}, {2}"
                , Absence.SeanceTraining.SeancePlanning.SeanceNumber.Code, Absence.SeanceTraining.SeancePlanning.Training.Former.ToString(), Absence.SeanceTraining.SeancePlanning.Training.ModuleTraining.Code);


            index_Absence_Model.StateOfAbsence = string.Format("comme ce jour : {0}, cette semaine : {1} , ce mois : {2}, cette année {3}, dans ce module {4}",
                index_Absence_Model.Number_Absences_In_This_DayOfWeek,
                index_Absence_Model.Number_Absences_In_This_Week,
                index_Absence_Model.Number_Absences_In_This_Month,
                index_Absence_Model.Number_Absences_In_This_Year,
                index_Absence_Model.Number_Absences_In_This_Module
                );
            return index_Absence_Model;
        }

        #endregion

    }
}
