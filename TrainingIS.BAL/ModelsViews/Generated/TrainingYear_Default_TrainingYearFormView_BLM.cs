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
	public partial class BaseDefault_TrainingYearFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingYearFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual TrainingYear ConverTo_TrainingYear(Default_TrainingYearFormView Default_TrainingYearFormView)
        {
			TrainingYear TrainingYear = null;
            if (Default_TrainingYearFormView.Id != 0)
            {
                TrainingYear = new TrainingYearBLO(this.UnitOfWork).FindBaseEntityByID(Default_TrainingYearFormView.Id);
            }
            else
            {
                TrainingYear = new TrainingYear();
            } 
			TrainingYear.Code = Default_TrainingYearFormView.Code;
			TrainingYear.StartDate = Default_TrainingYearFormView.StartDate;
			TrainingYear.EndtDate = Default_TrainingYearFormView.EndtDate;
			TrainingYear.Id = Default_TrainingYearFormView.Id;
            return TrainingYear;
        }
        public virtual Default_TrainingYearFormView ConverTo_Default_TrainingYearFormView(TrainingYear TrainingYear)
        {  
            Default_TrainingYearFormView Default_TrainingYearFormView = new Default_TrainingYearFormView();
			Default_TrainingYearFormView.Code = TrainingYear.Code;
			Default_TrainingYearFormView.StartDate = ConversionUtil.DefaultValue_if_Null<DateTime>(TrainingYear.StartDate);
			Default_TrainingYearFormView.EndtDate = ConversionUtil.DefaultValue_if_Null<DateTime>(TrainingYear.EndtDate);
			Default_TrainingYearFormView.Id = TrainingYear.Id;
            return Default_TrainingYearFormView;            
        }
    }

	public partial class Default_TrainingYearFormViewBLM : BaseDefault_TrainingYearFormViewBLM
	{
		public Default_TrainingYearFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
