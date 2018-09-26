//modelType = Default_Details_YearStudy_Model

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
	public partial class BaseDefault_Details_YearStudy_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_YearStudy_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual YearStudy ConverTo_YearStudy(Default_Details_YearStudy_Model Default_Details_YearStudy_Model)
        {
			YearStudy YearStudy = null;
            if (Default_Details_YearStudy_Model.Id != 0)
            {
                YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_YearStudy_Model.Id);
            }
            else
            {
                YearStudy = new YearStudy();
            } 
			YearStudy.Code = Default_Details_YearStudy_Model.Code;
			YearStudy.Name = Default_Details_YearStudy_Model.Name;
			YearStudy.Description = Default_Details_YearStudy_Model.Description;
			YearStudy.Id = Default_Details_YearStudy_Model.Id;
            return YearStudy;
        }
        public virtual Default_Details_YearStudy_Model ConverTo_Default_Details_YearStudy_Model(YearStudy YearStudy)
        {  
			Default_Details_YearStudy_Model Default_Details_YearStudy_Model = new Default_Details_YearStudy_Model();
			Default_Details_YearStudy_Model.toStringValue = YearStudy.ToString();
			Default_Details_YearStudy_Model.Code = YearStudy.Code;
			Default_Details_YearStudy_Model.Name = YearStudy.Name;
			Default_Details_YearStudy_Model.Description = YearStudy.Description;
			Default_Details_YearStudy_Model.Id = YearStudy.Id;
            return Default_Details_YearStudy_Model;            
        }

		public virtual Default_Details_YearStudy_Model CreateNew()
        {
            YearStudy YearStudy = new YearStudyBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_YearStudy_Model Default_Details_YearStudy_Model = this.ConverTo_Default_Details_YearStudy_Model(YearStudy);
            return Default_Details_YearStudy_Model;
        } 

        public List<Default_Details_YearStudy_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            YearStudyBLO entityBLO = new YearStudyBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<YearStudy> Query_Entity = entityBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_YearStudy_Model> ls_models = new List<Default_Details_YearStudy_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_YearStudy_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_YearStudy_ModelBLM : BaseDefault_Details_YearStudy_ModelBLM
	{
		public Default_Details_YearStudy_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
