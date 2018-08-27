using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Details_Absence_ModelBLM
    {
        public override Details_Absence_Model ConverTo_Details_Absence_Model(Absence Absence)
        {
            Details_Absence_Model details_Absence_Model = base.ConverTo_Details_Absence_Model(Absence);
            details_Absence_Model.StateOfAbsences = Absence.Trainee.StateOfAbseces;
            return details_Absence_Model;
        }
    }
}
