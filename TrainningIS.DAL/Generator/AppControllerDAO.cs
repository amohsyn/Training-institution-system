using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AppControllerDAO : BaseDAO<AppController>{
        
		public AppControllerDAO(DbContext context) : base(context)
		{

        }

		public AppControllerDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
