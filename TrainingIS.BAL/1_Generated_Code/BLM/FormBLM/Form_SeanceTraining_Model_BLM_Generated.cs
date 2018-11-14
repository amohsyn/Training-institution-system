//modelType = Form_SeanceTraining_Model

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
	public partial class BaseForm_SeanceTraining_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseForm_SeanceTraining_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Form_SeanceTraining_Model Form_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Form_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Form_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Form_SeanceTraining_Model.SeanceDate;
			SeanceTraining.SeancePlanningId = Form_SeanceTraining_Model.SeancePlanningId;
			SeanceTraining.SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_SeanceTraining_Model.SeancePlanningId)) ;
			SeanceTraining.Contained = Form_SeanceTraining_Model.Contained;
			SeanceTraining.Id = Form_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual void ConverTo_Form_SeanceTraining_Model(Form_SeanceTraining_Model Form_SeanceTraining_Model, SeanceTraining SeanceTraining)
        {  
			 
			Form_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Form_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Form_SeanceTraining_Model.SeancePlanningId = SeanceTraining.SeancePlanningId;
			Form_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Form_SeanceTraining_Model.Id = SeanceTraining.Id;
                     
        }

    }

	public partial class Form_SeanceTraining_ModelBLM : BaseForm_SeanceTraining_Model_BLM
	{
		public Form_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
