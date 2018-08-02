using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class RoleAppDAO : BaseDAO<RoleApp>{
        
		public RoleAppDAO(DbContext context) : base(context)
		{

        }

		public RoleAppDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
