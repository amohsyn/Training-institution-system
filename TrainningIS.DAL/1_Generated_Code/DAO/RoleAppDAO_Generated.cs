using GApp.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class RoleAppDAO : BaseDAO<RoleApp>{
        
		public RoleAppDAO(DbContext context) : base(context)
		{

        }

   }
}
