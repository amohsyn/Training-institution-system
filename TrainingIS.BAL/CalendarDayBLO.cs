using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class CalendarDayBLO
    {
 
        public void Fill_CalendarDay(DateTime startDate, DateTime endDate)
        {

 
            foreach (var date in EachDate(startDate, endDate))
            {


                CalendarDay calendarDay = this.FindByDate(date.Date);
                if (calendarDay != null) continue;

                calendarDay = new CalendarDay();
                calendarDay.Reference = date.ToString();

                calendarDay.Date = date;

                // Day
                calendarDay.DateName = date.ToString("dddd");
                calendarDay.DayOfWeek = (int) date.DayOfWeek;

                var dayOfYear = date.DayOfYear;
                calendarDay.DateNameAbbrev = date.ToString("ddd");
                calendarDay.CalendarYearBegin = new DateTime(date.Year, 1, 1);
                calendarDay.CalendarYearEnd = new DateTime(date.Year, 12, 31);
                calendarDay.CalendarYear = date.Year;
                calendarDay.CalendarMonthBegin = new DateTime(date.Year, date.Month, 1);
                calendarDay.CalendarMonthEnd = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
                calendarDay.CalendarMonthNumber = date.Month;
                calendarDay.CalendarMonthName = date.ToString("MMMM");
                calendarDay.CalendarMonthNameAbbrev = date.ToString("MMM");

                calendarDay.FiscalYear = FiscalYear(date);
                calendarDay.FiscalYearBegin = FiscalYearBegin(date);
                calendarDay.FiscalYearEnd = FiscalYearEnd(date);

                calendarDay.WeekBeginDate = StartOfWeek(date);
                calendarDay.WeekEndDate = EndOfWeek(date);

                calendarDay.WeekNumber = WeekNumber(date);

                this.Save(calendarDay);
             

                 
            }


        }

        private CalendarDay FindByDate(DateTime date)
        {
            return this._UnitOfWork.context.CalendarDaies.Where(c => c.Date == date).FirstOrDefault();
        }

        private static IEnumerable<DateTime> EachDate(DateTime start, DateTime end)
        {
            for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        private static int FiscalYear(DateTime actualDate)
        {
            /*
            if (actualDate.Month >= 4)
            {
                return actualDate.Year + 1;
            }*/

            return actualDate.Year;
        }

        private static DateTime FiscalYearBegin(DateTime actualDate)
        {
            /*
            if (actualDate.Month >= 4)
            {
                return new DateTime(actualDate.Year, 4, 1);
            }
            */
            return new DateTime(actualDate.Year, 1, 1);
        }

        private static DateTime FiscalYearEnd(DateTime actualDate)
        {
            /*if (actualDate.Month >= 4)
            {
                return new DateTime(actualDate.Year + 1, 3, 31);
            }*/

            return new DateTime(actualDate.Year, 12, 31);
        }

        private static DateTime StartOfWeek(DateTime actualDate)
        {
            int diff = actualDate.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }

            return actualDate.AddDays(-1 * diff).Date;
        }

        private static DateTime EndOfWeek(DateTime actualDate)
        {
            return actualDate.DayOfWeek == DayOfWeek.Sunday ? actualDate : actualDate.AddDays(7 - (int)actualDate.DayOfWeek);
        }

        private static int WeekNumber(DateTime actualDate)
        {
            DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.CurrentInfo;
            if (dateTimeFormat == null)
            {
                return 0;
            }
            Calendar cal = dateTimeFormat.Calendar;
            return cal.GetWeekOfYear(actualDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

      
    }
}
