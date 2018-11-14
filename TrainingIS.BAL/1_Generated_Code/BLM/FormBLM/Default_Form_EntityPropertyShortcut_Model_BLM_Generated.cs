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
	public partial class BaseDefault_Form_EntityPropertyShortcut_Model_BLM : BaseModelBLM
    {
        public GAppContext GAppContext {set;get;}
        public BaseDefault_Form_EntityPropertyShortcut_Model_BLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext)
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
			EntityPropertyShortcut.Reference = Default_Form_EntityPropertyShortcut_Model.Reference;
			EntityPropertyShortcut.Id = Default_Form_EntityPropertyShortcut_Model.Id;
            return EntityPropertyShortcut;
        }
        public virtual void ConverTo_Default_Form_EntityPropertyShortcut_Model(Default_Form_EntityPropertyShortcut_Model Default_Form_EntityPropertyShortcut_Model, EntityPropertyShortcut EntityPropertyShortcut)
        {  
			 
			Default_Form_EntityPropertyShortcut_Model.toStringValue = EntityPropertyShortcut.ToString();
			Default_Form_EntityPropertyShortcut_Model.EntityName = EntityPropertyShortcut.EntityName;
			Default_Form_EntityPropertyShortcut_Model.PropertyName = EntityPropertyShortcut.PropertyName;
			Default_Form_EntityPropertyShortcut_Model.PropertyShortcutName = EntityPropertyShortcut.PropertyShortcutName;
			Default_Form_EntityPropertyShortcut_Model.Description = EntityPropertyShortcut.Description;
			Default_Form_EntityPropertyShortcut_Model.Id = EntityPropertyShortcut.Id;
			Default_Form_EntityPropertyShortcut_Model.Reference = EntityPropertyShortcut.Reference;
                     
        }

    }

	public partial class Default_Form_EntityPropertyShortcut_ModelBLM : BaseDefault_Form_EntityPropertyShortcut_Model_BLM
	{
		public Default_Form_EntityPropertyShortcut_ModelBLM(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext) :base(unitOfWork, GAppContext) {

		}
	}
	 
}
