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
	public partial class BaseDefault_TrainingYearDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingYearDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual TrainingYear ConverTo_TrainingYear(Default_TrainingYearDetailsView Default_TrainingYearDetailsView)
        {
			TrainingYear TrainingYear = null;
            if (Default_TrainingYearDetailsView.Id != 0)
            {
                TrainingYear = new TrainingYearBLO(this.UnitOfWork).FindBaseEntityByID(Default_TrainingYearDetailsView.Id);
            }
            else
            {
                TrainingYear = new TrainingYear();
            } 
			TrainingYear.Code = Default_TrainingYearDetailsView.Code;
			TrainingYear.StartDate = Default_TrainingYearDetailsView.StartDate;
			TrainingYear.EndtDate = Default_TrainingYearDetailsView.EndtDate;
			TrainingYear.Id = Default_TrainingYearDetailsView.Id;
            return TrainingYear;
        }
        public virtual Default_TrainingYearDetailsView ConverTo_Default_TrainingYearDetailsView(TrainingYear TrainingYear)
        {  
            Default_TrainingYearDetailsView Default_TrainingYearDetailsView = new Default_TrainingYearDetailsView();
			Default_TrainingYearDetailsView.Code = TrainingYear.Code;
			Default_TrainingYearDetailsView.StartDate = ConversionUtil.DefaultValue_if_Null<DateTime>(TrainingYear.StartDate);
			Default_TrainingYearDetailsView.EndtDate = ConversionUtil.DefaultValue_if_Null<DateTime>(TrainingYear.EndtDate);
			Default_TrainingYearDetailsView.Id = TrainingYear.Id;
            return Default_TrainingYearDetailsView;            
        }
    }

	public partial class Default_TrainingYearDetailsViewBLM : BaseDefault_TrainingYearDetailsViewBLM
	{
		public Default_TrainingYearDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
