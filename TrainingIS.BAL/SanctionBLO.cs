using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class SanctionBLO
    {
        public Sanction Find_By_Meeting_Id(long MeetingId)
        {
            var sanction = this._UnitOfWork.context.Sanctions.Where(s => s.MeetingId == MeetingId).FirstOrDefault();
            return sanction;
        }
    }
}
