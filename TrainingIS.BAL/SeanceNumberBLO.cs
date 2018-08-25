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

            foreach (SeanceNumber seanceNumber in this.FindAll())
            {
                TimeSpan start_time = new TimeSpan(seanceNumber.StartTime.Hour, seanceNumber.StartTime.Minute, seanceNumber.StartTime.Second);
                TimeSpan end_time = new TimeSpan(seanceNumber.EndTime.Hour, seanceNumber.EndTime.Minute, seanceNumber.EndTime.Second);

                if (start_time.TotalSeconds < TimeNow.TotalSeconds && end_time.TotalSeconds > TimeNow.TotalSeconds)
                    return seanceNumber;
            }
            return null;
        }
    }
}
