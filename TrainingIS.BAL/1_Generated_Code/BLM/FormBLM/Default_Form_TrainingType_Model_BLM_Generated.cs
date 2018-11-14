//modelType = Default_Form_TrainingType_Model

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
	public partial class BaseDefault_Form_TrainingType_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_TrainingType_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TrainingType ConverTo_TrainingType(Default_Form_TrainingType_Model Default_Form_TrainingType_Model)
        {
			TrainingType TrainingType = null;
            if (Default_Form_TrainingType_Model.Id != 0)
            {
                TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_TrainingType_Model.Id);
            }
            else
            {
                TrainingType = new TrainingType();
            } 
			TrainingType.Code = Default_Form_TrainingType_Model.Code;
			TrainingType.Name = Default_Form_TrainingType_Model.Name;
			TrainingType.Description = Default_Form_TrainingType_Model.Description;
			TrainingType.Reference = Default_Form_TrainingType_Model.Reference;
			TrainingType.Id = Default_Form_TrainingType_Model.Id;
            return TrainingType;
        }
        public virtual void ConverTo_Default_Form_TrainingType_Model(Default_Form_TrainingType_Model Default_Form_TrainingType_Model, TrainingType TrainingType)
        {  
			 
			Default_Form_TrainingType_Model.toStringValue = TrainingType.ToString();
			Default_Form_TrainingType_Model.Code = TrainingType.Code;
			Default_Form_TrainingType_Model.Name = TrainingType.Name;
			Default_Form_TrainingType_Model.Description = TrainingType.Description;
			Default_Form_TrainingType_Model.Id = TrainingType.Id;
			Default_Form_TrainingType_Model.Reference = TrainingType.Reference;
                     
        }

    }

	public partial class Default_Form_TrainingType_ModelBLM : BaseDefault_Form_TrainingType_Model_BLM
	{
		public Default_Form_TrainingType_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
