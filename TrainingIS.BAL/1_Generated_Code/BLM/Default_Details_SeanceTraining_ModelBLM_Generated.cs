//modelType = Default_Details_SeanceTraining_Model

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
	public partial class BaseDefault_Details_SeanceTraining_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceTraining ConverTo_SeanceTraining(Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model)
        {
			SeanceTraining SeanceTraining = null;
            if (Default_Details_SeanceTraining_Model.Id != 0)
            {
                SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_SeanceTraining_Model.Id);
            }
            else
            {
                SeanceTraining = new SeanceTraining();
            } 
			SeanceTraining.SeanceDate = Default_Details_SeanceTraining_Model.SeanceDate;
			SeanceTraining.SeancePlanning = Default_Details_SeanceTraining_Model.SeancePlanning;
			SeanceTraining.Id = Default_Details_SeanceTraining_Model.Id;
            return SeanceTraining;
        }
        public virtual Default_Details_SeanceTraining_Model ConverTo_Default_Details_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {  
			Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model = new Default_Details_SeanceTraining_Model();
			Default_Details_SeanceTraining_Model.toStringValue = SeanceTraining.ToString();
			Default_Details_SeanceTraining_Model.SeanceDate = ConversionUtil.DefaultValue_if_Null<DateTime>(SeanceTraining.SeanceDate);
			Default_Details_SeanceTraining_Model.SeancePlanning = SeanceTraining.SeancePlanning;
			Default_Details_SeanceTraining_Model.Id = SeanceTraining.Id;
            return Default_Details_SeanceTraining_Model;            
        }

		public virtual Default_Details_SeanceTraining_Model CreateNew()
        {
            SeanceTraining SeanceTraining = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_SeanceTraining_Model Default_Details_SeanceTraining_Model = this.ConverTo_Default_Details_SeanceTraining_Model(SeanceTraining);
            return Default_Details_SeanceTraining_Model;
        } 
    }

	public partial class Default_Details_SeanceTraining_ModelBLM : BaseDefault_Details_SeanceTraining_ModelBLM
	{
		public Default_Details_SeanceTraining_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
