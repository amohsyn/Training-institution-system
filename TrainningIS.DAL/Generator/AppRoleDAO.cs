using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AppRoleDAO : BaseDAO<AppRole>{
        
		public AppRoleDAO(DbContext context) : base(context)
		{

        }

		public AppRoleDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
