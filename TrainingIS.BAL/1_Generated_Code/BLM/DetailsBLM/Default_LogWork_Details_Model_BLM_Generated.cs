//modelType = Default_LogWork_Details_Model

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
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_LogWork_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_LogWork_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual LogWork ConverTo_LogWork(Default_LogWork_Details_Model Default_LogWork_Details_Model)
        {
			LogWork LogWork = null;
            if (Default_LogWork_Details_Model.Id != 0)
            {
                LogWork = new LogWorkBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_LogWork_Details_Model.Id);
            }
            else
            {
                LogWork = new LogWork();
            } 
			LogWork.UserId = Default_LogWork_Details_Model.UserId;
			LogWork.OperationWorkType = Default_LogWork_Details_Model.OperationWorkType;
			LogWork.OperationReference = Default_LogWork_Details_Model.OperationReference;
			LogWork.EntityType = Default_LogWork_Details_Model.EntityType;
			LogWork.Description = Default_LogWork_Details_Model.Description;
			LogWork.Id = Default_LogWork_Details_Model.Id;
            return LogWork;
        }
        public virtual Default_LogWork_Details_Model ConverTo_Default_LogWork_Details_Model(LogWork LogWork)
        {  
			Default_LogWork_Details_Model Default_LogWork_Details_Model = new Default_LogWork_Details_Model();
			Default_LogWork_Details_Model.toStringValue = LogWork.ToString();
			Default_LogWork_Details_Model.UserId = LogWork.UserId;
			Default_LogWork_Details_Model.OperationWorkType = LogWork.OperationWorkType;
			Default_LogWork_Details_Model.OperationReference = LogWork.OperationReference;
			Default_LogWork_Details_Model.EntityType = LogWork.EntityType;
			Default_LogWork_Details_Model.Description = LogWork.Description;
			Default_LogWork_Details_Model.Id = LogWork.Id;
            return Default_LogWork_Details_Model;            
        }

		public virtual Default_LogWork_Details_Model CreateNew()
        {
            LogWork LogWork = new LogWorkBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_LogWork_Details_Model Default_LogWork_Details_Model = this.ConverTo_Default_LogWork_Details_Model(LogWork);
            return Default_LogWork_Details_Model;
        } 

		public virtual List<Default_LogWork_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            LogWorkBLO entityBLO = new LogWorkBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<LogWork> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_LogWork_Details_Model> ls_models = new List<Default_LogWork_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_LogWork_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_LogWork_Details_ModelBLM : BaseDefault_LogWork_Details_Model_BLM
	{
		public Default_LogWork_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
