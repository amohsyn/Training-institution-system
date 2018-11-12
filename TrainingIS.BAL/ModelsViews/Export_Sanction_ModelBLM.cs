using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Export_Sanction_ModelBLM
    {
        public override Export_Sanction_Model ConverTo_Export_Sanction_Model(Sanction Sanction)
        {
            var export_sanction = base.ConverTo_Export_Sanction_Model(Sanction);
            export_sanction.FirstName = export_sanction.Trainee.FirstName;
            export_sanction.LastName = export_sanction.Trainee.LastName;
           

            return export_sanction;
        }
    }

}
