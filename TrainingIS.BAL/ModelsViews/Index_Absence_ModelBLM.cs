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
        public override Index_Absence_Model ConverTo_Index_Absence_Model(Absence Absence)
        {
            Index_Absence_Model index_Absence_Model = base.ConverTo_Index_Absence_Model(Absence);



            if (index_Absence_Model.Trainee == null) return index_Absence_Model;

            var StateOfAbsences = index_Absence_Model.Trainee.StateOfAbseces;

            // Number_Absences_In_This_DayOfWeek
            StateOfAbsece StateOfAbsece_DayOfWeek = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.DayOfWeek && s.Name == Absence.AbsenceDate.DayOfWeek.ToString()).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_DayOfWeek = (StateOfAbsece_DayOfWeek==null) ? 0: StateOfAbsece_DayOfWeek.Value;

            //Number_Absences_In_This_Week
            StateOfAbsece StateOfAbsece_Week = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.Week && s.Name == Absence.AbsenceDate.GetWeekOfYear().ToString()).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Week = (StateOfAbsece_Week == null) ? 0 : StateOfAbsece_Week.Value;

            // Number_Absences_In_This_Month
            StateOfAbsece StateOfAbsece_Month = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.Month && s.Name == Absence.AbsenceDate.Month.ToString()).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Month = (StateOfAbsece_Month == null) ? 0 : StateOfAbsece_Month.Value;

            // Number_Absences_In_This_Module
            StateOfAbsece StateOfAbsece_Module = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.Module && s.Name == Absence.SeancePlanning.Training.ModuleTraining.Reference).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Module= (StateOfAbsece_Module == null) ? 0 : StateOfAbsece_Module.Value;


            // Number_Absences_In_This_Month
            string Current_TrainingYear = (this.GAppContext.Session[TrainingYearBLO.Current_TrainingYear_Key] as TrainingYear).Reference;
            StateOfAbsece StateOfAbsece_Year = StateOfAbsences.Where(s => s.Category == StateOfAbseceCategories.TrainingYear && s.Name == Current_TrainingYear).FirstOrDefault();
            index_Absence_Model.Number_Absences_In_This_Year = (StateOfAbsece_Year == null) ? 0 : StateOfAbsece_Year.Value;

            index_Absence_Model.Description = string.Format("{0}, {1}, {2}"
                , Absence.SeancePlanning.SeanceNumber.Code, Absence.SeancePlanning.Training.Former.ToString(), Absence.SeancePlanning.Training.ModuleTraining.Code);


            index_Absence_Model.StateOfAbsence = string.Format("comme ce jour : {0}, cette semaine : {1} , ce mois : {2}, cette année {3}, dans ce module {4}",
                index_Absence_Model.Number_Absences_In_This_DayOfWeek,
                index_Absence_Model.Number_Absences_In_This_Week,
                index_Absence_Model.Number_Absences_In_This_Month,
                index_Absence_Model.Number_Absences_In_This_Year,
                index_Absence_Model.Number_Absences_In_This_Module
                );
            return index_Absence_Model;
        }
    }
}
