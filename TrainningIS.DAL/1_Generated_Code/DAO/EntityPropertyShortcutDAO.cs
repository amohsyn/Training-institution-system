using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class EntityPropertyShortcutDAO : BaseDAO<EntityPropertyShortcut>{
        
		public EntityPropertyShortcutDAO(DbContext context) : base(context)
		{

        }

   }
}
