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
	public partial class BaseDefault_NationalityDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_NationalityDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Nationality ConverTo_Nationality(Default_NationalityDetailsView Default_NationalityDetailsView)
        {
			Nationality Nationality = new Nationality();
			Nationality.Code = Default_NationalityDetailsView.Code;
			Nationality.Name = Default_NationalityDetailsView.Name;
			Nationality.Description = Default_NationalityDetailsView.Description;
			Nationality.Id = Default_NationalityDetailsView.Id;
            return Nationality;

        }
        public virtual Default_NationalityDetailsView ConverTo_Default_NationalityDetailsView(Nationality Nationality)
        {
            Default_NationalityDetailsView Default_NationalityDetailsView = new Default_NationalityDetailsView();
			Default_NationalityDetailsView.Code = Nationality.Code;
			Default_NationalityDetailsView.Name = Nationality.Name;
			Default_NationalityDetailsView.Description = Nationality.Description;
			Default_NationalityDetailsView.Id = Nationality.Id;
            return Default_NationalityDetailsView;            
        }
    }

	public partial class Default_NationalityDetailsViewBLM : BaseDefault_NationalityDetailsViewBLM
	{
		public Default_NationalityDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
