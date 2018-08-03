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
	public partial class BaseDefault_SpecialtyFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SpecialtyFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Specialty ConverTo_Specialty(Default_SpecialtyFormView Default_SpecialtyFormView)
        {
			Specialty Specialty = null;
            if (Default_SpecialtyFormView.Id != 0)
            {
                Specialty = new SpecialtyBLO(this.UnitOfWork).FindBaseEntityByID(Default_SpecialtyFormView.Id);
            }
            else
            {
                Specialty = new Specialty();
            } 
			Specialty.Code = Default_SpecialtyFormView.Code;
			Specialty.Name = Default_SpecialtyFormView.Name;
			Specialty.Description = Default_SpecialtyFormView.Description;
			Specialty.Id = Default_SpecialtyFormView.Id;
            return Specialty;
        }
        public virtual Default_SpecialtyFormView ConverTo_Default_SpecialtyFormView(Specialty Specialty)
        {  
            Default_SpecialtyFormView Default_SpecialtyFormView = new Default_SpecialtyFormView();
			Default_SpecialtyFormView.Code = Specialty.Code;
			Default_SpecialtyFormView.Name = Specialty.Name;
			Default_SpecialtyFormView.Description = Specialty.Description;
			Default_SpecialtyFormView.Id = Specialty.Id;
            return Default_SpecialtyFormView;            
        }
    }

	public partial class Default_SpecialtyFormViewBLM : BaseDefault_SpecialtyFormViewBLM
	{
		public Default_SpecialtyFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
