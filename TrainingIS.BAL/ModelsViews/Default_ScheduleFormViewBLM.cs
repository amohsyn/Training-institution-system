using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Default_ScheduleFormViewBLM
    {
        public override Default_ScheduleFormView CreateNew()
        {
            var Default_ScheduleFormView = base.CreateNew();
            long? TrainingYearId = new TrainingYearBLO(new DAL.UnitOfWork()).getCurrentTrainingYear()?.Id;
            if (TrainingYearId != null)
                Default_ScheduleFormView.TrainingYearId = (long)TrainingYearId;
            return Default_ScheduleFormView;
        }
    }
}
