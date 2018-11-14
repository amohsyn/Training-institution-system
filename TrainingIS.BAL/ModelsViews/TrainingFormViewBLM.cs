using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Models.SeanceTrainings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Create_SeanceTraining_ModelBLM
    {
        public override Create_SeanceTraining_Model CreateNew()
        {
            var TrainingFormView = base.CreateNew();
            return TrainingFormView;
        }

        
    }
}
