﻿//modelType = Default_Form_YearStudy_Model

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
	public partial class BaseDefault_Form_YearStudy_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_YearStudy_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual YearStudy ConverTo_YearStudy(Default_Form_YearStudy_Model Default_Form_YearStudy_Model)
        {
			YearStudy YearStudy = null;
            if (Default_Form_YearStudy_Model.Id != 0)
            {
                YearStudy = new YearStudyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_YearStudy_Model.Id);
            }
            else
            {
                YearStudy = new YearStudy();
            } 
			YearStudy.Code = Default_Form_YearStudy_Model.Code;
			YearStudy.Name = Default_Form_YearStudy_Model.Name;
			YearStudy.Description = Default_Form_YearStudy_Model.Description;
			YearStudy.Reference = Default_Form_YearStudy_Model.Reference;
			YearStudy.Id = Default_Form_YearStudy_Model.Id;
            return YearStudy;
        }
        public virtual Default_Form_YearStudy_Model ConverTo_Default_Form_YearStudy_Model(YearStudy YearStudy)
        {  
			Default_Form_YearStudy_Model Default_Form_YearStudy_Model = new Default_Form_YearStudy_Model();
			Default_Form_YearStudy_Model.toStringValue = YearStudy.ToString();
			Default_Form_YearStudy_Model.Code = YearStudy.Code;
			Default_Form_YearStudy_Model.Name = YearStudy.Name;
			Default_Form_YearStudy_Model.Description = YearStudy.Description;
			Default_Form_YearStudy_Model.Id = YearStudy.Id;
			Default_Form_YearStudy_Model.Reference = YearStudy.Reference;
            return Default_Form_YearStudy_Model;            
        }

		public virtual Default_Form_YearStudy_Model CreateNew()
        {
            YearStudy YearStudy = new YearStudyBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_YearStudy_Model Default_Form_YearStudy_Model = this.ConverTo_Default_Form_YearStudy_Model(YearStudy);
            return Default_Form_YearStudy_Model;
        } 

		public virtual List<Default_Form_YearStudy_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            YearStudyBLO entityBLO = new YearStudyBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<YearStudy> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_YearStudy_Model> ls_models = new List<Default_Form_YearStudy_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_YearStudy_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_YearStudy_ModelBLM : BaseDefault_Form_YearStudy_ModelBLM
	{
		public Default_Form_YearStudy_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
