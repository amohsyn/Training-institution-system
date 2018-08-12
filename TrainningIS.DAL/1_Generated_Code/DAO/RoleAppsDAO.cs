using GApp.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class RoleAppsDAO : BaseDAO<RoleApp>{
        
		public RoleAppsDAO(DbContext context) : base(context)
		{

        }

   }
}
