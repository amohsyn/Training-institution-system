using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class SeanceNumberBLO
    {
        public SeanceNumber GetSeanceNumber(TimeSpan TimeNow)
        {

   
            var TimeHourNow = (TimeNow.Hours < 12) ? TimeNow.Hours : (TimeNow.Hours - 12);


            SeanceNumber seanceNumber = this._UnitOfWork.context.SeanceNumbers
                .Where(s =>
                   DbFunctions.CreateTime(s.StartTime.Hour, s.StartTime.Minute, s.StartTime.Second)
                   < DbFunctions.CreateTime(TimeHourNow, TimeNow.Minutes, TimeNow.Seconds) &&

                   DbFunctions.CreateTime(s.EndTime.Hour, s.EndTime.Minute, s.EndTime.Second)
                   > DbFunctions.CreateTime(TimeHourNow, TimeNow.Minutes, TimeNow.Seconds)
            ).FirstOrDefault();

            return this.FindAll().First() ;
        }
    }
}
