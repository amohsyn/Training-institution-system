//modelType = Default_Form_TrainingYear_Model

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
	public partial class BaseDefault_Form_TrainingYear_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_TrainingYear_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TrainingYear ConverTo_TrainingYear(Default_Form_TrainingYear_Model Default_Form_TrainingYear_Model)
        {
			TrainingYear TrainingYear = null;
            if (Default_Form_TrainingYear_Model.Id != 0)
            {
                TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_TrainingYear_Model.Id);
            }
            else
            {
                TrainingYear = new TrainingYear();
            } 
			TrainingYear.Code = Default_Form_TrainingYear_Model.Code;
			TrainingYear.StartDate = DefaultDateTime_If_Empty(Default_Form_TrainingYear_Model.StartDate);
			TrainingYear.EndtDate = DefaultDateTime_If_Empty(Default_Form_TrainingYear_Model.EndtDate);
			TrainingYear.Reference = Default_Form_TrainingYear_Model.Reference;
			TrainingYear.Id = Default_Form_TrainingYear_Model.Id;
            return TrainingYear;
        }
        public virtual void ConverTo_Default_Form_TrainingYear_Model(Default_Form_TrainingYear_Model Default_Form_TrainingYear_Model, TrainingYear TrainingYear)
        {  
			 
			Default_Form_TrainingYear_Model.toStringValue = TrainingYear.ToString();
			Default_Form_TrainingYear_Model.Code = TrainingYear.Code;
			Default_Form_TrainingYear_Model.StartDate = DefaultDateTime_If_Empty(TrainingYear.StartDate);
			Default_Form_TrainingYear_Model.EndtDate = DefaultDateTime_If_Empty(TrainingYear.EndtDate);
			Default_Form_TrainingYear_Model.Id = TrainingYear.Id;
			Default_Form_TrainingYear_Model.Reference = TrainingYear.Reference;
                     
        }

    }

	public partial class Default_Form_TrainingYear_ModelBLM : BaseDefault_Form_TrainingYear_Model_BLM
	{
		public Default_Form_TrainingYear_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
