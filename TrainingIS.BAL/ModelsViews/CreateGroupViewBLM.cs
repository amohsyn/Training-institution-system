using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class CreateGroupViewBLM
    {
        public override CreateGroupView CreateNew()
        {
            CreateGroupView CreateGroupView = base.CreateNew();
            long? TrainingYearId = new TrainingYearBLO(new DAL.UnitOfWork()).getCurrentTrainingYear()?.Id;
            if (TrainingYearId != null)
                CreateGroupView.TrainingYearId = (long)TrainingYearId;
            return CreateGroupView;

        }
    }
}
