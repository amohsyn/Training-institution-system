using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class SeancePlanningBLO
    {
        public override ImportReport Import(DataTable dataTable, string FileName = "")
        {
            try
            {
                // Check Existance of Schedule Reference
                string Schedule_Column_Name = typeof(SeancePlanning).GetProperty(nameof(SeancePlanning.Schedule)).getLocalName();
                int Schedule_Columns_Index = dataTable.Columns.IndexOf(Schedule_Column_Name);

                if (dataTable.Rows.Count > 1)
                {
                    string Schedule_Reference = dataTable.Rows[1][Schedule_Column_Name] as string;
                    if (string.IsNullOrEmpty(Schedule_Reference))
                    {
                        // Check existance of : Schedule_Columns_Index start Date
                        string Schedule_Start_Date_LocalName = typeof(Schedule).GetProperty(nameof(Schedule.StartDate)).getLocalName();
                        int Schedule_Start_Date_Columns_Index = dataTable.Columns.IndexOf(Schedule_Start_Date_LocalName);

                        if(Schedule_Start_Date_Columns_Index  > 0)
                        {
                            // Read Start_Date_Value
                            DateTime Schedule_Start_Date_Value = Convert.ToDateTime(dataTable.Rows[1][Schedule_Start_Date_Columns_Index]);

                            // Create a new Schedule
                            ScheduleBLO scheduleBLO = new ScheduleBLO(this._UnitOfWork, this.GAppContext);
                            Schedule Schedule = scheduleBLO.Close_And_Start_Schedule(Schedule_Start_Date_Value);

                            // Set the new Schedule in DataTable
                            for (int i = 1; i < dataTable.Rows.Count; i++)
                            {
                                dataTable.Rows[i][Schedule_Column_Name] = Schedule.Reference;
                            }
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                throw new ImportException(exception.Message);
            }
 
            return base.Import(dataTable, FileName);
        }

        public List<SeancePlanning> GetSeancesPlanning(DateTime date_now, SeanceNumber seanceNumber)
        {
            SeanceDayBLO seanceDayBLO = new SeanceDayBLO(this._UnitOfWork, this.GAppContext);
            SeanceDay SeanceDay = seanceDayBLO.GetSeanceDay(date_now);

            if(SeanceDay != null)
            {
                List<SeancePlanning> query = this._UnitOfWork.context.SeancePlannings
                                .Where(s => s.SeanceDay.Reference == SeanceDay.Reference && s.SeanceNumber.Reference == seanceNumber.Reference)
                                .Select(s => s).ToList();

                return query;
            }

            return new List<SeancePlanning>() ;
            
        }

        public List<SeancePlanning> GetSeancesPlanning(DateTime seanceDate, Former former)
        {
            SeanceDayBLO seanceDayBLO = new SeanceDayBLO(this._UnitOfWork, this.GAppContext);
            SeanceDay SeanceDay = seanceDayBLO.GetSeanceDay(seanceDate);

            if (SeanceDay != null)
            {
                List<SeancePlanning> query = this._UnitOfWork.context.SeancePlannings
                                .Where(s => s.SeanceDay.Reference == SeanceDay.Reference 
                                && s.Training.Former.Id == former.Id)
                                .Select(s => s).ToList();

                return query;
            }

            return new List<SeancePlanning>();

        }
    }
}
