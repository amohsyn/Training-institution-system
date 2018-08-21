using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // [Bug] must add Day in SeanceDay
            SeanceDay query = this._UnitOfWork.context.SeanceDays
                .Where(s => s.Ordre == date_now.Day).FirstOrDefault();
            return this.FindAll().First();

        }

    }
}
