//modelType = Default_Form_Training_Model

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
	public partial class BaseDefault_Form_Training_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_Training_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual Training ConverTo_Training(Default_Form_Training_Model Default_Form_Training_Model)
        {
			Training Training = null;
            if (Default_Form_Training_Model.Id != 0)
            {
                Training = new TrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_Training_Model.Id);
            }
            else
            {
                Training = new Training();
            } 
			Training.TrainingYearId = Default_Form_Training_Model.TrainingYearId;
			Training.TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Training_Model.TrainingYearId)) ;
			Training.ModuleTrainingId = Default_Form_Training_Model.ModuleTrainingId;
			Training.ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Training_Model.ModuleTrainingId)) ;
			Training.FormerId = Default_Form_Training_Model.FormerId;
			Training.Former = new FormerBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Training_Model.FormerId)) ;
			Training.GroupId = Default_Form_Training_Model.GroupId;
			Training.Group = new GroupBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Default_Form_Training_Model.GroupId)) ;
			Training.Code = Default_Form_Training_Model.Code;
			Training.Description = Default_Form_Training_Model.Description;
			Training.Id = Default_Form_Training_Model.Id;
            return Training;
        }
        public virtual Default_Form_Training_Model ConverTo_Default_Form_Training_Model(Training Training)
        {  
			Default_Form_Training_Model Default_Form_Training_Model = new Default_Form_Training_Model();
			Default_Form_Training_Model.toStringValue = Training.ToString();
			Default_Form_Training_Model.TrainingYearId = Training.TrainingYearId;
			Default_Form_Training_Model.ModuleTrainingId = Training.ModuleTrainingId;
			Default_Form_Training_Model.FormerId = Training.FormerId;
			Default_Form_Training_Model.GroupId = Training.GroupId;
			Default_Form_Training_Model.Code = Training.Code;
			Default_Form_Training_Model.Description = Training.Description;
			Default_Form_Training_Model.Id = Training.Id;
            return Default_Form_Training_Model;            
        }

		public virtual Default_Form_Training_Model CreateNew()
        {
            Training Training = new TrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_Training_Model Default_Form_Training_Model = this.ConverTo_Default_Form_Training_Model(Training);
            return Default_Form_Training_Model;
        } 
    }

	public partial class Default_Form_Training_ModelBLM : BaseDefault_Form_Training_ModelBLM
	{
		public Default_Form_Training_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
