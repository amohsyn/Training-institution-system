using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_StateOfAbseceFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_StateOfAbseceFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual StateOfAbsece ConverTo_StateOfAbsece(Default_StateOfAbseceFormView Default_StateOfAbseceFormView)
        {
			StateOfAbsece StateOfAbsece = null;
            if (Default_StateOfAbseceFormView.Id != 0)
            {
                StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork).FindBaseEntityByID(Default_StateOfAbseceFormView.Id);
            }
            else
            {
                StateOfAbsece = new StateOfAbsece();
            } 
			StateOfAbsece.Name = Default_StateOfAbseceFormView.Name;
			StateOfAbsece.Category = Default_StateOfAbseceFormView.Category;
			StateOfAbsece.Value = Default_StateOfAbseceFormView.Value;
			StateOfAbsece.TraineeId = Default_StateOfAbseceFormView.TraineeId;
			StateOfAbsece.Id = Default_StateOfAbseceFormView.Id;
            return StateOfAbsece;
        }
        public virtual Default_StateOfAbseceFormView ConverTo_Default_StateOfAbseceFormView(StateOfAbsece StateOfAbsece)
        {  
			Default_StateOfAbseceFormView Default_StateOfAbseceFormView = new Default_StateOfAbseceFormView();
			Default_StateOfAbseceFormView.toStringValue = StateOfAbsece.ToString();
			Default_StateOfAbseceFormView.Name = StateOfAbsece.Name;
			Default_StateOfAbseceFormView.Category = StateOfAbsece.Category;
			Default_StateOfAbseceFormView.Value = StateOfAbsece.Value;
			Default_StateOfAbseceFormView.TraineeId = StateOfAbsece.TraineeId;
			Default_StateOfAbseceFormView.Id = StateOfAbsece.Id;
            return Default_StateOfAbseceFormView;            
        }
    }

	public partial class Default_StateOfAbseceFormViewBLM : BaseDefault_StateOfAbseceFormViewBLM
	{
		public Default_StateOfAbseceFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
