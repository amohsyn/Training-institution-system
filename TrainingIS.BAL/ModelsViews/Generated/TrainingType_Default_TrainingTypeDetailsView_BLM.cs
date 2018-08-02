using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_TrainingTypeDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingTypeDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual TrainingType ConverTo_TrainingType(Default_TrainingTypeDetailsView Default_TrainingTypeDetailsView)
        {
			TrainingType TrainingType = null;
            if (Default_TrainingTypeDetailsView.Id != 0)
            {
                TrainingType = new TrainingTypeBLO(this.UnitOfWork).FindBaseEntityByID(Default_TrainingTypeDetailsView.Id);
            }
            else
            {
                TrainingType = new TrainingType();
            }
			TrainingType.Code = Default_TrainingTypeDetailsView.Code;
			TrainingType.Name = Default_TrainingTypeDetailsView.Name;
			TrainingType.Description = Default_TrainingTypeDetailsView.Description;
			TrainingType.Id = Default_TrainingTypeDetailsView.Id;
            return TrainingType;
        }
        public virtual Default_TrainingTypeDetailsView ConverTo_Default_TrainingTypeDetailsView(TrainingType TrainingType)
        {  
            Default_TrainingTypeDetailsView Default_TrainingTypeDetailsView = new Default_TrainingTypeDetailsView();
			Default_TrainingTypeDetailsView.Code = TrainingType.Code;
			Default_TrainingTypeDetailsView.Name = TrainingType.Name;
			Default_TrainingTypeDetailsView.Description = TrainingType.Description;
			Default_TrainingTypeDetailsView.Id = TrainingType.Id;
            return Default_TrainingTypeDetailsView;            
        }
    }

	public partial class Default_TrainingTypeDetailsViewBLM : BaseDefault_TrainingTypeDetailsViewBLM
	{
		public Default_TrainingTypeDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
