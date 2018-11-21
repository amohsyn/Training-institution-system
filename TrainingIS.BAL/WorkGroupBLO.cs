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


        /// <summary>
        /// Find the first WorkGroup by Mission_Workgin_Group_Id
        /// </summary>
        /// <param name="Mission_Workgin_Group_Id"></param>
        /// <returns></returns>
        public WorkGroup Find_By_Mission_Workgin_Group(long Mission_Workgin_Group_Id)
        {

            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);

            var Query = this._UnitOfWork.context.WorkGroups
                .Where(w => w.Mission_Working_Groups.Select(m => m.Id).ToList().Contains(Mission_Workgin_Group_Id))
                .FirstOrDefault();

            return Query;
        }
    }
}
