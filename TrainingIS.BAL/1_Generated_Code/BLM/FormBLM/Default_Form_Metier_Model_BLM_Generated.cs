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
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_Metier_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Metier_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
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
			Metier.Reference = Default_Form_Metier_Model.Reference;
			Metier.Id = Default_Form_Metier_Model.Id;
            return Metier;
        }
        public virtual void ConverTo_Default_Form_Metier_Model(Default_Form_Metier_Model Default_Form_Metier_Model, Metier Metier)
        {  
			 
			Default_Form_Metier_Model.toStringValue = Metier.ToString();
			Default_Form_Metier_Model.Code = Metier.Code;
			Default_Form_Metier_Model.Name = Metier.Name;
			Default_Form_Metier_Model.Description = Metier.Description;
			Default_Form_Metier_Model.Id = Metier.Id;
			Default_Form_Metier_Model.Reference = Metier.Reference;
                     
        }

    }

	public partial class Default_Form_Metier_ModelBLM : BaseDefault_Form_Metier_Model_BLM
	{
		public Default_Form_Metier_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
