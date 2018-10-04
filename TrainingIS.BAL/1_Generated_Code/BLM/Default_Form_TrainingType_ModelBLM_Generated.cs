//modelType = Default_Form_TrainingType_Model

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
	public partial class BaseDefault_Form_TrainingType_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_TrainingType_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TrainingType ConverTo_TrainingType(Default_Form_TrainingType_Model Default_Form_TrainingType_Model)
        {
			TrainingType TrainingType = null;
            if (Default_Form_TrainingType_Model.Id != 0)
            {
                TrainingType = new TrainingTypeBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_TrainingType_Model.Id);
            }
            else
            {
                TrainingType = new TrainingType();
            } 
			TrainingType.Code = Default_Form_TrainingType_Model.Code;
			TrainingType.Name = Default_Form_TrainingType_Model.Name;
			TrainingType.Description = Default_Form_TrainingType_Model.Description;
			TrainingType.Id = Default_Form_TrainingType_Model.Id;
            return TrainingType;
        }
        public virtual Default_Form_TrainingType_Model ConverTo_Default_Form_TrainingType_Model(TrainingType TrainingType)
        {  
			Default_Form_TrainingType_Model Default_Form_TrainingType_Model = new Default_Form_TrainingType_Model();
			Default_Form_TrainingType_Model.toStringValue = TrainingType.ToString();
			Default_Form_TrainingType_Model.Code = TrainingType.Code;
			Default_Form_TrainingType_Model.Name = TrainingType.Name;
			Default_Form_TrainingType_Model.Description = TrainingType.Description;
			Default_Form_TrainingType_Model.Id = TrainingType.Id;
            return Default_Form_TrainingType_Model;            
        }

		public virtual Default_Form_TrainingType_Model CreateNew()
        {
            TrainingType TrainingType = new TrainingTypeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_TrainingType_Model Default_Form_TrainingType_Model = this.ConverTo_Default_Form_TrainingType_Model(TrainingType);
            return Default_Form_TrainingType_Model;
        } 

		public virtual List<Default_Form_TrainingType_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TrainingTypeBLO entityBLO = new TrainingTypeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TrainingType> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_TrainingType_Model> ls_models = new List<Default_Form_TrainingType_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_TrainingType_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_TrainingType_ModelBLM : BaseDefault_Form_TrainingType_ModelBLM
	{
		public Default_Form_TrainingType_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
