using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class EntityPropertyShortcutDAO : BaseDAO<EntityPropertyShortcut>{
        
		public EntityPropertyShortcutDAO(DbContext context) : base(context)
		{

        }

   }
}
