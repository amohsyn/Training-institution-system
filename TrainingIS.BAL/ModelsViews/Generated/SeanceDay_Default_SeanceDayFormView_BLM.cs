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
	public partial class BaseDefault_SeanceDayFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceDayFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceDay ConverTo_SeanceDay(Default_SeanceDayFormView Default_SeanceDayFormView)
        {
			SeanceDay SeanceDay = null;
            if (Default_SeanceDayFormView.Id != 0)
            {
                SeanceDay = new SeanceDayBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeanceDayFormView.Id);
            }
            else
            {
                SeanceDay = new SeanceDay();
            }
			SeanceDay.Name = Default_SeanceDayFormView.Name;
			SeanceDay.Code = Default_SeanceDayFormView.Code;
			SeanceDay.Description = Default_SeanceDayFormView.Description;
			SeanceDay.Id = Default_SeanceDayFormView.Id;
            return SeanceDay;
        }
        public virtual Default_SeanceDayFormView ConverTo_Default_SeanceDayFormView(SeanceDay SeanceDay)
        {  
            Default_SeanceDayFormView Default_SeanceDayFormView = new Default_SeanceDayFormView();
			Default_SeanceDayFormView.Name = SeanceDay.Name;
			Default_SeanceDayFormView.Code = SeanceDay.Code;
			Default_SeanceDayFormView.Description = SeanceDay.Description;
			Default_SeanceDayFormView.Id = SeanceDay.Id;
            return Default_SeanceDayFormView;            
        }
    }

	public partial class Default_SeanceDayFormViewBLM : BaseDefault_SeanceDayFormViewBLM
	{
		public Default_SeanceDayFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
