using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Default_Form_Sanction_ModelBLM
    {
        public override void ConverTo_Default_Form_Sanction_Model(Default_Form_Sanction_Model Default_Form_Sanction_Model, Sanction Sanction)
        {
            base.ConverTo_Default_Form_Sanction_Model(Default_Form_Sanction_Model, Sanction);
            Default_Form_Sanction_Model.MeetingId = Sanction.Meeting.Id;
        }
      
    }
}
