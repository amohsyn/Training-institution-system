using GApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class CreateGroupViewBLM
    {
        public override CreateGroupView CreateNew()
        {
            CreateGroupView CreateGroupView = base.CreateNew();
            long? TrainingYearId = new TrainingYearBLO(new UnitOfWork<TrainingISModel>(), this.GAppContext).getCurrentTrainingYear()?.Id;
            if (TrainingYearId != null)
                CreateGroupView.TrainingYearId = (long)TrainingYearId;
            return CreateGroupView;

        }
    }
}
