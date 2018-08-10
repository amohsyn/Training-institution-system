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
	public partial class BaseDefault_SeanceNumberFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceNumberFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceNumber ConverTo_SeanceNumber(Default_SeanceNumberFormView Default_SeanceNumberFormView)
        {
			SeanceNumber SeanceNumber = null;
            if (Default_SeanceNumberFormView.Id != 0)
            {
                SeanceNumber = new SeanceNumberBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeanceNumberFormView.Id);
            }
            else
            {
                SeanceNumber = new SeanceNumber();
            } 
			SeanceNumber.Code = Default_SeanceNumberFormView.Code;
			SeanceNumber.StartTime = DefaultDateTime_If_Empty(Default_SeanceNumberFormView.StartTime);
			SeanceNumber.EndTime = DefaultDateTime_If_Empty(Default_SeanceNumberFormView.EndTime);
			SeanceNumber.Description = Default_SeanceNumberFormView.Description;
			SeanceNumber.Id = Default_SeanceNumberFormView.Id;
            return SeanceNumber;
        }
        public virtual Default_SeanceNumberFormView ConverTo_Default_SeanceNumberFormView(SeanceNumber SeanceNumber)
        {  
			Default_SeanceNumberFormView Default_SeanceNumberFormView = new Default_SeanceNumberFormView();
			Default_SeanceNumberFormView.toStringValue = SeanceNumber.ToString();
			Default_SeanceNumberFormView.Code = SeanceNumber.Code;
			Default_SeanceNumberFormView.StartTime = DefaultDateTime_If_Empty(SeanceNumber.StartTime);
			Default_SeanceNumberFormView.EndTime = DefaultDateTime_If_Empty(SeanceNumber.EndTime);
			Default_SeanceNumberFormView.Description = SeanceNumber.Description;
			Default_SeanceNumberFormView.Id = SeanceNumber.Id;
            return Default_SeanceNumberFormView;            
        }

		public virtual Default_SeanceNumberFormView CreateNew()
        {
            SeanceNumber SeanceNumber = new SeanceNumber();
            Default_SeanceNumberFormView Default_SeanceNumberFormView = this.ConverTo_Default_SeanceNumberFormView(SeanceNumber);
            return Default_SeanceNumberFormView;
        } 
    }

	public partial class Default_SeanceNumberFormViewBLM : BaseDefault_SeanceNumberFormViewBLM
	{
		public Default_SeanceNumberFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
