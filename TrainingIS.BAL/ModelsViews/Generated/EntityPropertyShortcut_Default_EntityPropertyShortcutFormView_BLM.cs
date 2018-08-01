using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.DAL;
namespace TrainingIS.BLL.ModelsViews
{
	public partial class BaseDefault_EntityPropertyShortcutFormViewBLM : ViewModelBLM
    {
        
        public BaseDefault_EntityPropertyShortcutFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork)
        {

        }

        public virtual EntityPropertyShortcut ConverTo_EntityPropertyShortcut(Default_EntityPropertyShortcutFormView Default_EntityPropertyShortcutFormView)
        {
			EntityPropertyShortcut EntityPropertyShortcut = new EntityPropertyShortcut();
			EntityPropertyShortcut.EntityName = Default_EntityPropertyShortcutFormView.EntityName;
			EntityPropertyShortcut.PropertyName = Default_EntityPropertyShortcutFormView.PropertyName;
			EntityPropertyShortcut.PropertyShortcutName = Default_EntityPropertyShortcutFormView.PropertyShortcutName;
			EntityPropertyShortcut.Description = Default_EntityPropertyShortcutFormView.Description;
			EntityPropertyShortcut.Id = Default_EntityPropertyShortcutFormView.Id;
            return EntityPropertyShortcut;

        }
        public virtual Default_EntityPropertyShortcutFormView ConverTo_Default_EntityPropertyShortcutFormView(EntityPropertyShortcut EntityPropertyShortcut)
        {
            Default_EntityPropertyShortcutFormView Default_EntityPropertyShortcutFormView = new Default_EntityPropertyShortcutFormView();
			Default_EntityPropertyShortcutFormView.EntityName = EntityPropertyShortcut.EntityName;
			Default_EntityPropertyShortcutFormView.PropertyName = EntityPropertyShortcut.PropertyName;
			Default_EntityPropertyShortcutFormView.PropertyShortcutName = EntityPropertyShortcut.PropertyShortcutName;
			Default_EntityPropertyShortcutFormView.Description = EntityPropertyShortcut.Description;
			Default_EntityPropertyShortcutFormView.Id = EntityPropertyShortcut.Id;
            return Default_EntityPropertyShortcutFormView;            
        }
    }

	public partial class Default_EntityPropertyShortcutFormViewBLM : BaseDefault_EntityPropertyShortcutFormViewBLM
	{
		public Default_EntityPropertyShortcutFormViewBLM(UnitOfWork unitOfWork) :base(unitOfWork) {

		}
	}
	 
}
