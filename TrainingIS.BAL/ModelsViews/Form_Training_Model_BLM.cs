using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Form_Training_ModelBLM
    {
        public override void ConverTo_Form_Training_Model(Form_Training_Model Form_Training_Model, Training Training)
        {
            base.ConverTo_Form_Training_Model(Form_Training_Model, Training);
            if (Form_Training_Model.ModuleTrainingId != 0)
            {
                ModuleTrainingBLO moduleTrainingBLO  = new ModuleTrainingBLO(this.UnitOfWork, this.GAppContext);
                Form_Training_Model.SpecialtyId = moduleTrainingBLO.FindBaseEntityByID(Form_Training_Model.ModuleTrainingId).Specialty.Id;
                Form_Training_Model.FormerSpecialtyId = Training.Former.FormerSpecialtyId;
            }
 
        }
    }
}
