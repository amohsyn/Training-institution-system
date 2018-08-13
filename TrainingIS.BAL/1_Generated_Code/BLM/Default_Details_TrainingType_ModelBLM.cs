using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;
using GApp.Core.Utils;
using GApp.Entities;
using GApp.DAL;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_TrainingType_ModelBLM : BaseModelBLM
    {
        
        public BaseDefault_Details_TrainingType_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork)
        {

        }

        public virtual TrainingType ConverTo_TrainingType(Default_Details_TrainingType_Model Default_Details_TrainingType_Model)
        {
			TrainingType TrainingType = null;
            if (Default_Details_TrainingType_Model.Id != 0)
            {
                TrainingType = new TrainingTypeBLO(this.UnitOfWork).FindBaseEntityByID(Default_Details_TrainingType_Model.Id);
            }
            else
            {
                TrainingType = new TrainingType();
            } 
			TrainingType.Code = Default_Details_TrainingType_Model.Code;
			TrainingType.Name = Default_Details_TrainingType_Model.Name;
			TrainingType.Description = Default_Details_TrainingType_Model.Description;
			TrainingType.Id = Default_Details_TrainingType_Model.Id;
            return TrainingType;
        }
        public virtual Default_Details_TrainingType_Model ConverTo_Default_Details_TrainingType_Model(TrainingType TrainingType)
        {  
			Default_Details_TrainingType_Model Default_Details_TrainingType_Model = new Default_Details_TrainingType_Model();
			Default_Details_TrainingType_Model.toStringValue = TrainingType.ToString();
			Default_Details_TrainingType_Model.Code = TrainingType.Code;
			Default_Details_TrainingType_Model.Name = TrainingType.Name;
			Default_Details_TrainingType_Model.Description = TrainingType.Description;
			Default_Details_TrainingType_Model.Id = TrainingType.Id;
            return Default_Details_TrainingType_Model;            
        }

		public virtual Default_Details_TrainingType_Model CreateNew()
        {
            TrainingType TrainingType = new TrainingType();
            Default_Details_TrainingType_Model Default_Details_TrainingType_Model = this.ConverTo_Default_Details_TrainingType_Model(TrainingType);
            return Default_Details_TrainingType_Model;
        } 
    }

	public partial class Default_Details_TrainingType_ModelBLM : BaseDefault_Details_TrainingType_ModelBLM
	{
		public Default_Details_TrainingType_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
