using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.FormerModelsViews;
using TrainingIS.Models.FormerModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Create_Former_ModelBLM
    {

        public override Create_Former_Model CreateNew()
        {
            Create_Former_Model formerFormView = base.CreateNew();
            formerFormView.CreateUserAccount = true;
            return formerFormView;
        }

       
    }
}
