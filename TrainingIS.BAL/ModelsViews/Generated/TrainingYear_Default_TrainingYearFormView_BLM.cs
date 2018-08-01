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
	public partial class BaseDefault_TrainingYearFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_TrainingYearFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual TrainingYear ConverTo_TrainingYear(Default_TrainingYearFormView Default_TrainingYearFormView)
        {
			TrainingYear TrainingYear = new TrainingYear();
			TrainingYear.Code = Default_TrainingYearFormView.Code;
			TrainingYear.Id = Default_TrainingYearFormView.Id;
            return TrainingYear;

        }
        public virtual Default_TrainingYearFormView ConverTo_Default_TrainingYearFormView(TrainingYear TrainingYear)
        {
            Default_TrainingYearFormView Default_TrainingYearFormView = new Default_TrainingYearFormView();
			Default_TrainingYearFormView.Code = TrainingYear.Code;
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
