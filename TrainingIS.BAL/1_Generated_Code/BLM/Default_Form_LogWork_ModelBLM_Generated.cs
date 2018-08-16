﻿//modelType = Default_Form_LogWork_Model

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
using GApp.Entities;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Form_LogWork_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_LogWork_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual LogWork ConverTo_LogWork(Default_Form_LogWork_Model Default_Form_LogWork_Model)
        {
			LogWork LogWork = null;
            if (Default_Form_LogWork_Model.Id != 0)
            {
                LogWork = new LogWorkBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_LogWork_Model.Id);
            }
            else
            {
                LogWork = new LogWork();
            } 
			LogWork.UserId = Default_Form_LogWork_Model.UserId;
			LogWork.OperationWorkType = Default_Form_LogWork_Model.OperationWorkType;
			LogWork.OperationReference = Default_Form_LogWork_Model.OperationReference;
			LogWork.EntityType = Default_Form_LogWork_Model.EntityType;
			LogWork.Description = Default_Form_LogWork_Model.Description;
			LogWork.Id = Default_Form_LogWork_Model.Id;
            return LogWork;
        }
        public virtual Default_Form_LogWork_Model ConverTo_Default_Form_LogWork_Model(LogWork LogWork)
        {  
			Default_Form_LogWork_Model Default_Form_LogWork_Model = new Default_Form_LogWork_Model();
			Default_Form_LogWork_Model.toStringValue = LogWork.ToString();
			Default_Form_LogWork_Model.UserId = LogWork.UserId;
			Default_Form_LogWork_Model.OperationWorkType = LogWork.OperationWorkType;
			Default_Form_LogWork_Model.OperationReference = LogWork.OperationReference;
			Default_Form_LogWork_Model.EntityType = LogWork.EntityType;
			Default_Form_LogWork_Model.Description = LogWork.Description;
			Default_Form_LogWork_Model.Id = LogWork.Id;
            return Default_Form_LogWork_Model;            
        }

		public virtual Default_Form_LogWork_Model CreateNew()
        {
            LogWork LogWork = new LogWork();
            Default_Form_LogWork_Model Default_Form_LogWork_Model = this.ConverTo_Default_Form_LogWork_Model(LogWork);
            return Default_Form_LogWork_Model;
        } 
    }

	public partial class Default_Form_LogWork_ModelBLM : BaseDefault_Form_LogWork_ModelBLM
	{
		public Default_Form_LogWork_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
