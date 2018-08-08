using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class FormerFormViewBLM
    {
        public override FormerFormView CreateNew()
        {

            FormerFormView formerFormView = base.CreateNew();
            formerFormView.CreateUserAccount = true;
            return formerFormView;
        } 
    }
}
