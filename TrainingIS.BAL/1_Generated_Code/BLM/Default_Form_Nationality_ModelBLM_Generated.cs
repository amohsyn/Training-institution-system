//modelType = Default_Form_Nationality_Model

using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Nationality_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Nationality_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Nationality ConverTo_Nationality(Default_Form_Nationality_Model Default_Form_Nationality_Model)
        {
			Nationality Nationality = null;
            if (Default_Form_Nationality_Model.Id != 0)
            {
                Nationality = new NationalityBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Nationality_Model.Id);
            }
            else
            {
                Nationality = new Nationality();
            } 
			Nationality.Code = Default_Form_Nationality_Model.Code;
			Nationality.Name = Default_Form_Nationality_Model.Name;
			Nationality.Description = Default_Form_Nationality_Model.Description;
			Nationality.Id = Default_Form_Nationality_Model.Id;
            return Nationality;
        }
        public virtual Default_Form_Nationality_Model ConverTo_Default_Form_Nationality_Model(Nationality Nationality)
        {  
			Default_Form_Nationality_Model Default_Form_Nationality_Model = new Default_Form_Nationality_Model();
			Default_Form_Nationality_Model.toStringValue = Nationality.ToString();
			Default_Form_Nationality_Model.Code = Nationality.Code;
			Default_Form_Nationality_Model.Name = Nationality.Name;
			Default_Form_Nationality_Model.Description = Nationality.Description;
			Default_Form_Nationality_Model.Id = Nationality.Id;
            return Default_Form_Nationality_Model;            
        }

		public virtual Default_Form_Nationality_Model CreateNew()
        {
            Nationality Nationality = new NationalityBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Nationality_Model Default_Form_Nationality_Model = this.ConverTo_Default_Form_Nationality_Model(Nationality);
            return Default_Form_Nationality_Model;
        } 
    }

	public partial class Default_Form_Nationality_ModelBLM : BaseDefault_Form_Nationality_ModelBLM
	{
		public Default_Form_Nationality_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
