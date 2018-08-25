using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class StateOfAbseceBLO
    {




        public void Calculate_State_Of_Absence(Trainee Trainee,DateTime AbsenceDate , SeancePlanning SeancePlanning, bool Add)
        {
            // In DayOfWeek
            StateOfAbseceCategories DayOfWeek = StateOfAbseceCategories.DayOfWeek;
            string DayOfWeek_Code = AbsenceDate.DayOfWeek.ToString();
            this.Number_Absence_In_Category_And_Code(Trainee, AbsenceDate, SeancePlanning, DayOfWeek, DayOfWeek_Code, Add);

            // In Week
            StateOfAbseceCategories Week = StateOfAbseceCategories.Week;
            string Week_Code = AbsenceDate.GetWeekOfYear().ToString();
            this.Number_Absence_In_Category_And_Code(Trainee,AbsenceDate, SeancePlanning, Week, Week_Code, Add);

            // In Month
            StateOfAbseceCategories Month = StateOfAbseceCategories.Month;
            string Month_Code = AbsenceDate.Month.ToString();
            this.Number_Absence_In_Category_And_Code(Trainee,AbsenceDate, SeancePlanning, Month, Month_Code, Add);

            // In Year
            StateOfAbseceCategories TrainingYear = StateOfAbseceCategories.TrainingYear;
            string TrainingYear_Code = (this.GAppContext.Session[TrainingYearBLO.Current_TrainingYear_Key] as TrainingYear).Reference;
            this.Number_Absence_In_Category_And_Code(Trainee,AbsenceDate, SeancePlanning, TrainingYear, TrainingYear_Code, Add);

            // In Module
            StateOfAbseceCategories Module = StateOfAbseceCategories.Module;
            string Module_Code = SeancePlanning.Training.ModuleTraining.Reference;
            this.Number_Absence_In_Category_And_Code(Trainee,AbsenceDate, SeancePlanning, Module, Module_Code, Add);

        }

        private void Number_Absence_In_Category_And_Code(Trainee Trainee, DateTime AbsenceDate, SeancePlanning SeancePlanning, StateOfAbseceCategories Category, string Code, bool Add)
        {
            StateOfAbsece stateOfAbsece = this.FindAll_By_Category_And_Code(Trainee,Category, Code);
            if (stateOfAbsece == null)
            {
                stateOfAbsece = this.CreateInstance();
                stateOfAbsece.Category = Category;
                stateOfAbsece.Name = Code;
                stateOfAbsece.Trainee = Trainee;
            }
            if (Add)
                stateOfAbsece.Value++;
            else
                stateOfAbsece.Value--;
            this.Save(stateOfAbsece);
        }

        private StateOfAbsece FindAll_By_Category_And_Code(Trainee Trainee , StateOfAbseceCategories category, string Name)
        {
            StateOfAbsece stateOfAbsece = this._UnitOfWork.context.StateOfAbseces
                .Where(s => s.Category == category && s.Name == Name && s.Trainee.Id == Trainee.Id).FirstOrDefault();
            return stateOfAbsece;
        }
    }
}
