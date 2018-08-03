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
	public partial class BaseDefault_SpecialtyDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_SpecialtyDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Specialty ConverTo_Specialty(Default_SpecialtyDetailsView Default_SpecialtyDetailsView)
        {
			Specialty Specialty = null;
            if (Default_SpecialtyDetailsView.Id != 0)
            {
                Specialty = new SpecialtyBLO(this.UnitOfWork).FindBaseEntityByID(Default_SpecialtyDetailsView.Id);
            }
            else
            {
                Specialty = new Specialty();
            } 
			Specialty.Code = Default_SpecialtyDetailsView.Code;
			Specialty.Name = Default_SpecialtyDetailsView.Name;
			Specialty.Description = Default_SpecialtyDetailsView.Description;
			Specialty.Id = Default_SpecialtyDetailsView.Id;
            return Specialty;
        }
        public virtual Default_SpecialtyDetailsView ConverTo_Default_SpecialtyDetailsView(Specialty Specialty)
        {  
            Default_SpecialtyDetailsView Default_SpecialtyDetailsView = new Default_SpecialtyDetailsView();
			Default_SpecialtyDetailsView.Code = Specialty.Code;
			Default_SpecialtyDetailsView.Name = Specialty.Name;
			Default_SpecialtyDetailsView.Description = Specialty.Description;
			Default_SpecialtyDetailsView.Id = Specialty.Id;
            return Default_SpecialtyDetailsView;            
        }
    }

	public partial class Default_SpecialtyDetailsViewBLM : BaseDefault_SpecialtyDetailsViewBLM
	{
		public Default_SpecialtyDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
