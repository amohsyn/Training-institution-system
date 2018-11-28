using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.Meetings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Index_Meeting_ModelBLM
    {
        public override Index_Meeting_Model ConverTo_Index_Meeting_Model(Meeting Meeting)
        {
            // BLO
            MeetingBLO meetingBLO = new MeetingBLO(this.UnitOfWork, this.GAppContext);

            Index_Meeting_Model index_Meeting_Model = base.ConverTo_Index_Meeting_Model(Meeting);
            index_Meeting_Model.Presences = meetingBLO.Get_Presences(Meeting);
            index_Meeting_Model.Decision = meetingBLO.Get_Decision_Info(Meeting);

            return index_Meeting_Model;
        }
    }
}
