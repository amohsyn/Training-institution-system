using GApp.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AuthrorizationAppsDAO : BaseDAO<AuthrorizationApp>{
        
		public AuthrorizationAppsDAO(DbContext context) : base(context)
		{

        }

   }
}
