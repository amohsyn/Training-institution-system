﻿//modelType = Default_Details_FormerSpecialty_Model

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
	public partial class BaseDefault_Details_FormerSpecialty_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_FormerSpecialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual FormerSpecialty ConverTo_FormerSpecialty(Default_Details_FormerSpecialty_Model Default_Details_FormerSpecialty_Model)
        {
			FormerSpecialty FormerSpecialty = null;
            if (Default_Details_FormerSpecialty_Model.Id != 0)
            {
                FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_FormerSpecialty_Model.Id);
            }
            else
            {
                FormerSpecialty = new FormerSpecialty();
            } 
			FormerSpecialty.Code = Default_Details_FormerSpecialty_Model.Code;
			FormerSpecialty.Name = Default_Details_FormerSpecialty_Model.Name;
			FormerSpecialty.Description = Default_Details_FormerSpecialty_Model.Description;
			FormerSpecialty.Id = Default_Details_FormerSpecialty_Model.Id;
            return FormerSpecialty;
        }
        public virtual Default_Details_FormerSpecialty_Model ConverTo_Default_Details_FormerSpecialty_Model(FormerSpecialty FormerSpecialty)
        {  
			Default_Details_FormerSpecialty_Model Default_Details_FormerSpecialty_Model = new Default_Details_FormerSpecialty_Model();
			Default_Details_FormerSpecialty_Model.toStringValue = FormerSpecialty.ToString();
			Default_Details_FormerSpecialty_Model.Code = FormerSpecialty.Code;
			Default_Details_FormerSpecialty_Model.Name = FormerSpecialty.Name;
			Default_Details_FormerSpecialty_Model.Description = FormerSpecialty.Description;
			Default_Details_FormerSpecialty_Model.Id = FormerSpecialty.Id;
            return Default_Details_FormerSpecialty_Model;            
        }

		public virtual Default_Details_FormerSpecialty_Model CreateNew()
        {
            FormerSpecialty FormerSpecialty = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Details_FormerSpecialty_Model Default_Details_FormerSpecialty_Model = this.ConverTo_Default_Details_FormerSpecialty_Model(FormerSpecialty);
            return Default_Details_FormerSpecialty_Model;
        } 

        public List<Default_Details_FormerSpecialty_Model> Find(string OrderBy, string FilterBy,  string SearchBy, List<string> SearchCreteria, int? CurrentPage, int? PageSize, out int totalRecords)
        {
            FormerSpecialtyBLO entityBLO = new FormerSpecialtyBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<FormerSpecialty> Query_Entity = entityBLO
                .Find_as_Queryable(OrderBy, FilterBy, SearchBy, SearchCreteria, CurrentPage, PageSize, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Details_FormerSpecialty_Model> ls_models = new List<Default_Details_FormerSpecialty_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Details_FormerSpecialty_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Details_FormerSpecialty_ModelBLM : BaseDefault_Details_FormerSpecialty_ModelBLM
	{
		public Default_Details_FormerSpecialty_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
