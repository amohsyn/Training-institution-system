﻿//modelType = Default_Details_DisciplineCategory_Model

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
	public partial class BaseDefault_Details_DisciplineCategory_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_DisciplineCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual DisciplineCategory ConverTo_DisciplineCategory(Default_Details_DisciplineCategory_Model Default_Details_DisciplineCategory_Model)
        {
			DisciplineCategory DisciplineCategory = null;
            if (Default_Details_DisciplineCategory_Model.Id != 0)
            {
                DisciplineCategory = new DisciplineCategoryBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_DisciplineCategory_Model.Id);
            }
            else
            {
                DisciplineCategory = new DisciplineCategory();
            } 
			DisciplineCategory.Code = Default_Details_DisciplineCategory_Model.Code;
			DisciplineCategory.Name = Default_Details_DisciplineCategory_Model.Name;
			DisciplineCategory.Description = Default_Details_DisciplineCategory_Model.Description;
			DisciplineCategory.Id = Default_Details_DisciplineCategory_Model.Id;
            return DisciplineCategory;
        }
        public virtual Default_Details_DisciplineCategory_Model ConverTo_Default_Details_DisciplineCategory_Model(DisciplineCategory DisciplineCategory)
        {  
			Default_Details_DisciplineCategory_Model Default_Details_DisciplineCategory_Model = new Default_Details_DisciplineCategory_Model();
			Default_Details_DisciplineCategory_Model.toStringValue = DisciplineCategory.ToString();
			Default_Details_DisciplineCategory_Model.Code = DisciplineCategory.Code;
			Default_Details_DisciplineCategory_Model.Name = DisciplineCategory.Name;
			Default_Details_DisciplineCategory_Model.Description = DisciplineCategory.Description;
			Default_Details_DisciplineCategory_Model.Id = DisciplineCategory.Id;
            return Default_Details_DisciplineCategory_Model;            
        }

		public virtual Default_Details_DisciplineCategory_Model CreateNew()
        {
            DisciplineCategory DisciplineCategory = new DisciplineCategoryBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_DisciplineCategory_Model Default_Details_DisciplineCategory_Model = this.ConverTo_Default_Details_DisciplineCategory_Model(DisciplineCategory);
            return Default_Details_DisciplineCategory_Model;
        } 

		public virtual List<Default_Details_DisciplineCategory_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            DisciplineCategoryBLO entityBLO = new DisciplineCategoryBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<DisciplineCategory> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_DisciplineCategory_Model> ls_models = new List<Default_Details_DisciplineCategory_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_DisciplineCategory_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_DisciplineCategory_ModelBLM : BaseDefault_Details_DisciplineCategory_ModelBLM
	{
		public Default_Details_DisciplineCategory_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}