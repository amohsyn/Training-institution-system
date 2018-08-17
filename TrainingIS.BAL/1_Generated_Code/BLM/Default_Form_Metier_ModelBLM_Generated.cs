//modelType = Default_Form_Metier_Model

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
	public partial class BaseDefault_Form_Metier_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Metier_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Metier ConverTo_Metier(Default_Form_Metier_Model Default_Form_Metier_Model)
        {
			Metier Metier = null;
            if (Default_Form_Metier_Model.Id != 0)
            {
                Metier = new MetierBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Metier_Model.Id);
            }
            else
            {
                Metier = new Metier();
            } 
			Metier.Code = Default_Form_Metier_Model.Code;
			Metier.Name = Default_Form_Metier_Model.Name;
			Metier.Description = Default_Form_Metier_Model.Description;
			Metier.Id = Default_Form_Metier_Model.Id;
            return Metier;
        }
        public virtual Default_Form_Metier_Model ConverTo_Default_Form_Metier_Model(Metier Metier)
        {  
			Default_Form_Metier_Model Default_Form_Metier_Model = new Default_Form_Metier_Model();
			Default_Form_Metier_Model.toStringValue = Metier.ToString();
			Default_Form_Metier_Model.Code = Metier.Code;
			Default_Form_Metier_Model.Name = Metier.Name;
			Default_Form_Metier_Model.Description = Metier.Description;
			Default_Form_Metier_Model.Id = Metier.Id;
            return Default_Form_Metier_Model;            
        }

		public virtual Default_Form_Metier_Model CreateNew()
        {
            Metier Metier = new MetierBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Metier_Model Default_Form_Metier_Model = this.ConverTo_Default_Form_Metier_Model(Metier);
            return Default_Form_Metier_Model;
        } 
    }

	public partial class Default_Form_Metier_ModelBLM : BaseDefault_Form_Metier_ModelBLM
	{
		public Default_Form_Metier_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
