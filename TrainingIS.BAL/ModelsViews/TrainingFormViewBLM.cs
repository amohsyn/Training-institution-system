using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.Trainings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class TrainingFormViewBLM
    {
        public override TrainingFormView CreateNew()
        {
            var TrainingFormView = base.CreateNew();
            long? TrainingYearId = new TrainingYearBLO(new DAL.UnitOfWork()).getCurrentTrainingYear()?.Id;
            if (TrainingYearId != null)
                TrainingFormView.TrainingYearId = (long) TrainingYearId;
            return TrainingFormView;

        }
    }
}
