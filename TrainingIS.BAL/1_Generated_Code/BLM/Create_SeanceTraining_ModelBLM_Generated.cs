//modelType = Create_SeanceTraining_Model

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
using TrainingIS.Models.SeanceTrainings;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseCreate_SeanceTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseCreate_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Create_SeanceTraining_Model Create_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Create_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Create_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Create_SeanceTraining_Model.SeanceDate;
			SeanceTraining.SeancePlanningId = Create_SeanceTraining_Model.SeancePlanningId;
			SeanceTraining.SeancePlanning = new SeancePlanningBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Convert.ToInt64(Create_SeanceTraining_Model.SeancePlanningId)) ;
			SeanceTraining.Contained = Create_SeanceTraining_Model.Contained;
			SeanceTraining.Id = Create_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Create_SeanceTraining_Model ConverTo_Create_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Create_SeanceTraining_Model Create_SeanceTraining_Model = new Create_SeanceTraining_Model();
			Create_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Create_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Create_SeanceTraining_Model.SeancePlanningId = SeanceTraining.SeancePlanningId;
			Create_SeanceTraining_Model.Contained = SeanceTraining.Contained;
			Create_SeanceTraining_Model.Id = SeanceTraining.Id;
        
            return Create_SeanceTraining_Model;            
        }

		public virtual Create_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Create_SeanceTraining_Model Create_SeanceTraining_Model = this.ConverTo_Create_SeanceTraining_Model(SeanceTraining);
            return Create_SeanceTraining_Model;
        } 
    }

	public partial class Create_SeanceTraining_ModelBLM : BaseCreate_SeanceTraining_ModelBLM
	{
		public Create_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
