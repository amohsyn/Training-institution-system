//modelType = Default_Details_TrainingYear_Model

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
	public partial class BaseDefault_Details_TrainingYear_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_TrainingYear_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TrainingYear ConverTo_TrainingYear(Default_Details_TrainingYear_Model Default_Details_TrainingYear_Model)
        {
			TrainingYear TrainingYear = null;
            if (Default_Details_TrainingYear_Model.Id != 0)
            {
                TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_TrainingYear_Model.Id);
            }
            else
            {
                TrainingYear = new TrainingYear();
            } 
			TrainingYear.Code = Default_Details_TrainingYear_Model.Code;
			TrainingYear.StartDate = DefaultDateTime_If_Empty(Default_Details_TrainingYear_Model.StartDate);
			TrainingYear.EndtDate = DefaultDateTime_If_Empty(Default_Details_TrainingYear_Model.EndtDate);
			TrainingYear.Id = Default_Details_TrainingYear_Model.Id;
            return TrainingYear;
        }
        public virtual Default_Details_TrainingYear_Model ConverTo_Default_Details_TrainingYear_Model(TrainingYear TrainingYear)
        {  
			Default_Details_TrainingYear_Model Default_Details_TrainingYear_Model = new Default_Details_TrainingYear_Model();
			Default_Details_TrainingYear_Model.toStringValue = TrainingYear.ToString();
			Default_Details_TrainingYear_Model.Code = TrainingYear.Code;
			Default_Details_TrainingYear_Model.StartDate = DefaultDateTime_If_Empty(TrainingYear.StartDate);
			Default_Details_TrainingYear_Model.EndtDate = DefaultDateTime_If_Empty(TrainingYear.EndtDate);
			Default_Details_TrainingYear_Model.Id = TrainingYear.Id;
            return Default_Details_TrainingYear_Model;            
        }

		public virtual Default_Details_TrainingYear_Model CreateNew()
        {
            TrainingYear TrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_TrainingYear_Model Default_Details_TrainingYear_Model = this.ConverTo_Default_Details_TrainingYear_Model(TrainingYear);
            return Default_Details_TrainingYear_Model;
        } 

        public List<Default_Details_TrainingYear_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            TrainingYearBLO entityBLO = new TrainingYearBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TrainingYear> Query_Entity = entityBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_TrainingYear_Model> ls_models = new List<Default_Details_TrainingYear_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_TrainingYear_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_TrainingYear_ModelBLM : BaseDefault_Details_TrainingYear_ModelBLM
	{
		public Default_Details_TrainingYear_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
