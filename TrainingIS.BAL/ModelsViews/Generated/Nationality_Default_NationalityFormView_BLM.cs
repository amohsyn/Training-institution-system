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
	public partial class BaseDefault_NationalityFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_NationalityFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Nationality ConverTo_Nationality(Default_NationalityFormView Default_NationalityFormView)
        {
			Nationality Nationality = null;
            if (Default_NationalityFormView.Id != 0)
            {
                Nationality = new NationalityBLO(this.UnitOfWork).FindBaseEntityByID(Default_NationalityFormView.Id);
            }
            else
            {
                Nationality = new Nationality();
            } 
			Nationality.Code = Default_NationalityFormView.Code;
			Nationality.Name = Default_NationalityFormView.Name;
			Nationality.Description = Default_NationalityFormView.Description;
			Nationality.Id = Default_NationalityFormView.Id;
            return Nationality;
        }
        public virtual Default_NationalityFormView ConverTo_Default_NationalityFormView(Nationality Nationality)
        {  
			Default_NationalityFormView Default_NationalityFormView = new Default_NationalityFormView();
			Default_NationalityFormView.toStringValue = Nationality.ToString();
			Default_NationalityFormView.Code = Nationality.Code;
			Default_NationalityFormView.Name = Nationality.Name;
			Default_NationalityFormView.Description = Nationality.Description;
			Default_NationalityFormView.Id = Nationality.Id;
            return Default_NationalityFormView;            
        }
    }

	public partial class Default_NationalityFormViewBLM : BaseDefault_NationalityFormViewBLM
	{
		public Default_NationalityFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
