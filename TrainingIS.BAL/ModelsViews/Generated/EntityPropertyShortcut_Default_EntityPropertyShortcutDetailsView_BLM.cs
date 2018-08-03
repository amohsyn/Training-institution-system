using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
using GApp.Core.Utils;

namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_EntityPropertyShortcutDetailsViewBLM : ViewModelBLM
    {
        
        public BaseDefault_EntityPropertyShortcutDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual EntityPropertyShortcut ConverTo_EntityPropertyShortcut(Default_EntityPropertyShortcutDetailsView Default_EntityPropertyShortcutDetailsView)
        {
			EntityPropertyShortcut EntityPropertyShortcut = null;
            if (Default_EntityPropertyShortcutDetailsView.Id != 0)
            {
                EntityPropertyShortcut = new EntityPropertyShortcutBLO(this.UnitOfWork).FindBaseEntityByID(Default_EntityPropertyShortcutDetailsView.Id);
            }
            else
            {
                EntityPropertyShortcut = new EntityPropertyShortcut();
            } 
			EntityPropertyShortcut.EntityName = Default_EntityPropertyShortcutDetailsView.EntityName;
			EntityPropertyShortcut.PropertyName = Default_EntityPropertyShortcutDetailsView.PropertyName;
			EntityPropertyShortcut.PropertyShortcutName = Default_EntityPropertyShortcutDetailsView.PropertyShortcutName;
			EntityPropertyShortcut.Description = Default_EntityPropertyShortcutDetailsView.Description;
			EntityPropertyShortcut.Id = Default_EntityPropertyShortcutDetailsView.Id;
            return EntityPropertyShortcut;
        }
        public virtual Default_EntityPropertyShortcutDetailsView ConverTo_Default_EntityPropertyShortcutDetailsView(EntityPropertyShortcut EntityPropertyShortcut)
        {  
			Default_EntityPropertyShortcutDetailsView Default_EntityPropertyShortcutDetailsView = new Default_EntityPropertyShortcutDetailsView();
			Default_EntityPropertyShortcutDetailsView.toStringValue = EntityPropertyShortcut.ToString();
			Default_EntityPropertyShortcutDetailsView.EntityName = EntityPropertyShortcut.EntityName;
			Default_EntityPropertyShortcutDetailsView.PropertyName = EntityPropertyShortcut.PropertyName;
			Default_EntityPropertyShortcutDetailsView.PropertyShortcutName = EntityPropertyShortcut.PropertyShortcutName;
			Default_EntityPropertyShortcutDetailsView.Description = EntityPropertyShortcut.Description;
			Default_EntityPropertyShortcutDetailsView.Id = EntityPropertyShortcut.Id;
            return Default_EntityPropertyShortcutDetailsView;            
        }
    }

	public partial class Default_EntityPropertyShortcutDetailsViewBLM : BaseDefault_EntityPropertyShortcutDetailsViewBLM
	{
		public Default_EntityPropertyShortcutDetailsViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
