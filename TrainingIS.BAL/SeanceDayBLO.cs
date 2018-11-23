using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class SeanceDayBLO
    {
        public override List<SeanceDay> FindAll()
        {
            return base.FindAll().OrderBy(entity => entity.Ordre).ToList();
        }

        public SeanceDay GetSeanceDay(DateTime date_now)
        {
            int day = (int)date_now.DayOfWeek;
            SeanceDay SeanceDay = this._UnitOfWork.context.SeanceDays
                .Where(s => s.Day == day ).FirstOrDefault();

            if(SeanceDay == null)
            {
                //[Localization]
                string msg_ex = string.Format("Le jour {0} n'est pas un jour de formation valide", DateTimeFormatInfo.CurrentInfo.GetDayName(date_now.DayOfWeek));
                throw new BLL_Exception(msg_ex);
            }
            return SeanceDay;

        }

    }
}
