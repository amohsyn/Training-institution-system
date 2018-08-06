using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AuthrorizationAppDAO : BaseDAO<AuthrorizationAppFormView>{
        
		public AuthrorizationAppDAO(DbContext context) : base(context)
		{

        }

   }
}
