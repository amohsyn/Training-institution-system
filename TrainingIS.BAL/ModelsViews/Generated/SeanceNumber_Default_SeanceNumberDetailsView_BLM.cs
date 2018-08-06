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
	public partial class BaseDefault_SeanceNumberDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceNumberDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceNumber ConverTo_SeanceNumber(Default_SeanceNumberDetailsView Default_SeanceNumberDetailsView)
        {
			SeanceNumber SeanceNumber = null;
            if (Default_SeanceNumberDetailsView.Id != 0)
            {
                SeanceNumber = new SeanceNumberBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeanceNumberDetailsView.Id);
            }
            else
            {
                SeanceNumber = new SeanceNumber();
            } 
			SeanceNumber.Code = Default_SeanceNumberDetailsView.Code;
			SeanceNumber.StartTime = Default_SeanceNumberDetailsView.StartTime;
			SeanceNumber.EndTime = Default_SeanceNumberDetailsView.EndTime;
			SeanceNumber.Description = Default_SeanceNumberDetailsView.Description;
			SeanceNumber.Id = Default_SeanceNumberDetailsView.Id;
            return SeanceNumber;
        }
        public virtual Default_SeanceNumberDetailsView ConverTo_Default_SeanceNumberDetailsView(SeanceNumber SeanceNumber)
        {  
			Default_SeanceNumberDetailsView Default_SeanceNumberDetailsView = new Default_SeanceNumberDetailsView();
			Default_SeanceNumberDetailsView.toStringValue = SeanceNumber.ToString();
			Default_SeanceNumberDetailsView.Code = SeanceNumber.Code;
			Default_SeanceNumberDetailsView.StartTime = SeanceNumber.StartTime;
			Default_SeanceNumberDetailsView.EndTime = SeanceNumber.EndTime;
			Default_SeanceNumberDetailsView.Description = SeanceNumber.Description;
			Default_SeanceNumberDetailsView.Id = SeanceNumber.Id;
            return Default_SeanceNumberDetailsView;            
        }
    }

	public partial class Default_SeanceNumberDetailsViewBLM : BaseDefault_SeanceNumberDetailsViewBLM
	{
		public Default_SeanceNumberDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
