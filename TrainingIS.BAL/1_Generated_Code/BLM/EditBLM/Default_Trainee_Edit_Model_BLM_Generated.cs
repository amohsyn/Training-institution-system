﻿//modelType = Default_Trainee_Edit_Model

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
	public partial class BaseDefault_Trainee_Edit_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_Trainee_ModelBLM Default_Form_Trainee_ModelBLM {set;get;}
        
		public BaseDefault_Trainee_Edit_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_Trainee_ModelBLM = new Default_Form_Trainee_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual Trainee ConverTo_Trainee(Default_Trainee_Edit_Model Default_Trainee_Edit_Model)
        {
            var Trainee = Default_Form_Trainee_ModelBLM.ConverTo_Trainee(Default_Trainee_Edit_Model);
            return Trainee;
        }

		public virtual Default_Trainee_Edit_Model ConverTo_Default_Trainee_Edit_Model(Trainee Trainee)
        {
            Default_Trainee_Edit_Model Default_Trainee_Edit_Model = new Default_Trainee_Edit_Model();
            Default_Form_Trainee_ModelBLM.ConverTo_Default_Form_Trainee_Model(Default_Trainee_Edit_Model, Trainee);
            return Default_Trainee_Edit_Model;            
        }

		public virtual Default_Trainee_Edit_Model CreateNew()
        {
            Trainee Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Trainee_Edit_Model Default_Trainee_Edit_Model = this.ConverTo_Default_Trainee_Edit_Model(Trainee);
            return Default_Trainee_Edit_Model;
        } 

		public virtual List<Default_Trainee_Edit_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            TraineeBLO entityBLO = new TraineeBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<Trainee> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Trainee_Edit_Model> ls_models = new List<Default_Trainee_Edit_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Trainee_Edit_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Trainee_Edit_ModelBLM : BaseDefault_Trainee_Edit_Model_BLM
	{
		public Default_Trainee_Edit_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
