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
	public partial class BaseDefault_SeanceDayDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SeanceDayDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual SeanceDay ConverTo_SeanceDay(Default_SeanceDayDetailsView Default_SeanceDayDetailsView)
        {
			SeanceDay SeanceDay = null;
            if (Default_SeanceDayDetailsView.Id != 0)
            {
                SeanceDay = new SeanceDayBLO(this.UnitOfWork).FindBaseEntityByID(Default_SeanceDayDetailsView.Id);
            }
            else
            {
                SeanceDay = new SeanceDay();
            } 
			SeanceDay.Name = Default_SeanceDayDetailsView.Name;
			SeanceDay.Code = Default_SeanceDayDetailsView.Code;
			SeanceDay.Description = Default_SeanceDayDetailsView.Description;
			SeanceDay.Id = Default_SeanceDayDetailsView.Id;
            return SeanceDay;
        }
        public virtual Default_SeanceDayDetailsView ConverTo_Default_SeanceDayDetailsView(SeanceDay SeanceDay)
        {  
			Default_SeanceDayDetailsView Default_SeanceDayDetailsView = new Default_SeanceDayDetailsView();
			Default_SeanceDayDetailsView.toStringValue = SeanceDay.ToString();
			Default_SeanceDayDetailsView.Name = SeanceDay.Name;
			Default_SeanceDayDetailsView.Code = SeanceDay.Code;
			Default_SeanceDayDetailsView.Description = SeanceDay.Description;
			Default_SeanceDayDetailsView.Id = SeanceDay.Id;
            return Default_SeanceDayDetailsView;            
        }

		public virtual Default_SeanceDayDetailsView CreateNew()
        {
            SeanceDay SeanceDay = new SeanceDay();
            Default_SeanceDayDetailsView Default_SeanceDayDetailsView = this.ConverTo_Default_SeanceDayDetailsView(SeanceDay);
            return Default_SeanceDayDetailsView;
        } 
    }

	public partial class Default_SeanceDayDetailsViewBLM : BaseDefault_SeanceDayDetailsViewBLM
	{
		public Default_SeanceDayDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
