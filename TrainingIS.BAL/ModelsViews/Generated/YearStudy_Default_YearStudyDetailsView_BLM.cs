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
	public partial class BaseDefault_YearStudyDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_YearStudyDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual YearStudy ConverTo_YearStudy(Default_YearStudyDetailsView Default_YearStudyDetailsView)
        {
			YearStudy YearStudy = null;
            if (Default_YearStudyDetailsView.Id != 0)
            {
                YearStudy = new YearStudyBLO(this.UnitOfWork).FindBaseEntityByID(Default_YearStudyDetailsView.Id);
            }
            else
            {
                YearStudy = new YearStudy();
            } 
			YearStudy.Code = Default_YearStudyDetailsView.Code;
			YearStudy.Name = Default_YearStudyDetailsView.Name;
			YearStudy.Description = Default_YearStudyDetailsView.Description;
			YearStudy.Id = Default_YearStudyDetailsView.Id;
            return YearStudy;
        }
        public virtual Default_YearStudyDetailsView ConverTo_Default_YearStudyDetailsView(YearStudy YearStudy)
        {  
			Default_YearStudyDetailsView Default_YearStudyDetailsView = new Default_YearStudyDetailsView();
			Default_YearStudyDetailsView.toStringValue = YearStudy.ToString();
			Default_YearStudyDetailsView.Code = YearStudy.Code;
			Default_YearStudyDetailsView.Name = YearStudy.Name;
			Default_YearStudyDetailsView.Description = YearStudy.Description;
			Default_YearStudyDetailsView.Id = YearStudy.Id;
            return Default_YearStudyDetailsView;            
        }
    }

	public partial class Default_YearStudyDetailsViewBLM : BaseDefault_YearStudyDetailsViewBLM
	{
		public Default_YearStudyDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
