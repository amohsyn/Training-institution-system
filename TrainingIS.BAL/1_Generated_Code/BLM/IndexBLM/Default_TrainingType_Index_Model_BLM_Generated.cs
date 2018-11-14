//modelType = Default_TrainingType_Index_Model

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
	public partial class BaseDefault_TrainingType_Index_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_TrainingType_Index_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TrainingType ConverTo_TrainingType(Default_TrainingType_Index_Model Default_TrainingType_Index_Model)
        {
			TrainingType TrainingType = null;
            if (Default_TrainingType_Index_Model.Id != 0)
            {
                TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_TrainingType_Index_Model.Id);
            }
            else
            {
                TrainingType = new TrainingType();
            } 
			TrainingType.Code = Default_TrainingType_Index_Model.Code;
			TrainingType.Name = Default_TrainingType_Index_Model.Name;
			TrainingType.Description = Default_TrainingType_Index_Model.Description;
			TrainingType.Id = Default_TrainingType_Index_Model.Id;
            return TrainingType;
        }
        public virtual Default_TrainingType_Index_Model ConverTo_Default_TrainingType_Index_Model(TrainingType TrainingType)
        {  
			Default_TrainingType_Index_Model Default_TrainingType_Index_Model = new Default_TrainingType_Index_Model();
			Default_TrainingType_Index_Model.toStringValue = TrainingType.ToString();
			Default_TrainingType_Index_Model.Code = TrainingType.Code;
			Default_TrainingType_Index_Model.Name = TrainingType.Name;
			Default_TrainingType_Index_Model.Description = TrainingType.Description;
			Default_TrainingType_Index_Model.Id = TrainingType.Id;
            return Default_TrainingType_Index_Model;            
        }

		public virtual Default_TrainingType_Index_Model CreateNew()
        {
            TrainingType TrainingType = new TrainingTypeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_TrainingType_Index_Model Default_TrainingType_Index_Model = this.ConverTo_Default_TrainingType_Index_Model(TrainingType);
            return Default_TrainingType_Index_Model;
        } 

		public virtual List<Default_TrainingType_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TrainingTypeBLO entityBLO = new TrainingTypeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TrainingType> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_TrainingType_Index_Model> ls_models = new List<Default_TrainingType_Index_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_TrainingType_Index_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_TrainingType_Index_ModelBLM : BaseDefault_TrainingType_Index_Model_BLM
	{
		public Default_TrainingType_Index_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
