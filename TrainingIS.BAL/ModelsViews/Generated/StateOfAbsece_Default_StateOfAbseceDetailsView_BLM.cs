using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_StateOfAbseceDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_StateOfAbseceDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual StateOfAbsece ConverTo_StateOfAbsece(Default_StateOfAbseceDetailsView Default_StateOfAbseceDetailsView)
        {
			StateOfAbsece StateOfAbsece = new StateOfAbsece();
			StateOfAbsece.Name = Default_StateOfAbseceDetailsView.Name;
			StateOfAbsece.Category = Default_StateOfAbseceDetailsView.Category;
			StateOfAbsece.Value = Default_StateOfAbseceDetailsView.Value;
			StateOfAbsece.TraineeId = Default_StateOfAbseceDetailsView.TraineeId;
			StateOfAbsece.Id = Default_StateOfAbseceDetailsView.Id;
            return StateOfAbsece;

        }
        public virtual Default_StateOfAbseceDetailsView ConverTo_Default_StateOfAbseceDetailsView(StateOfAbsece StateOfAbsece)
        {
            Default_StateOfAbseceDetailsView Default_StateOfAbseceDetailsView = new Default_StateOfAbseceDetailsView();
			Default_StateOfAbseceDetailsView.Name = StateOfAbsece.Name;
			Default_StateOfAbseceDetailsView.Category = StateOfAbsece.Category;
			Default_StateOfAbseceDetailsView.Value = StateOfAbsece.Value;
			Default_StateOfAbseceDetailsView.TraineeId = StateOfAbsece.TraineeId;
			Default_StateOfAbseceDetailsView.Id = StateOfAbsece.Id;
            return Default_StateOfAbseceDetailsView;            
        }
    }

	public partial class Default_StateOfAbseceDetailsViewBLM : BaseDefault_StateOfAbseceDetailsViewBLM
	{
		public Default_StateOfAbseceDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
