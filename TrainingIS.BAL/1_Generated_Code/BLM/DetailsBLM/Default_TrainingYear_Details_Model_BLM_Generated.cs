﻿//modelType = Default_TrainingYear_Details_Model

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
	public partial class BaseDefault_TrainingYear_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_TrainingYear_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual TrainingYear ConverTo_TrainingYear(Default_TrainingYear_Details_Model Default_TrainingYear_Details_Model)
        {
			TrainingYear TrainingYear = null;
            if (Default_TrainingYear_Details_Model.Id != 0)
            {
                TrainingYear = new TrainingYearBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_TrainingYear_Details_Model.Id);
            }
            else
            {
                TrainingYear = new TrainingYear();
            } 
			TrainingYear.Code = Default_TrainingYear_Details_Model.Code;
			TrainingYear.StartDate = DefaultDateTime_If_Empty(Default_TrainingYear_Details_Model.StartDate);
			TrainingYear.EndtDate = DefaultDateTime_If_Empty(Default_TrainingYear_Details_Model.EndtDate);
			TrainingYear.Id = Default_TrainingYear_Details_Model.Id;
            return TrainingYear;
        }
        public virtual Default_TrainingYear_Details_Model ConverTo_Default_TrainingYear_Details_Model(TrainingYear TrainingYear)
        {  
			Default_TrainingYear_Details_Model Default_TrainingYear_Details_Model = new Default_TrainingYear_Details_Model();
			Default_TrainingYear_Details_Model.toStringValue = TrainingYear.ToString();
			Default_TrainingYear_Details_Model.Code = TrainingYear.Code;
			Default_TrainingYear_Details_Model.StartDate = DefaultDateTime_If_Empty(TrainingYear.StartDate);
			Default_TrainingYear_Details_Model.EndtDate = DefaultDateTime_If_Empty(TrainingYear.EndtDate);
			Default_TrainingYear_Details_Model.Id = TrainingYear.Id;
            return Default_TrainingYear_Details_Model;            
        }

		public virtual Default_TrainingYear_Details_Model CreateNew()
        {
            TrainingYear TrainingYear = new TrainingYearBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_TrainingYear_Details_Model Default_TrainingYear_Details_Model = this.ConverTo_Default_TrainingYear_Details_Model(TrainingYear);
            return Default_TrainingYear_Details_Model;
        } 

		public virtual List<Default_TrainingYear_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TrainingYearBLO entityBLO = new TrainingYearBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<TrainingYear> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_TrainingYear_Details_Model> ls_models = new List<Default_TrainingYear_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_TrainingYear_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_TrainingYear_Details_ModelBLM : BaseDefault_TrainingYear_Details_Model_BLM
	{
		public Default_TrainingYear_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
