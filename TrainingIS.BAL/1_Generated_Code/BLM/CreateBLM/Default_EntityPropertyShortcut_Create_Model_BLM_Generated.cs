﻿//modelType = Default_EntityPropertyShortcut_Create_Model

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
using GApp.Entities;
using GApp.Models.Pages;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_EntityPropertyShortcut_Create_Model_BLM : BaseModelBLM
    {
       
        public GAppContext GAppContext {set;get;} 
		public Default_Form_EntityPropertyShortcut_ModelBLM Default_Form_EntityPropertyShortcut_ModelBLM {set;get;}
        
		public BaseDefault_EntityPropertyShortcut_Create_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
			Default_Form_EntityPropertyShortcut_ModelBLM = new Default_Form_EntityPropertyShortcut_ModelBLM(this.UnitOfWork, this.GAppContext);
        }

		public virtual EntityPropertyShortcut ConverTo_EntityPropertyShortcut(Default_EntityPropertyShortcut_Create_Model Default_EntityPropertyShortcut_Create_Model)
        {
            var EntityPropertyShortcut = Default_Form_EntityPropertyShortcut_ModelBLM.ConverTo_EntityPropertyShortcut(Default_EntityPropertyShortcut_Create_Model);
            return EntityPropertyShortcut;
        }

		public virtual Default_EntityPropertyShortcut_Create_Model ConverTo_Default_EntityPropertyShortcut_Create_Model(EntityPropertyShortcut EntityPropertyShortcut)
        {
            Default_EntityPropertyShortcut_Create_Model Default_EntityPropertyShortcut_Create_Model = new Default_EntityPropertyShortcut_Create_Model();
            Default_Form_EntityPropertyShortcut_ModelBLM.ConverTo_Default_Form_EntityPropertyShortcut_Model(Default_EntityPropertyShortcut_Create_Model, EntityPropertyShortcut);
            return Default_EntityPropertyShortcut_Create_Model;            
        }

		public virtual Default_EntityPropertyShortcut_Create_Model CreateNew()
        {
            EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_EntityPropertyShortcut_Create_Model Default_EntityPropertyShortcut_Create_Model = this.ConverTo_Default_EntityPropertyShortcut_Create_Model(EntityPropertyShortcut);
            return Default_EntityPropertyShortcut_Create_Model;
        } 

		public virtual List<Default_EntityPropertyShortcut_Create_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            EntityPropertyShortcutBLO entityBLO = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<EntityPropertyShortcut> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_EntityPropertyShortcut_Create_Model> ls_models = new List<Default_EntityPropertyShortcut_Create_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_EntityPropertyShortcut_Create_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_EntityPropertyShortcut_Create_ModelBLM : BaseDefault_EntityPropertyShortcut_Create_Model_BLM
	{
		public Default_EntityPropertyShortcut_Create_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
