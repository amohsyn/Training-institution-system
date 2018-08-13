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
	public partial class BaseDefault_Details_Nationality_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_Nationality_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual Nationality ConverTo_Nationality(Default_Details_Nationality_Model Default_Details_Nationality_Model)
        {
			Nationality Nationality = null;
            if (Default_Details_Nationality_Model.Id != 0)
            {
                Nationality = new NationalityBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_Nationality_Model.Id);
            }
            else
            {
                Nationality = new Nationality();
            } 
			Nationality.Code = Default_Details_Nationality_Model.Code;
			Nationality.Name = Default_Details_Nationality_Model.Name;
			Nationality.Description = Default_Details_Nationality_Model.Description;
			Nationality.Id = Default_Details_Nationality_Model.Id;
            return Nationality;
        }
        public virtual Default_Details_Nationality_Model ConverTo_Default_Details_Nationality_Model(Nationality Nationality)
        {  
			Default_Details_Nationality_Model Default_Details_Nationality_Model = new Default_Details_Nationality_Model();
			Default_Details_Nationality_Model.toStringValue = Nationality.ToString();
			Default_Details_Nationality_Model.Code = Nationality.Code;
			Default_Details_Nationality_Model.Name = Nationality.Name;
			Default_Details_Nationality_Model.Description = Nationality.Description;
			Default_Details_Nationality_Model.Id = Nationality.Id;
            return Default_Details_Nationality_Model;            
        }

		public virtual Default_Details_Nationality_Model CreateNew()
        {
            Nationality Nationality = new Nationality();
            Default_Details_Nationality_Model Default_Details_Nationality_Model = this.ConverTo_Default_Details_Nationality_Model(Nationality);
            return Default_Details_Nationality_Model;
        } 
    }

	public partial class Default_Details_Nationality_ModelBLM : BaseDefault_Details_Nationality_ModelBLM
	{
		public Default_Details_Nationality_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
