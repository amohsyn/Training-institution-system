using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class AuthrorizationAppDAO : BaseDAO<AuthrorizationApp>{
        
		public AuthrorizationAppDAO(DbContext context) : base(context)
		{

        }

   }
}
