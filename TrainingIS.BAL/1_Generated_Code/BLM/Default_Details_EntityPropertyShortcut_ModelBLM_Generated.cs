//modelType = Default_Details_EntityPropertyShortcut_Model

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

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_Details_EntityPropertyShortcut_ModelBLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Details_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
        {
			this.GAppContext = GAppContext;
        }

        public virtual EntityPropertyShortcut ConverTo_EntityPropertyShortcut(Default_Details_EntityPropertyShortcut_Model Default_Details_EntityPropertyShortcut_Model)
        {
			EntityPropertyShortcut EntityPropertyShortcut = null;
            if (Default_Details_EntityPropertyShortcut_Model.Id != 0)
            {
                EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork,this.GAppContext).FindBaseEntityByID(Default_Details_EntityPropertyShortcut_Model.Id);
            }
            else
            {
                EntityPropertyShortcut = new EntityPropertyShortcut();
            } 
			EntityPropertyShortcut.EntityName = Default_Details_EntityPropertyShortcut_Model.EntityName;
			EntityPropertyShortcut.PropertyName = Default_Details_EntityPropertyShortcut_Model.PropertyName;
			EntityPropertyShortcut.PropertyShortcutName = Default_Details_EntityPropertyShortcut_Model.PropertyShortcutName;
			EntityPropertyShortcut.Description = Default_Details_EntityPropertyShortcut_Model.Description;
			EntityPropertyShortcut.Id = Default_Details_EntityPropertyShortcut_Model.Id;
            return EntityPropertyShortcut;
        }
        public virtual Default_Details_EntityPropertyShortcut_Model ConverTo_Default_Details_EntityPropertyShortcut_Model(EntityPropertyShortcut EntityPropertyShortcut)
        {  
			Default_Details_EntityPropertyShortcut_Model Default_Details_EntityPropertyShortcut_Model = new Default_Details_EntityPropertyShortcut_Model();
			Default_Details_EntityPropertyShortcut_Model.toStringValue = EntityPropertyShortcut.ToString();
			Default_Details_EntityPropertyShortcut_Model.EntityName = EntityPropertyShortcut.EntityName;
			Default_Details_EntityPropertyShortcut_Model.PropertyName = EntityPropertyShortcut.PropertyName;
			Default_Details_EntityPropertyShortcut_Model.PropertyShortcutName = EntityPropertyShortcut.PropertyShortcutName;
			Default_Details_EntityPropertyShortcut_Model.Description = EntityPropertyShortcut.Description;
			Default_Details_EntityPropertyShortcut_Model.Id = EntityPropertyShortcut.Id;
            return Default_Details_EntityPropertyShortcut_Model;            
        }

		public virtual Default_Details_EntityPropertyShortcut_Model CreateNew()
        {
            EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcut();
            Default_Details_EntityPropertyShortcut_Model Default_Details_EntityPropertyShortcut_Model = this.ConverTo_Default_Details_EntityPropertyShortcut_Model(EntityPropertyShortcut);
            return Default_Details_EntityPropertyShortcut_Model;
        } 
    }

	public partial class Default_Details_EntityPropertyShortcut_ModelBLM : BaseDefault_Details_EntityPropertyShortcut_ModelBLM
	{
		public Default_Details_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
