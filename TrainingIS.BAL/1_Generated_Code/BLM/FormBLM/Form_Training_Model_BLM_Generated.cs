//modelType = Form_Training_Model

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
	public partial class BaseForm_Training_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseForm_Training_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Training ConverTo_Training(Form_Training_Model Form_Training_Model)
        {
			Training Training = null;
            if (Form_Training_Model.Id != 0)
            {
                Training = new TrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Form_Training_Model.Id);
            }
            else
            {
                Training = new Training();
            } 
			Training.TrainingYearId = Form_Training_Model.TrainingYearId;
			Training.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Training_Model.TrainingYearId)) ;
			Training.ModuleTrainingId = Form_Training_Model.ModuleTrainingId;
			Training.ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Training_Model.ModuleTrainingId)) ;
			Training.FormerId = Form_Training_Model.FormerId;
			Training.Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Training_Model.FormerId)) ;
			Training.GroupId = Form_Training_Model.GroupId;
			Training.Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Form_Training_Model.GroupId)) ;
			Training.Code = Form_Training_Model.Code;
			Training.Description = Form_Training_Model.Description;
			Training.Id = Form_Training_Model.Id;
            return Training;
        }
        public virtual void ConverTo_Form_Training_Model(Form_Training_Model Form_Training_Model, Training Training)
        {  
			 
			Form_Training_Model.toStringValue = Training.ToString();
			Form_Training_Model.TrainingYearId = Training.TrainingYearId;
			Form_Training_Model.ModuleTrainingId = Training.ModuleTrainingId;
			Form_Training_Model.FormerId = Training.FormerId;
			Form_Training_Model.GroupId = Training.GroupId;
			Form_Training_Model.Code = Training.Code;
			Form_Training_Model.Description = Training.Description;
			Form_Training_Model.Id = Training.Id;
                     
        }

    }

	public partial class Form_Training_ModelBLM : BaseForm_Training_Model_BLM
	{
		public Form_Training_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
