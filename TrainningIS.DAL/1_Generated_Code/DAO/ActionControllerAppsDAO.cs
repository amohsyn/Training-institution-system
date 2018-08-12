using GApp.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class ActionControllerAppsDAO : BaseDAO<ActionControllerApp>{
        
		public ActionControllerAppsDAO(DbContext context) : base(context)
		{

        }

   }
}
