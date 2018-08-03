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
	public partial class BaseDefault_TrainingTypeFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingTypeFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual TrainingType ConverTo_TrainingType(Default_TrainingTypeFormView Default_TrainingTypeFormView)
        {
			TrainingType TrainingType = null;
            if (Default_TrainingTypeFormView.Id != 0)
            {
                TrainingType = new TrainingTypeBLO(this.UnitOfWork).FindBaseEntityByID(Default_TrainingTypeFormView.Id);
            }
            else
            {
                TrainingType = new TrainingType();
            } 
			TrainingType.Code = Default_TrainingTypeFormView.Code;
			TrainingType.Name = Default_TrainingTypeFormView.Name;
			TrainingType.Description = Default_TrainingTypeFormView.Description;
			TrainingType.Id = Default_TrainingTypeFormView.Id;
            return TrainingType;
        }
        public virtual Default_TrainingTypeFormView ConverTo_Default_TrainingTypeFormView(TrainingType TrainingType)
        {  
            Default_TrainingTypeFormView Default_TrainingTypeFormView = new Default_TrainingTypeFormView();
			Default_TrainingTypeFormView.Code = TrainingType.Code;
			Default_TrainingTypeFormView.Name = TrainingType.Name;
			Default_TrainingTypeFormView.Description = TrainingType.Description;
			Default_TrainingTypeFormView.Id = TrainingType.Id;
            return Default_TrainingTypeFormView;            
        }
    }

	public partial class Default_TrainingTypeFormViewBLM : BaseDefault_TrainingTypeFormViewBLM
	{
		public Default_TrainingTypeFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
