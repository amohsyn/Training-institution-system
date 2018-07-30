using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AppControllerActionDAO : BaseDAO<AppControllerAction>{
        
		public AppControllerActionDAO(DbContext context) : base(context)
		{

        }

		public AppControllerActionDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
