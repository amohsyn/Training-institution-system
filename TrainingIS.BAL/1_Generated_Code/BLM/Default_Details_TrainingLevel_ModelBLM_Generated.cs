//modelType = Default_Details_TrainingLevel_Model

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
	public partial class BaseDefault_Details_TrainingLevel_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_TrainingLevel_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TrainingLevel ConverTo_TrainingLevel(Default_Details_TrainingLevel_Model Default_Details_TrainingLevel_Model)
        {
			TrainingLevel TrainingLevel = null;
            if (Default_Details_TrainingLevel_Model.Id != 0)
            {
                TrainingLevel = new TrainingLevelBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_TrainingLevel_Model.Id);
            }
            else
            {
                TrainingLevel = new TrainingLevel();
            } 
			TrainingLevel.Code = Default_Details_TrainingLevel_Model.Code;
			TrainingLevel.Name = Default_Details_TrainingLevel_Model.Name;
			TrainingLevel.Description = Default_Details_TrainingLevel_Model.Description;
			TrainingLevel.Id = Default_Details_TrainingLevel_Model.Id;
            return TrainingLevel;
        }
        public virtual Default_Details_TrainingLevel_Model ConverTo_Default_Details_TrainingLevel_Model(TrainingLevel TrainingLevel)
        {  
			Default_Details_TrainingLevel_Model Default_Details_TrainingLevel_Model = new Default_Details_TrainingLevel_Model();
			Default_Details_TrainingLevel_Model.toStringValue = TrainingLevel.ToString();
			Default_Details_TrainingLevel_Model.Code = TrainingLevel.Code;
			Default_Details_TrainingLevel_Model.Name = TrainingLevel.Name;
			Default_Details_TrainingLevel_Model.Description = TrainingLevel.Description;
			Default_Details_TrainingLevel_Model.Id = TrainingLevel.Id;
            return Default_Details_TrainingLevel_Model;            
        }

		public virtual Default_Details_TrainingLevel_Model CreateNew()
        {
            TrainingLevel TrainingLevel = new TrainingLevelBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_TrainingLevel_Model Default_Details_TrainingLevel_Model = this.ConverTo_Default_Details_TrainingLevel_Model(TrainingLevel);
            return Default_Details_TrainingLevel_Model;
        } 
    }

	public partial class Default_Details_TrainingLevel_ModelBLM : BaseDefault_Details_TrainingLevel_ModelBLM
	{
		public Default_Details_TrainingLevel_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
