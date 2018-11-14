//modelType = Default_YearStudy_Details_Model

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
	public partial class BaseDefault_YearStudy_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_YearStudy_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual YearStudy ConverTo_YearStudy(Default_YearStudy_Details_Model Default_YearStudy_Details_Model)
        {
			YearStudy YearStudy = null;
            if (Default_YearStudy_Details_Model.Id != 0)
            {
                YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_YearStudy_Details_Model.Id);
            }
            else
            {
                YearStudy = new YearStudy();
            } 
			YearStudy.Code = Default_YearStudy_Details_Model.Code;
			YearStudy.Name = Default_YearStudy_Details_Model.Name;
			YearStudy.Description = Default_YearStudy_Details_Model.Description;
			YearStudy.Id = Default_YearStudy_Details_Model.Id;
            return YearStudy;
        }
        public virtual Default_YearStudy_Details_Model ConverTo_Default_YearStudy_Details_Model(YearStudy YearStudy)
        {  
			Default_YearStudy_Details_Model Default_YearStudy_Details_Model = new Default_YearStudy_Details_Model();
			Default_YearStudy_Details_Model.toStringValue = YearStudy.ToString();
			Default_YearStudy_Details_Model.Code = YearStudy.Code;
			Default_YearStudy_Details_Model.Name = YearStudy.Name;
			Default_YearStudy_Details_Model.Description = YearStudy.Description;
			Default_YearStudy_Details_Model.Id = YearStudy.Id;
            return Default_YearStudy_Details_Model;            
        }

		public virtual Default_YearStudy_Details_Model CreateNew()
        {
            YearStudy YearStudy = new YearStudyBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_YearStudy_Details_Model Default_YearStudy_Details_Model = this.ConverTo_Default_YearStudy_Details_Model(YearStudy);
            return Default_YearStudy_Details_Model;
        } 

		public virtual List<Default_YearStudy_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            YearStudyBLO entityBLO = new YearStudyBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<YearStudy> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_YearStudy_Details_Model> ls_models = new List<Default_YearStudy_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_YearStudy_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_YearStudy_Details_ModelBLM : BaseDefault_YearStudy_Details_Model_BLM
	{
		public Default_YearStudy_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
