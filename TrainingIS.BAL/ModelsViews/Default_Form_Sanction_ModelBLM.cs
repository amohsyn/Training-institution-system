using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Form_Sanction_ModelBLM
    {
        public override void ConverTo_Form_Sanction_Model(Form_Sanction_Model Form_Sanction_Model, Sanction Sanction)
        {
            base.ConverTo_Form_Sanction_Model(Form_Sanction_Model, Sanction);
            Form_Sanction_Model.MeetingId = Sanction.Meeting.Id;
        }
      
    }
}
