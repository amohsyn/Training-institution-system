//modelType = Default_Form_EntityPropertyShortcut_Model

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
	public partial class BaseDefault_Form_EntityPropertyShortcut_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual EntityPropertyShortcut ConverTo_EntityPropertyShortcut(Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model)
        {
			EntityPropertyShortcut EntityPropertyShortcut = null;
            if (Default_Form_EntityPropertyShortcut_Model.Id != 0)
            {
                EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Form_EntityPropertyShortcut_Model.Id);
            }
            else
            {
                EntityPropertyShortcut = new EntityPropertyShortcut();
            } 
			EntityPropertyShortcut.EntityName = Default_Form_EntityPropertyShortcut_Model.EntityName;
			EntityPropertyShortcut.PropertyName = Default_Form_EntityPropertyShortcut_Model.PropertyName;
			EntityPropertyShortcut.PropertyShortcutName = Default_Form_EntityPropertyShortcut_Model.PropertyShortcutName;
			EntityPropertyShortcut.Description = Default_Form_EntityPropertyShortcut_Model.Description;
			EntityPropertyShortcut.Id = Default_Form_EntityPropertyShortcut_Model.Id;
            return EntityPropertyShortcut;
        }
        public virtual Default_Form_EntityPropertyShortcut_Model ConverTo_Default_Form_EntityPropertyShortcut_Model(EntityPropertyShortcut EntityPropertyShortcut)
        {  
			Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model = new Default_Form_EntityPropertyShortcut_Model();
			Default_Form_EntityPropertyShortcut_Model.toStringValue = EntityPropertyShortcut.ToString();
			Default_Form_EntityPropertyShortcut_Model.EntityName = EntityPropertyShortcut.EntityName;
			Default_Form_EntityPropertyShortcut_Model.PropertyName = EntityPropertyShortcut.PropertyName;
			Default_Form_EntityPropertyShortcut_Model.PropertyShortcutName = EntityPropertyShortcut.PropertyShortcutName;
			Default_Form_EntityPropertyShortcut_Model.Description = EntityPropertyShortcut.Description;
			Default_Form_EntityPropertyShortcut_Model.Id = EntityPropertyShortcut.Id;
            return Default_Form_EntityPropertyShortcut_Model;            
        }

		public virtual Default_Form_EntityPropertyShortcut_Model CreateNew()
        {
            EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext).CreateInstance();
            Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model = this.ConverTo_Default_Form_EntityPropertyShortcut_Model(EntityPropertyShortcut);
            return Default_Form_EntityPropertyShortcut_Model;
        } 

		public List<Default_Form_EntityPropertyShortcut_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            EntityPropertyShortcutBLO entityBLO = new EntityPropertyShortcutBLO(this.UnitOfWork, this.GAppContext);
            IQueryable<EntityPropertyShortcut> Query_Entity = entityBLO
                .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            var list_entities = Query_Entity.ToList();

            // Converto List of Absences to List of Model
            List<Default_Form_EntityPropertyShortcut_Model> ls_models = new List<Default_Form_EntityPropertyShortcut_Model>();
            foreach (var entity in list_entities)
            {
                ls_models.Add(this.ConverTo_Default_Form_EntityPropertyShortcut_Model(entity));
            }
            return ls_models;
        }


    }

	public partial class Default_Form_EntityPropertyShortcut_ModelBLM : BaseDefault_Form_EntityPropertyShortcut_ModelBLM
	{
		public Default_Form_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
