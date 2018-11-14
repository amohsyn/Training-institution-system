using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Default_Schedule_Create_ModelBLM
    {

        public override Default_Schedule_Create_Model CreateNew()
        {
            var Default_ScheduleFormView = base.CreateNew();
            long? TrainingYearId = new TrainingYearBLO(new UnitOfWork<TrainingISModel>(), this.GAppContext).getCurrentTrainingYear()?.Id;
            if (TrainingYearId != null)
                Default_ScheduleFormView.TrainingYearId = (long)TrainingYearId;
            return Default_ScheduleFormView;
        }

       
    }
}
