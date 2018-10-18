using GApp.Entities;
using GApp.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS.Entities
{
    [EntityMetataData(isMale = true)]
    public class CalendarDay : BaseEntity
    {
        public override string ToString()
        {
            string msg = string.Format("{0}-{1}", this.Date.ToShortDateString(), this.DateName);
            return msg;
            
        }

        public DateTime Date { set; get; }
        public string DateName { set; get; }
        public string DateNameAbbrev { set; get; }
        public int DayOfWeek { set; get; }

        public bool IsWeekend { set; get; }
        public int WeekNumber { set; get; }
        public DateTime WeekBeginDate { set; get; }
        public DateTime WeekEndDate { set; get; }


        public string CalendarMonthName { set; get; }
        public string CalendarMonthNameAbbrev { set; get; }
        public DateTime CalendarMonthBegin { set; get; }
        public DateTime CalendarMonthEnd { set; get; }
        public int CalendarMonthNumber { set; get; }

        public int CalendarYear { set; get; }
        public int FiscalYear { set; get; }
        public int DayOfYear { set; get; }
        public DateTime CalendarYearBegin { set; get; }
        public DateTime CalendarYearEnd { set; get; }
        public DateTime FiscalYearBegin { set; get; }
        public DateTime FiscalYearEnd { set; get; }
    }
}
