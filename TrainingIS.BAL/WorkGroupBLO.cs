using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL.Exceptions;
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

        public WorkGroup Get_By_Mission_Workgin_Group(long Mission_Workgin_Group_Id)
        {
            // BLO
            Mission_Working_GroupBLO mission_Working_GroupBLO = new Mission_Working_GroupBLO(this._UnitOfWork,this.GAppContext );
            Mission_Working_Group mission_Working_Group = mission_Working_GroupBLO.FindBaseEntityByID(Mission_Workgin_Group_Id);
            var WorkGroup = this.Find_By_Mission_Workgin_Group(Mission_Workgin_Group_Id);

            if(WorkGroup == null)
            {
                //[Localization]
                string msg_ex = string.Format("Le conseil ou comité qui traite la mission : {0},n'exist pas dans la base de données. Veuillez ajouter un conseil avec cette mission. ", mission_Working_Group.ToString());
                throw new BLL_Exception(msg_ex);
            }

            return WorkGroup;
        }
    }
}
