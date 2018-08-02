using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ActionControllerAppDAO : BaseDAO<ActionControllerApp>{
        
		public ActionControllerAppDAO(DbContext context) : base(context)
		{

        }

		public ActionControllerAppDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}
