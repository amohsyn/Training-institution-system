﻿//modelType = Default_Schoollevel_Create_Model

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
	public partial class BaseDefault_Schoollevel_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_Schoollevel_ModelBLM Default_Form_Schoollevel_ModelBLM {set;get;}
        
		public BaseDefault_Schoollevel_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Schoollevel_ModelBLM = new Default_Form_Schoollevel_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Schoollevel ConverTo_Schoollevel(Default_Schoollevel_Create_Model Default_Schoollevel_Create_Model)
        {
            var Schoollevel = Default_Form_Schoollevel_ModelBLM.ConverTo_Schoollevel(Default_Schoollevel_Create_Model);
            return Schoollevel;
        }

		public virtual Default_Schoollevel_Create_Model ConverTo_Default_Schoollevel_Create_Model(Schoollevel Schoollevel)
        {
            Default_Schoollevel_Create_Model Default_Schoollevel_Create_Model = new Default_Schoollevel_Create_Model();
            Default_Form_Schoollevel_ModelBLM.ConverTo_Default_Form_Schoollevel_Model(Default_Schoollevel_Create_Model, Schoollevel);
            return Default_Schoollevel_Create_Model;            
        }

		public virtual Default_Schoollevel_Create_Model CreateNew()
        {
            Schoollevel Schoollevel = new SchoollevelBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Schoollevel_Create_Model Default_Schoollevel_Create_Model = this.ConverTo_Default_Schoollevel_Create_Model(Schoollevel);
            return Default_Schoollevel_Create_Model;
        } 

		public virtual List<Default_Schoollevel_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            SchoollevelBLO entityBLO = new SchoollevelBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Schoollevel> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Schoollevel_Create_Model> ls_models = new List<Default_Schoollevel_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Schoollevel_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Schoollevel_Create_ModelBLM : BaseDefault_Schoollevel_Create_Model_BLM
	{
		public Default_Schoollevel_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
