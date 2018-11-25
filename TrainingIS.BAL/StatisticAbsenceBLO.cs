using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using TrainingIS.Entities;
using TrainingIS.Models.StatisticAbsence;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using System.Linq.Dynamic;
using System.Data;

namespace TrainingIS.BLL
{
    public class StatisticAbsenceBLO : Base_NotDb_BLO
    {
        public StatisticAbsenceBLO(GAppContext GAppContext) : base(GAppContext)
        {
        }

        public Statistic Calculate(StatisticAbsenceForm statisticAbsenceForm)
        {
            Statistic statistic = new Statistic();
            statistic.StartDate = statisticAbsenceForm.StartDate;
            statistic.EndDate = statisticAbsenceForm.EndDate;
            statistic.StatisticSelectors = statisticAbsenceForm.Selected_StatisticSelectors;
            statistic.GroupId = statisticAbsenceForm.GroupId;
            statistic.AbsenceState = statisticAbsenceForm.AbsenceState;
            // Group
            List<Group> Groups = new List<Group>();
            if (statisticAbsenceForm.GroupId == 0)
            {

                statistic.Name = "Statistique d'absence - tous les groupes";
            }
            else
            {

                Group group = new GroupBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByID(statisticAbsenceForm.GroupId);
                statistic.Name = "Statistique d'absence - " + group.Code;
                statistic.Group = group;

            }

            string Statistic_SQL_Query = this.Get_Statistic_SQL_Query(statistic);
            statistic.StatisticAbsenceValues = this.UnitOfWork.context.Database.SqlQuery<StatisticAbsenceValue>(Statistic_SQL_Query).ToList();

            statistic.DataTable = this.CreateDataTable(statistic);

            return statistic;
        }


        #region SQL
        public string Get_Statistic_SQL_Query(Statistic statistic)
        {
            string SQL_Query_Absence_Table = this.Get_SQL_Query_Absence_Table(statistic);
            string SQL_Query_Presence_Table = this.Get_SQL_Query_Presence_Table(statistic);
            string sql = "Select AbsenceTable.* , PresenceTable.Presence , ";
            sql += "Convert(decimal, AbsenceTable.AbsenceCount) / Convert(decimal, PresenceTable.Presence) * 100 as Percentage ";
            sql += string.Format("from ({0}) as PresenceTable", SQL_Query_Presence_Table);
            sql += string.Format(", ({0}) as AbsenceTable", SQL_Query_Absence_Table);
            sql += " where AbsenceTable.reference = PresenceTable.reference";
            return sql;
        }

        private string Get_SQL_Query_Presence_Table(Statistic statistic)
        {
            // Select 
            string sql = "Select CONVERT(bigint, Count(*)) as Presence, ";
            sql += this.Get_Select_Clause(statistic.StatisticSelectors);

            // From
            sql += "from SeanceTrainings ";

            // Join
            sql += "Join SeancePlannings on SeanceTrainings.SeancePlanningId = SeancePlannings.Id ";
            sql += "Join Trainings on SeancePlannings.TrainingId = Trainings.Id ";
            sql += "Join Groups on Trainings.GroupId = Groups.Id ";
            sql += "Join Trainees on Trainees.GroupId = Groups.Id ";
            sql += "join SeanceDays on SeancePlannings.SeanceDayId = SeanceDays.Id ";
            sql += "join SeanceNumbers on SeancePlannings.SeanceNumberId = SeanceNumbers.Id ";
            sql += "join ModuleTrainings on Trainings.ModuleTrainingId = ModuleTrainings.Id ";
            sql += "join Formers on Trainings.FormerId = Formers.Id ";

            // Where
            sql += string.Format("Where SeanceTrainings.SeanceDate >= CONVERT(date, '{0}',103) and SeanceTrainings.SeanceDate <= CONVERT(date, '{1}',103) ",
                statistic.StartDate.Date.ToShortDateString(), statistic.EndDate.Date.ToShortDateString());

            if (statistic.GroupId != 0)
            {
                sql += string.Format(" and Groups.Id = {0} ", statistic.GroupId.ToString());
            }



            // Group By
            sql += this.Get_GroupBy_Clause(statistic.StatisticSelectors);

            return sql;
        }

        private string Get_SQL_Query_Absence_Table(Statistic statistic)
        {

            // Select 
            string sql = "Select CONVERT(bigint, Count(*)) as AbsenceCount, ";
            sql += this.Get_Select_Clause(statistic.StatisticSelectors);

            // From
            sql += "from Absences ";

            // Join
            sql += "join Trainees on Absences.TraineeId = Trainees.Id ";
            sql += "join SeanceTrainings on Absences.SeanceTrainingId = SeanceTrainings.id ";
            sql += "join SeancePlannings on SeanceTrainings.SeancePlanningId = SeancePlannings.Id ";


            sql += "join SeanceDays on SeancePlannings.SeanceDayId = SeanceDays.Id ";
            sql += "join SeanceNumbers on SeancePlannings.SeanceNumberId = SeanceNumbers.Id ";
            sql += "join Trainings on SeancePlannings.TrainingId = Trainings.Id ";
            sql += "join ModuleTrainings on Trainings.ModuleTrainingId = ModuleTrainings.Id ";
            sql += "join Groups on Trainings.GroupId = Groups.Id ";
            sql += "join Formers on Trainings.FormerId = Formers.Id ";

            // Where
            sql += string.Format("Where Absences.AbsenceDate >= CONVERT(date, '{0}',103) and Absences.AbsenceDate <= CONVERT(date, '{1}',103) ",
                statistic.StartDate.Date.ToShortDateString(), statistic.EndDate.Date.ToShortDateString());

           
             // Count only not by  AbsenceState :  isHaveAuthorization = false 
             sql += string.Format(" and Absences.AbsenceState = {0} ", ((int) statistic.AbsenceState).ToString());
           
          


            if (statistic.GroupId != 0)
            {
                sql += string.Format(" and Groups.Id = {0} ", statistic.GroupId.ToString());
            }



            // Group By
            sql += this.Get_GroupBy_Clause(statistic.StatisticSelectors);

            return sql;
        }

        private string Get_Select_Clause(List<string> statisticSelectors)
        {
            string sql = "";
            List<string> List_SQL = new List<string>();
            foreach (string statisticSelector in statisticSelectors)
            {
                string Select_Clause_By_Selector = this.Get_Select_Clause_By_Selector(statisticSelector, statisticSelectors);
                if (!string.IsNullOrEmpty(Select_Clause_By_Selector))
                    List_SQL.Add(Select_Clause_By_Selector);
            }
            List_SQL.Add(this.Get_Select_Reference(statisticSelectors));
            return sql + string.Join(",", List_SQL);
        }

        /// <summary>
        /// Get The reference of the row of Statistic Absence
        /// </summary>
        /// <param name="statisticSelectors"></param>
        /// <returns></returns>
        private string Get_Select_Reference(List<string> statisticSelectors)
        {
            List<string> SelectorsTables = new List<string>();
            foreach (var item in statisticSelectors)
            {
                SelectorsTables.Add(string.Format("{0}.Reference", item.Pluralize()));
            }
            if (SelectorsTables.Count > 1)
            {
                string sql = string.Format("({0}) as Reference ", string.Join("+", SelectorsTables));
                return sql;
            }
            else
            {
                string sql = string.Format("{0} as Reference ", SelectorsTables.First());
                return sql;
            }

        }

        private string Get_GroupBy_Reference(List<string> statisticSelectors)
        {
            List<string> SelectorsTables = new List<string>();
            foreach (var item in statisticSelectors)
            {
                SelectorsTables.Add(string.Format("{0}.Reference", item.Pluralize()));
            }
            string sql = string.Join(",", SelectorsTables) + " ";
            return sql;
        }

        private string Get_GroupBy_Clause(List<string> statisticSelectors)
        {
            string sql = "Group by ";
            List<string> List_SQL = new List<string>();
            foreach (string statisticSelector in statisticSelectors)
            {
                List_SQL.Add(this.Get_GroupBy_Clause_By_Selector(statisticSelector));
            }
            List_SQL.Add(this.Get_GroupBy_Reference(statisticSelectors));
            return sql + string.Join(",", List_SQL);
        }


        private string Get_Select_Clause_By_Selector(string statisticSelector, List<string> statisticSelectors)
        {
            string sql = "";
            switch (statisticSelector)
            {
                case nameof(Trainee):
                    {
                        sql += "Trainees.CNE as TraineeCNE ,Trainees.FirstName as TraineeFirstName, Trainees.LastName as TraineeLastName, Groups.Code as GroupCode  ";
                    }
                    break;
                case nameof(Group):
                    {
                        if (!statisticSelectors.Contains(nameof(Trainee)))
                            sql += "Groups.Code as GroupCode  ";
                    }
                    break;
                case nameof(ModuleTraining):
                    {
                        sql += "ModuleTrainings.Code as ModuleTrainingCode, ModuleTrainings.Name as ModuleTrainingName ";
                    }
                    break;
                case nameof(Former):
                    {
                        sql += "Formers.FirstName as FormerFirstName , Formers.LastName as FormerLastName ";
                    }
                    break;
                case nameof(SeanceNumber):
                    {
                        sql += "SeanceNumbers.Code as SeanceNumberCode ";
                    }
                    break;
                case nameof(SeanceDay):
                    {
                        sql += "SeanceDays.Code as SeanceDayCode ";
                    }
                    break;
            }
            return sql;
        }

        private string Get_GroupBy_Clause_By_Selector(string statisticSelector)
        {
            string sql = "";
            switch (statisticSelector)
            {
                case nameof(Trainee):
                    {
                        sql += "Trainees.CNE, Trainees.FirstName, Trainees.LastName, Groups.Code ";
                    }
                    break;
                case nameof(Group):
                    {
                        sql += "Groups.Code ";
                    }
                    break;
                case nameof(ModuleTraining):
                    {
                        sql += "ModuleTrainings.Code, ModuleTrainings.Name ";
                    }
                    break;
                case nameof(Former):
                    {
                        sql += "Formers.FirstName, Formers.LastName ";
                    }
                    break;
                case nameof(SeanceNumber):
                    {
                        sql += "SeanceNumbers.Code ";
                    }
                    break;
                case nameof(SeanceDay):
                    {
                        sql += "SeanceDays.Code ";
                    }
                    break;
            }
            return sql;
        }
        #endregion


        #region DataTable
        private DataTable CreateDataTable(Statistic statistic)
        {
            DataTable dataTable = new DataTable();

            Type StatisticAbsenceValueType = typeof(StatisticAbsenceValue);




            // Trainee
            if (statistic.StatisticSelectors.Contains(nameof(Trainee)))
            {
                // TraineeCNE
                DataColumn TraineeCNE_Column = new DataColumn();
                TraineeCNE_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.TraineeCNE)).getLocalName(); ;
                dataTable.Columns.Add(TraineeCNE_Column);

                // TraineeFirstName
                DataColumn TraineeFirstName_Column = new DataColumn();
                TraineeFirstName_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.TraineeFirstName)).getLocalName(); ;
                dataTable.Columns.Add(TraineeFirstName_Column);

                // TraineeLastName
                DataColumn TraineeLastName_Column = new DataColumn();
                TraineeLastName_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.TraineeLastName)).getLocalName();
                dataTable.Columns.Add(TraineeLastName_Column);
            }

            // Group
            if (statistic.StatisticSelectors.Contains(nameof(Trainee)) || statistic.StatisticSelectors.Contains(nameof(Group)))
            {
                // GroupCode
                DataColumn GroupCode_Column = new DataColumn();
                GroupCode_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.GroupCode)).getLocalName();
                dataTable.Columns.Add(GroupCode_Column);
            }

            // Former
            if (statistic.StatisticSelectors.Contains(nameof(Former)))
            {
                // FormerFirstName
                DataColumn FormerFirstName_Column = new DataColumn();
                FormerFirstName_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.FormerFirstName)).getLocalName();
                dataTable.Columns.Add(FormerFirstName_Column);
                // FormerLastName
                DataColumn FormerLastName_Column = new DataColumn();
                FormerLastName_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.FormerLastName)).getLocalName();
                dataTable.Columns.Add(FormerLastName_Column);
            }

            // ModuleTraining
            if (statistic.StatisticSelectors.Contains(nameof(ModuleTraining)))
            {
                // ModuleTrainingCode
                DataColumn ModuleTrainingCode_Column = new DataColumn();
                ModuleTrainingCode_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.ModuleTrainingCode)).getLocalName();
                dataTable.Columns.Add(ModuleTrainingCode_Column);

                // ModuleTrainingName
                DataColumn ModuleTrainingName_Column = new DataColumn();
                ModuleTrainingName_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.ModuleTrainingName)).getLocalName();
                dataTable.Columns.Add(ModuleTrainingName_Column);
            }

            // SeanceNumber
            if (statistic.StatisticSelectors.Contains(nameof(SeanceNumber)))
            {
                // SeanceNumberCode
                DataColumn SeanceNumberCode_Column = new DataColumn();
                SeanceNumberCode_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.SeanceNumberCode)).getLocalName();
                dataTable.Columns.Add(SeanceNumberCode_Column);
            }

            // SeanceDay
            if (statistic.StatisticSelectors.Contains(nameof(SeanceDay)))
            {
                // SeanceDayCode
                DataColumn SeanceDayCode_Column = new DataColumn();
                SeanceDayCode_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.SeanceDayCode)).getLocalName();
                dataTable.Columns.Add(SeanceDayCode_Column);
            }

            // Présence : SeanceTrainingsCount
            DataColumn SeanceTrainingsCount_Column = new DataColumn();
            SeanceTrainingsCount_Column.DataType = typeof(Int64);
            SeanceTrainingsCount_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.Presence)).getLocalName();
            dataTable.Columns.Add(SeanceTrainingsCount_Column);

            // AbsenceCount
            DataColumn AbsenceCount_Column = new DataColumn();
            AbsenceCount_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.AbsenceCount)).getLocalName();
            AbsenceCount_Column.DataType = typeof(Int64);
            dataTable.Columns.Add(AbsenceCount_Column);

            // Percentage
            DataColumn Percentage_Column = new DataColumn();
            Percentage_Column.DataType = typeof(Decimal);
           
            Percentage_Column.ColumnName = StatisticAbsenceValueType.GetProperty(nameof(StatisticAbsenceValue.Percentage)).getLocalName(); ;
            dataTable.Columns.Add(Percentage_Column);

            foreach (StatisticAbsenceValue statisticAbsenceValue in statistic.StatisticAbsenceValues)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (PropertyInfo Property in statisticAbsenceValue.GetType().GetProperties())
                {


                    if (dataTable.Columns.Contains(Property.getLocalName()))
                    {

                        if (Property.Name == nameof(StatisticAbsenceValue.Percentage))
                        {

                            Decimal percentage = (Decimal)Property.GetValue(statisticAbsenceValue);
                            dataRow[Property.getLocalName()] =  percentage;
                           // dataRow[Property.getLocalName()] = String.Format("{0:0.##}", percentage);
                        }
                        else
                        {
                            dataRow[Property.getLocalName()] = Property.GetValue(statisticAbsenceValue);
                        }

                    }



                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;

        }
        #endregion

    }
}
