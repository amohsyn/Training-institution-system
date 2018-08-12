using GApp.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class EntityPropertyShortcutsDAO : BaseDAO<EntityPropertyShortcut>{
        
		public EntityPropertyShortcutsDAO(DbContext context) : base(context)
		{

        }

   }
}
