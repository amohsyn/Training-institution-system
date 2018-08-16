//modelType = Default_Details_ModuleTraining_Model

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
	public partial class BaseDefault_Details_ModuleTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual ModuleTraining ConverTo_ModuleTraining(Default_Details_ModuleTraining_Model Default_Details_ModuleTraining_Model)
        {
			ModuleTraining ModuleTraining = null;
            if (Default_Details_ModuleTraining_Model.Id != 0)
            {
                ModuleTraining = new ModuleTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_ModuleTraining_Model.Id);
            }
            else
            {
                ModuleTraining = new ModuleTraining();
            } 
			ModuleTraining.Specialty = Default_Details_ModuleTraining_Model.Specialty;
			ModuleTraining.Name = Default_Details_ModuleTraining_Model.Name;
			ModuleTraining.Code = Default_Details_ModuleTraining_Model.Code;
			ModuleTraining.Description = Default_Details_ModuleTraining_Model.Description;
			ModuleTraining.Id = Default_Details_ModuleTraining_Model.Id;
            return ModuleTraining;
        }
        public virtual Default_Details_ModuleTraining_Model ConverTo_Default_Details_ModuleTraining_Model(ModuleTraining ModuleTraining)
        {  
			Default_Details_ModuleTraining_Model Default_Details_ModuleTraining_Model = new Default_Details_ModuleTraining_Model();
			Default_Details_ModuleTraining_Model.toStringValue = ModuleTraining.ToString();
			Default_Details_ModuleTraining_Model.Specialty = ModuleTraining.Specialty;
			Default_Details_ModuleTraining_Model.Name = ModuleTraining.Name;
			Default_Details_ModuleTraining_Model.Code = ModuleTraining.Code;
			Default_Details_ModuleTraining_Model.Description = ModuleTraining.Description;
			Default_Details_ModuleTraining_Model.Id = ModuleTraining.Id;
            return Default_Details_ModuleTraining_Model;            
        }

		public virtual Default_Details_ModuleTraining_Model CreateNew()
        {
            ModuleTraining ModuleTraining = new ModuleTraining();
            Default_Details_ModuleTraining_Model Default_Details_ModuleTraining_Model = this.ConverTo_Default_Details_ModuleTraining_Model(ModuleTraining);
            return Default_Details_ModuleTraining_Model;
        } 
    }

	public partial class Default_Details_ModuleTraining_ModelBLM : BaseDefault_Details_ModuleTraining_ModelBLM
	{
		public Default_Details_ModuleTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
