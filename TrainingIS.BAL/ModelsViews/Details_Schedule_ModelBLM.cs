using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Details_Schedule_ModelBLM
    {
        public override Details_Schedule_Model ConverTo_Details_Schedule_Model(Schedule Schedule)
        {
            Details_Schedule_Model details_Schedule_Model = base.ConverTo_Details_Schedule_Model(Schedule);
            details_Schedule_Model.SeancePlannings = Schedule.SeancePlannings;
            return details_Schedule_Model;
        }
    }
}
