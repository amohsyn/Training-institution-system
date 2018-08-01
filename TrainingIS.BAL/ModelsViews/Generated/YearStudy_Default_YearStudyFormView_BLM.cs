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
	public partial class BaseDefault_YearStudyFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_YearStudyFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual YearStudy ConverTo_YearStudy(Default_YearStudyFormView Default_YearStudyFormView)
        {
			YearStudy YearStudy = new YearStudy();
			YearStudy.Code = Default_YearStudyFormView.Code;
			YearStudy.Name = Default_YearStudyFormView.Name;
			YearStudy.Description = Default_YearStudyFormView.Description;
			YearStudy.Id = Default_YearStudyFormView.Id;
            return YearStudy;

        }
        public virtual Default_YearStudyFormView ConverTo_Default_YearStudyFormView(YearStudy YearStudy)
        {
            Default_YearStudyFormView Default_YearStudyFormView = new Default_YearStudyFormView();
			Default_YearStudyFormView.Code = YearStudy.Code;
			Default_YearStudyFormView.Name = YearStudy.Name;
			Default_YearStudyFormView.Description = YearStudy.Description;
			Default_YearStudyFormView.Id = YearStudy.Id;
            return Default_YearStudyFormView;            
        }
    }

	public partial class Default_YearStudyFormViewBLM : BaseDefault_YearStudyFormViewBLM
	{
		public Default_YearStudyFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
