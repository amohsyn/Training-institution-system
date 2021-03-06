﻿//modelType = Default_SeanceDay_Details_Model

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
	public partial class BaseDefault_SeanceDay_Details_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_SeanceDay_Details_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual SeanceDay ConverTo_SeanceDay(Default_SeanceDay_Details_Model Default_SeanceDay_Details_Model)
        {
			SeanceDay SeanceDay = null;
            if (Default_SeanceDay_Details_Model.Id != 0)
            {
                SeanceDay = new SeanceDayBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_SeanceDay_Details_Model.Id);
            }
            else
            {
                SeanceDay = new SeanceDay();
            } 
			SeanceDay.Name = Default_SeanceDay_Details_Model.Name;
			SeanceDay.Code = Default_SeanceDay_Details_Model.Code;
			SeanceDay.Day = Default_SeanceDay_Details_Model.Day;
			SeanceDay.Description = Default_SeanceDay_Details_Model.Description;
			SeanceDay.Id = Default_SeanceDay_Details_Model.Id;
            return SeanceDay;
        }
        public virtual Default_SeanceDay_Details_Model ConverTo_Default_SeanceDay_Details_Model(SeanceDay SeanceDay)
        {  
			Default_SeanceDay_Details_Model Default_SeanceDay_Details_Model = new Default_SeanceDay_Details_Model();
			Default_SeanceDay_Details_Model.toStringValue = SeanceDay.ToString();
			Default_SeanceDay_Details_Model.Name = SeanceDay.Name;
			Default_SeanceDay_Details_Model.Code = SeanceDay.Code;
			Default_SeanceDay_Details_Model.Day = SeanceDay.Day;
			Default_SeanceDay_Details_Model.Description = SeanceDay.Description;
			Default_SeanceDay_Details_Model.Id = SeanceDay.Id;
            return Default_SeanceDay_Details_Model;            
        }

		public virtual Default_SeanceDay_Details_Model CreateNew()
        {
            SeanceDay SeanceDay = new SeanceDayBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_SeanceDay_Details_Model Default_SeanceDay_Details_Model = this.ConverTo_Default_SeanceDay_Details_Model(SeanceDay);
            return Default_SeanceDay_Details_Model;
        } 

		public virtual List<Default_SeanceDay_Details_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SeanceDayBLO entityBLO = new SeanceDayBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<SeanceDay> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_SeanceDay_Details_Model> ls_models = new List<Default_SeanceDay_Details_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_SeanceDay_Details_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_SeanceDay_Details_ModelBLM : BaseDefault_SeanceDay_Details_Model_BLM
	{
		public Default_SeanceDay_Details_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
