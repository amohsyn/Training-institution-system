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
	public partial class BaseDefault_StateOfAbseceDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_StateOfAbseceDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual StateOfAbsece ConverTo_StateOfAbsece(Default_StateOfAbseceDetailsView Default_StateOfAbseceDetailsView)
        {
			StateOfAbsece StateOfAbsece = null;
            if (Default_StateOfAbseceDetailsView.Id != 0)
            {
                StateOfAbsece = new StateOfAbseceBLO(this.UnitOfWork).FindBaseEntityByID(Default_StateOfAbseceDetailsView.Id);
            }
            else
            {
                StateOfAbsece = new StateOfAbsece();
            }
			StateOfAbsece.Name = Default_StateOfAbseceDetailsView.Name;
			StateOfAbsece.Category = Default_StateOfAbseceDetailsView.Category;
			StateOfAbsece.Value = Default_StateOfAbseceDetailsView.Value;
			StateOfAbsece.Trainee = Default_StateOfAbseceDetailsView.Trainee;
			StateOfAbsece.Id = Default_StateOfAbseceDetailsView.Id;
            return StateOfAbsece;
        }
        public virtual Default_StateOfAbseceDetailsView ConverTo_Default_StateOfAbseceDetailsView(StateOfAbsece StateOfAbsece)
        {  
            Default_StateOfAbseceDetailsView Default_StateOfAbseceDetailsView = new Default_StateOfAbseceDetailsView();
			Default_StateOfAbseceDetailsView.Name = StateOfAbsece.Name;
			Default_StateOfAbseceDetailsView.Category = StateOfAbsece.Category;
			Default_StateOfAbseceDetailsView.Value = StateOfAbsece.Value;
			Default_StateOfAbseceDetailsView.Trainee = StateOfAbsece.Trainee;
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
