//modelType = Default_Details_Specialty_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_Specialty_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_Specialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Specialty ConverTo_Specialty(Default_Details_Specialty_Model Default_Details_Specialty_Model)
        {
			Specialty Specialty = null;
            if (Default_Details_Specialty_Model.Id != 0)
            {
                Specialty = new SpecialtyBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_Specialty_Model.Id);
            }
            else
            {
                Specialty = new Specialty();
            } 
			Specialty.Code = Default_Details_Specialty_Model.Code;
			Specialty.Name = Default_Details_Specialty_Model.Name;
			Specialty.Description = Default_Details_Specialty_Model.Description;
			Specialty.Id = Default_Details_Specialty_Model.Id;
            return Specialty;
        }
        public virtual Default_Details_Specialty_Model ConverTo_Default_Details_Specialty_Model(Specialty Specialty)
        {  
			Default_Details_Specialty_Model Default_Details_Specialty_Model = new Default_Details_Specialty_Model();
			Default_Details_Specialty_Model.toStringValue = Specialty.ToString();
			Default_Details_Specialty_Model.Code = Specialty.Code;
			Default_Details_Specialty_Model.Name = Specialty.Name;
			Default_Details_Specialty_Model.Description = Specialty.Description;
			Default_Details_Specialty_Model.Id = Specialty.Id;
            return Default_Details_Specialty_Model;            
        }

		public virtual Default_Details_Specialty_Model CreateNew()
        {
            Specialty Specialty = new Specialty();
            Default_Details_Specialty_Model Default_Details_Specialty_Model = this.ConverTo_Default_Details_Specialty_Model(Specialty);
            return Default_Details_Specialty_Model;
        } 
    }

	public partial class Default_Details_Specialty_ModelBLM : BaseDefault_Details_Specialty_ModelBLM
	{
		public Default_Details_Specialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
