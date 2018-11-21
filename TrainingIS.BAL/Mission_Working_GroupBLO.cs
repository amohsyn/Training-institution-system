using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class Mission_Working_GroupBLO
    {
        /// <summary>
        /// Find the first Mission_Working_Group of Sanction
        /// </summary>
        /// <param name="Sanction_Id"></param>
        /// <returns></returns>
        public Mission_Working_Group Find_By_Sanction(long Sanction_Id)
        {
            SanctionBLO sanctionBLO = new SanctionBLO(this._UnitOfWork, this.GAppContext);
            Sanction sanction = sanctionBLO.FindBaseEntityByID(Sanction_Id);
            var mission_Working_Group = this._UnitOfWork.context.Mission_Working_Groups
                .Where(m => m.DecisionAuthority == sanction.SanctionCategory.DecisionAuthority)
                .FirstOrDefault();
            return mission_Working_Group;


        }
    }
}
