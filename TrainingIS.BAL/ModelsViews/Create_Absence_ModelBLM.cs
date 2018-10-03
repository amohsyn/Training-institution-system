using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.Absences;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Create_Absence_ModelBLM
    {
        public override Create_Absence_Model ConverTo_Create_Absence_Model(Absence Absence)
        {
  
            var r = base.ConverTo_Create_Absence_Model(Absence);
            r.JustificationAbsence = Absence.JustificationAbsence;
            return r;

        }
    }
}
