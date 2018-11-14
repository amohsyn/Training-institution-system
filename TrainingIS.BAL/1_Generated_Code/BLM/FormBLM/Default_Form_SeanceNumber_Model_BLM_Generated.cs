//modelType = Default_Form_SeanceNumber_Model

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
	public partial class BaseDefault_Form_SeanceNumber_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_SeanceNumber_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceNumber ConverTo_SeanceNumber(Default_Form_SeanceNumber_Model Default_Form_SeanceNumber_Model)
        {
			SeanceNumber SeanceNumber = null;
            if (Default_Form_SeanceNumber_Model.Id != 0)
            {
                SeanceNumber = new SeanceNumberBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_SeanceNumber_Model.Id);
            }
            else
            {
                SeanceNumber = new SeanceNumber();
            } 
			SeanceNumber.Code = Default_Form_SeanceNumber_Model.Code;
			SeanceNumber.StartTime = DefaultDateTime_If_Empty(Default_Form_SeanceNumber_Model.StartTime);
			SeanceNumber.EndTime = DefaultDateTime_If_Empty(Default_Form_SeanceNumber_Model.EndTime);
			SeanceNumber.Description = Default_Form_SeanceNumber_Model.Description;
			SeanceNumber.Reference = Default_Form_SeanceNumber_Model.Reference;
			SeanceNumber.Id = Default_Form_SeanceNumber_Model.Id;
            return SeanceNumber;
        }
        public virtual void ConverTo_Default_Form_SeanceNumber_Model(Default_Form_SeanceNumber_Model Default_Form_SeanceNumber_Model, SeanceNumber SeanceNumber)
        {  
			 
			Default_Form_SeanceNumber_Model.toStringValue = SeanceNumber.ToString();
			Default_Form_SeanceNumber_Model.Code = SeanceNumber.Code;
			Default_Form_SeanceNumber_Model.StartTime = DefaultDateTime_If_Empty(SeanceNumber.StartTime);
			Default_Form_SeanceNumber_Model.EndTime = DefaultDateTime_If_Empty(SeanceNumber.EndTime);
			Default_Form_SeanceNumber_Model.Description = SeanceNumber.Description;
			Default_Form_SeanceNumber_Model.Id = SeanceNumber.Id;
			Default_Form_SeanceNumber_Model.Reference = SeanceNumber.Reference;
                     
        }

    }

	public partial class Default_Form_SeanceNumber_ModelBLM : BaseDefault_Form_SeanceNumber_Model_BLM
	{
		public Default_Form_SeanceNumber_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
