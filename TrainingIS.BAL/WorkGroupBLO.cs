using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class WorkGroupBLO
    {
        public WorkGroup FindByCode(string workGroup_Code)
        {
            return this._UnitOfWork.context
                .WorkGroups
                .Where(w => w.Code == workGroup_Code)
                .FirstOrDefault();
        }
    }
}
